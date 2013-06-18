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
        private CTLInputSource inputSource;
        private CTLDomain domain;
        private Timer updateTimer;
        private DateTime startSessionTime;
        private TimeSpan sessionTime
        {
            get { return (DateTime.Now - startSessionTime); }
        }
        private bool activeTasksHaveChanged;
        // The list of tasks that is used for model calculation 
        private List<CTLTask> tasksInCalculationFrame;

        /// <summary>
        /// A list of events that are currently in progress
        /// </summary>
        public List<CTLEvent> activeEvents { get; private set; }
        /// <summary>
        /// A list of tasks that are currently in progress
        /// </summary>
        public List<CTLTask> activeTasks { get; private set; }

        /// <summary>
        /// The CTLModel constructor.
        /// </summary>
        /// <param name="inputSource">a input source for the model</param>
        /// <param name="domain">the domain in which the model will work</param>
        public CTLModel(CTLInputSource inputSource, CTLDomain domain)
        {
            this.inputSource = inputSource;
            inputSource.delegateObject = this;
            this.domain = domain;
            lengthTimeframe = new TimeSpan(0, 0, 10); //hours, minutes, seconds
            activeEvents = new List<CTLEvent>();
            activeTasks = new List<CTLTask>();
            tasksInCalculationFrame = new List<CTLTask>();
        }

        #region Abstract CLModel implementation

        /// <summary>
        /// Starts a new session, calculateModelValue() will
        /// now produce valid values.
        /// </summary>
        public override void startSession()
        {
            Console.WriteLine("CTLModel.startSession()");
            startSessionTime = DateTime.Now;
            inputSource.startReceivingInput();

            // Create and start a timer to update the model input values
            updateTimer = new Timer(updateTimerCallback, null, 0, 500);
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

            return CTLMath.calculateMentalWorkLoad(lip, mo, tss, tasksInCalculationFrame);
        }

        /// <summary>
        /// Stops the current session.
        /// </summary>
        public override void stopSession()
        {
            Console.WriteLine("CTLModel.stopSession()");

            inputSource.stopReceivingInput();

            updateTimer.Dispose();
        }

        #endregion

        #region CTLInputSourceDelegate methods

        /// <summary>
        /// Proces events that have started
        /// </summary>
        /// <param name="eventElement"></param>
        public void eventHasStarted(InputElement eventElement)
        {
            Console.WriteLine("CTLModel.eventHasStarted()");
            CTLEvent eventStarted = domain.generateEvent(eventElement);
            if (eventStarted != null)
            {
                eventStarted.startTime = sessionTime;
                activeEvents.Add(eventStarted);
            }
        }

        /// <summary>
        /// Proces events that have stopped
        /// </summary>
        /// <param name="eventElement"></param>
        public void eventHasStopped(InputElement eventElement)
        {
            Console.WriteLine("CTLModel.eventHasStopped()");

            CTLEvent ctlEventToRemove = null;
            foreach (CTLEvent ctlEvent in activeEvents)
            {
                if (ctlEvent.identifier.Equals(eventElement.identifier))
                {
                    ctlEventToRemove = ctlEvent;
                }
            }

            activeEvents.Remove(ctlEventToRemove);
        }

        /// <summary>
        /// Proces the tasks that have started
        /// </summary>
        /// <param name="taskElement"></param>
        public void taskHasStarted(InputElement taskElement)
        {
            Console.WriteLine("CTLModel.taskHasStarted()");
            
            CTLTask taskStarted = domain.generateTask(taskElement);
            if (taskStarted != null)
            {
                taskStarted.eventIdentifier = taskElement.secondaryIndentifier;
                taskStarted.startTime = taskStarted.endTime = sessionTime;

                foreach (CTLEvent ctlEvent in activeEvents)
                {
                    if (ctlEvent.identifier.Equals(taskStarted.eventIdentifier))
                    {
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

                //TODO: deze bool wisselen voor direct herberekenen (?)
                activeTasksHaveChanged = true;
            }
        }

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

            //TODO: deze bool wisselen voor direct herberekenen (?)
            activeTasksHaveChanged = true;
        }

        #endregion

        private void updateTimerCallback(Object stateInfo)
        {
            // Update the 'end time' for all active events
            foreach (CTLEvent ctlEvent in activeEvents)
            {
                ctlEvent.endTime = sessionTime;
            }

            // Update the 'end time' for all active tasks
            foreach (CTLTask task in activeTasks)
            {
                task.endTime = sessionTime;
            }

            updateTasksInCalculationFrame(sessionTime);
        }

        private void updateTasksInCalculationFrame(TimeSpan sessionTime)
        {
            List<CTLTask> tasksToRemove = new List<CTLTask>();
            foreach (CTLTask task in tasksInCalculationFrame)
            {
                // Delete all tasks that have dropped outside of the calculation frame
                if (task.endTime < (sessionTime - lengthTimeframe))
                {
                    tasksToRemove.Add(task);
                }
                // When the start time of a task drops outside of the calculation frame, crop it
                else if (task.startTime < (sessionTime - lengthTimeframe))
                {
                    task.startTime = (sessionTime - lengthTimeframe);
                }
            }
            foreach (CTLTask task in tasksToRemove)
            {
                tasksInCalculationFrame.Remove(task);
            }

            // When active tasks change, we need to edit the calculation frame
            if (activeTasksHaveChanged)
            {
                // Stop the last task on the frame if still in progress
                if (tasksInCalculationFrame.Count > 0 && tasksInCalculationFrame.Last().inProgress)
                {
                    CTLTask lastTask = tasksInCalculationFrame.Last();
                    lastTask.endTime = sessionTime;
                    lastTask.inProgress = false;
                }

                // Put a cloned task on the frame if there is only one active task
                if (activeTasks.Count == 1)
                {
                    CTLTask newTask = (CTLTask)activeTasks[0].Clone();
                    newTask.startTime = sessionTime;
                    newTask.endTime = sessionTime;
                    newTask.inProgress = true;

                    tasksInCalculationFrame.Add(newTask);
                }

                // Put a multitask on the frame if there are multiple active tasks
                if (activeTasks.Count > 1)
                {
                    CTLTask multitask = activeTasks[0];

                    // Because of the checked count, this will loop at least once
                    for (int i = 1; i < activeTasks.Count; i++)
                    {
                        multitask = createMultitask(multitask, activeTasks[i]);
                    }

                    multitask.inProgress = true;
                    tasksInCalculationFrame.Add(multitask);
                }
                
                activeTasksHaveChanged = false;
            }
            else if (tasksInCalculationFrame.Count > 0 && tasksInCalculationFrame.Last().inProgress)
            {
                tasksInCalculationFrame.Last().endTime = sessionTime;
            }
        }

        /// <summary>
        /// Creates a multitask from two tasks. The CTL-values for the multitask will be
        /// calculated and set, however the start- and endtime will not.
        /// </summary>
        /// <param name="task1">the first task</param>
        /// <param name="task2">the second task</param>
        /// <returns>the multitask</returns>
        private CTLTask createMultitask(CTLTask task1, CTLTask task2)
        {
            // Create a new CTLTask
            CTLTask multiTask = new CTLTask(task1.identifier + "+" + task2.identifier, task1.name + task2.name, null);
            // Set its values
            if (task1 != null && task2 != null)
            {
                multiTask.moValue = CTLMath.multitaskMO(task1, task2);
                multiTask.lipValue = CTLMath.multitaskLip(task1, task2);
                multiTask.informationDomains = CTLMath.multitaskDomain(task1, task2);
            }
            return multiTask;
        }
    }
}
