using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CLESMonitor.Model.CL
{
    /// <summary>
    /// This class is an implementation of the CTL-model for cognitive load.
    /// It uses XMLFileTaskParser to receive domain-specific input, PRLDomain to 
    /// convert this into general classes and then calculates cognitive load.
    /// </summary>
    public class CTLModel : CLModel, CTLInputSourceDelegate
    {
        private static int multiTaskCounter = 0;

        private CTLInputSource inputSource;
        private CTLDomain domain;
        private Timer updateTimer;
        private DateTime startSessionTime;
        private TimeSpan sessionTime { get { return (DateTime.Now - startSessionTime); } }
        private bool activeTasksHaveChanged;

        /// <summary>A list of events that are currently in progress</summary>
        public List<CTLEvent> activeEvents { get; private set; }
        /// <summary>A list of tasks that are currently in progress</summary>
        public List<CTLTask> activeTasks { get; private set; }
        /// <summary> The list of tasks that is used for model calculation</summary>
        public List<CTLTask> tasksInCalculationFrame { get; private set; }

        /// <summary>
        /// The constructor method.
        /// </summary>
        /// <param name="inputSource">An input source for the model</param>
        /// <param name="domain">The domain in which the model will work</param>
        public CTLModel(CTLInputSource inputSource, CTLDomain domain)
        {
            this.inputSource = inputSource;
            inputSource.delegateObject = this;
            this.domain = domain;
            lengthTimeframe = new TimeSpan(0, 0, 10);
            activeEvents = new List<CTLEvent>();
            activeTasks = new List<CTLTask>();
            tasksInCalculationFrame = new List<CTLTask>();
        }

        #region Abstract CLModel implementation

        private const int DEFAULT_TIMER_INTERVAL = 1000;

        /// <summary>
        /// Starts a new session, calculateModelValue() will
        /// now produce valid values.
        /// </summary>
        public override void startSession()
        {
            startSession(0, DEFAULT_TIMER_INTERVAL);
        }

        /// <summary>
        /// Starts a new session with the given Timer-parameters, 
        /// calculateModelValue() will now produce valid values.
        /// </summary>
        public void startSession(int dueTime, int period)
        {
            startSessionTime = DateTime.Now;
            inputSource.startReceivingInput();

            // Create and start a timer to update the model input values
            updateTimer = new Timer(updateTimerCallback, null, dueTime, period);
        }

        /// <summary>
        /// (Re)calculates the model value
        /// </summary>
        /// <returns>The model value</returns>
        public override double calculateModelValue()
        {
            // Calculate all necessary values
            double lip = CTLMath.calculateOverallLip(tasksInCalculationFrame, lengthTimeframe);
            double mo = CTLMath.calculateOverallMo(tasksInCalculationFrame, lengthTimeframe);
            double tss = CTLMath.calculateTSS(tasksInCalculationFrame);

            return CTLMath.categoriseWorkLoad(CTLMath.calculateMentalWorkLoad(lip, mo, tss, tasksInCalculationFrame.Count()));
        }

        /// <summary>
        /// Stops the current session.
        /// </summary>
        public override void stopSession()
        {
            inputSource.stopReceivingInput();
            inputSource.reset();
            updateTimer.Dispose();

            activeEvents.Clear();
            activeTasks.Clear();
            tasksInCalculationFrame.Clear();
        }

        #endregion

        #region CTLInputSourceDelegate methods

        /// <summary>
        /// Proces events that have started.
        /// </summary>
        /// <param name="eventElement">The (parsed) event data</param>
        public void eventHasStarted(InputElement eventElement)
        {
            Console.WriteLine("CTLModel.eventHasStarted()");
            CTLEvent ctlEvent = domain.generateEvent(eventElement);
            if (ctlEvent != null)
            {
                ctlEvent.startEvent(sessionTime);
                activeEvents.Add(ctlEvent);
            }
        }

        /// <summary>
        /// Proces events that have stopped.
        /// </summary>
        /// <param name="eventElement">The (parsed) event data</param>
        public void eventHasStopped(InputElement eventElement)
        {
            Console.WriteLine("CTLModel.eventHasStopped()");

            CTLEvent ctlEventToRemove = null;
            foreach (CTLEvent ctlEvent in activeEvents)
            {
                if (ctlEvent.identifier.Equals(eventElement.identifier))
                {
                    ctlEvent.stopEvent(sessionTime);
                    ctlEventToRemove = ctlEvent;
                }
            }

            activeEvents.Remove(ctlEventToRemove);
        }

        /// <summary>
        /// Proces the tasks that have started.
        /// </summary>
        /// <param name="taskElement">The (parsed) task data</param>
        public void taskHasStarted(InputElement taskElement)
        {
            Console.WriteLine("CTLModel.taskHasStarted()");
            
            CTLTask taskStarted = domain.generateTask(taskElement);
            if (taskStarted != null)
            {
                taskStarted.startTime = taskStarted.endTime = sessionTime;

                foreach (CTLEvent ctlEvent in activeEvents)
                {
                    if (ctlEvent.identifier.Equals(taskElement.secondaryIndentifier))
                    {
                        taskStarted.ctlEvent = ctlEvent;

                        // If the task's mo value is not set, fallback by grabbing it from
                        // the event it belongs to.
                        if (taskStarted.moValue == -1)
                        {
                            taskStarted.moValue = ctlEvent.moValue;
                        }
                        // If the task's lip value is not set, fallback by grabbing it from
                        // the event it belongs to.
                        if (taskStarted.lipValue == 0)
                        {
                            taskStarted.lipValue = ctlEvent.lipValue;
                        }
                    }
                }

                activeTasks.Add(taskStarted);

                activeTasksHaveChanged = true;
            }
        }

        /// <summary>
        /// Proces the tasks that have stopped.
        /// </summary>
        /// <param name="taskElement">The (parsed) task data</param>
        public void taskHasStopped(InputElement taskElement)
        {
            Console.WriteLine("CTLModel.taskHasStopped()");

            CTLTask taskToRemove = null;
            foreach (CTLTask task in activeTasks)
            {
                if (task.identifier.Equals(taskElement.identifier))
                {
                    taskToRemove = task;
                }
            }
            activeTasks.Remove(taskToRemove);

            activeTasksHaveChanged = true;
        }

        #endregion

        private void updateTimerCallback(Object stateInfo)
        {
            // Update the 'end time' for all active tasks
            foreach (CTLTask task in activeTasks)
            {
                task.endTime = sessionTime;
            }

            updateCalculationFrame();
        }

        /// <summary>
        /// Updates the calculation frame.
        /// </summary>
        public void updateCalculationFrame()
        {
            // When the start time of a task drops outside of the calculation frame, crop it
            foreach (CTLTask task in tasksInCalculationFrame)
            {
                if (task.startTime < (sessionTime - lengthTimeframe))
                {
                    task.startTime = (sessionTime - lengthTimeframe);
                }
            }
            
            // Update the endTime of the last task in the calculation frame if still in progress
            if (tasksInCalculationFrame.Count > 0 && tasksInCalculationFrame.Last().inProgress)
            {
                tasksInCalculationFrame.Last().endTime = sessionTime;
            }

            removeTasksFromCalculationFrame(sessionTime);

            if (activeTasksHaveChanged)
            {
                addTasksToCalculationFrame(sessionTime);
                activeTasksHaveChanged = false;
            }
        }

        /// <summary>
        /// Removes outdated tasks from the calculation frame.
        /// </summary>
        /// <param name="sessionTime">The current session time</param>
        private void removeTasksFromCalculationFrame(TimeSpan sessionTime)
        {
            List<CTLTask> tasksToRemove = new List<CTLTask>();

            // Delete all tasks that have dropped outside of the calculation frame
            foreach (CTLTask task in tasksInCalculationFrame)
            {
                if (task.endTime < (sessionTime - lengthTimeframe))
                {
                    tasksToRemove.Add(task);
                }
            }

            foreach (CTLTask task in tasksToRemove)
            {
                tasksInCalculationFrame.Remove(task);
            }
        }

        private void addTasksToCalculationFrame(TimeSpan sessionTime)
        {
            // Stop the last task on the frame (the one most recently added) if still in progress
            if (tasksInCalculationFrame.Count > 0 && tasksInCalculationFrame.Last().inProgress)
            {
                CTLTask lastTask = tasksInCalculationFrame.Last();
                lastTask.endTime = sessionTime;
                lastTask.inProgress = false;   
            }

            // Put a cloned task on the frame
            if (activeTasks.Count == 1)
            {
                CTLTask newTask = (CTLTask)activeTasks[0].Clone();
                newTask.startTime = sessionTime;
                newTask.endTime = sessionTime;
                newTask.inProgress = true;

                tasksInCalculationFrame.Add(newTask);
            }
            // Put a multitask on the frame
            else if (activeTasks.Count > 1)
            {
                displayLogMessage("Er zijn nu meerdere taken tegelijk bezig");

                CTLTask multitask = activeTasks[0];

                // This will loop at least once
                for (int i = 1; i < activeTasks.Count; i++)
                {
                    multitask = createMultitask(multitask, activeTasks[i]);
                }

                multitask.startTime = multitask.endTime = sessionTime;
                multitask.inProgress = true;
                tasksInCalculationFrame.Add(multitask);
            }
        }

        /// <summary>
        /// Creates a multitask from two tasks. The CTL-values for the multitask will be
        /// calculated and set, however the start- and endtime will not.
        /// </summary>
        /// <param name="task1">the first task</param>
        /// <param name="task2">the second task</param>
        /// <returns>the multitask</returns>
        private static CTLTask createMultitask(CTLTask task1, CTLTask task2)
        {
            // Create a new CTLTask
            CTLTask multiTask = new CTLTask("m"+multiTaskCounter+"("+task1.identifier+"+"+task2.identifier+")", task1.name + task2.name);
            // Set its values
            if (task1 != null && task2 != null)
            {
                multiTask.moValue = CTLMath.multitaskMO(task1, task2);
                multiTask.lipValue = CTLMath.multitaskLip(task1, task2);
                multiTask.informationDomains = CTLMath.multitaskDomain(task1, task2);
            }
            multiTaskCounter++;
            return multiTask;
        }

        private void displayLogMessage(string message)
        {
            if (delegateObject != null)
            {
                delegateObject.displayLogMessage("[CTLModel] " + message);
            }
        }
    }
}
