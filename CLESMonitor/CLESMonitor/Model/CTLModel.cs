using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CLESMonitor.Model
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
            double lip = calculateOverallLip(tasksInCalculationFrame, lengthTimeframe);
            double mo = calculateOverallMo(tasksInCalculationFrame, lengthTimeframe);
            double tss = calculateTSS(tasksInCalculationFrame);

            return calculateMentalWorkLoad(lip, mo, tss);
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
                multiTask.moValue = multitaskMO(task1, task2);
                multiTask.lipValue = multitaskLip(task1, task2);
                multiTask.informationDomains = multitaskDomain(task1, task2);
            }
            return multiTask;
        }

        /// <summary>
        /// Sets the array of domains of the new tasks to be the combined domains of both task1 and task2
        /// Domains that occur in both tasks are only added once.
        /// </summary>
        /// <param name="task1">The first task that overlaps</param>
        /// <param name="task2">The task that overlaps with task1</param>
        /// <returns>An array of informationDomains</returns>
        public static List<int> multitaskDomain(CTLTask task1, CTLTask task2)
        {
            List<int> newDomain = null;
            if (task1 != null && task2 != null)
            {
                if (task1.informationDomains != null && task2.informationDomains != null)
                {
                    newDomain = task1.informationDomains;
                    List<int> tempDomain = task2.informationDomains;
                    for (int i = 0; i <= tempDomain.Count - 1; i++)
                    {
                        if (!newDomain.Contains(tempDomain[i]))
                        {
                            newDomain.Add(tempDomain[i]);
                        }
                    }
                }
            }
            return newDomain;
        }

        /// <summary>
        /// Sets the MO-value of a multitaks to be the sum of the MO-values of the original tasks when this sum is greater than 1.
        /// </summary>
        /// <param name="task1">The first task that overlaps</param>
        /// <param name="task2">The task that overlaps with task1</param>
        /// <returns>A double representing the MO value of the new multitask (value between 1 and 2)</returns>
        public static double multitaskMO(CTLTask task1, CTLTask task2)
        {
            double MO1 = 0.0;
            double MO2 = 0.0;
            double returnMO = 0.0;

            if (task1 != null && task2 != null)
            {
                MO1 = task1.moValue;
                MO2 = task2.moValue;
                returnMO = Math.Max(MO1 + MO2, 1);
            }
            return returnMO; 
        }

        /// <summary>
        /// Sets the Lip-value of a multitask: the largest of the two lip-values of the original tasks
        /// </summary>
        /// <param name="task1">The first task that overlaps</param>
        /// <param name="task2">The task that overlaps with task1</param>
        /// <returns>The Lip value for a new task</returns>
        public static int multitaskLip(CTLTask task1, CTLTask task2)
        {
            int lip1 = 0;
            int lip2 = 0;
            int returnLip = 0;
            
            if(task1 != null && task2 != null)
            {
                lip1 = task1.lipValue;
                lip2 = task2.lipValue;
                returnLip = Math.Max(lip1, lip2);
            }
            return returnLip;
        }
        
        /// <summary>
        /// Implements the overall Level of Information Processing (LIP) formula as defined in the scientific literature.
        /// </summary>
        /// <param name="tasks">A list of task that are currently in the timeframe</param>
        /// <returns>Average Lip-value (not rounded). 
        /// It can attain values between 1 and 3 (only without overlapping tasks!).</returns>
        public static double calculateOverallLip(List<CTLTask> tasks, TimeSpan lengthTimeframe)
        {
            double lipValue = 0;

            for (int i = 0; i < tasks.Count; i++)
            {
                lipValue += tasks[i].lipValue * tasks[i].getDuration().TotalSeconds;
            }
            lipValue = lipValue / lengthTimeframe.TotalSeconds;

            return lipValue;
        }

        /// <summary>
        /// Implements the overall Mental occupancy (MO) formula as defined in the scientific literature.
        /// </summary>
        /// <param name="tasks">A list of task that are currently in the timeframe</param>
        /// <returns>The normalized MO-value across 1 time frame. 
        /// It can attain values between 0 and 1 (only without overlapping tasks!).</returns>
        public static double calculateOverallMo(List<CTLTask> tasks, TimeSpan lengthTimeframe)
        {
            double moValue = 0;

            for (int i = 0; i < tasks.Count; i++)
            {
                moValue += tasks[i].moValue * tasks[i].getDuration().TotalSeconds;
            }
            moValue = moValue / lengthTimeframe.TotalSeconds;

            return moValue;
        }

        /// <summary>
        /// Implements the overall Task Set Switching (TSS) formula as defined in the scientific literature.
        /// </summary>
        /// <param name="tasks">The list of tasks to use</param>
        /// <returns>The calculated TSS-value. 
        /// It can attain values between 0 and (tasks.Count-1) (only without overlapping tasks!).</returns>
        public static double calculateTSS(List<CTLTask> tasks)
        {
            double tssValue = 0;

            for (int i = 0; i < tasks.Count-1; i++)
            {
                int unionCount = tasks[i].informationDomains.Union(tasks[i + 1].informationDomains).Count();
                int intersectionCount = tasks[i].informationDomains.Intersect(tasks[i + 1].informationDomains).Count();

                tssValue += (unionCount - intersectionCount) / unionCount;
            }

            return tssValue;
        }

        /// <summary>
        /// Implements the mental workload (MWL) formula as defined in the scientific literature
        /// </summary>
        /// <param name="lipValue">The level of information processing</param>
        /// <param name="moValue">The mental occupancy</param>
        /// <param name="tssValue">The task set switches</param>
        /// <returns>The calculated MWL-value</returns>
        private double calculateMentalWorkLoad(double lipValue, double moValue, double tssValue)
        {
            double mwlValue = 0;
            double normalizedTssValue = 0;

            // Define the bounds as tuples (lower bound, upper bound)
            Tuple<int, int> lipBounds = Tuple.Create(0, 3);
            Tuple<int, int> moBounds = Tuple.Create(0, 1);
            Tuple<int, int> tssBounds = Tuple.Create(0, Math.Max(tasksInCalculationFrame.Count - 1,0));

            // Project all metrics on the [0, 1] interval
            double normalizedLipValue = (lipValue - lipBounds.Item1) / lipBounds.Item2;
            double normalizedMoValue = (moValue - moBounds.Item1) / moBounds.Item2;
            // Avoid NaN values!
            if (tssBounds.Item2 != 0)
            {
                normalizedTssValue = (tssValue - tssBounds.Item1) / tssBounds.Item2;
            }

            Vector diagonalVector = new Vector(1.0, 1.0, 1.0);
            Vector mwlVector = new Vector(normalizedLipValue, normalizedMoValue, normalizedTssValue);

            // The distance to the origin (the length of the input vector)
            // For now we use this as MWL value, since the formula appeared to be unuseable
            double distanceToOrigin = mwlVector.length();

            Vector mwlProjDiagonal = mwlVector.orthogonalProjection(diagonalVector);

            Vector zVector = mwlVector - mwlProjDiagonal;

            double distanceToDiagonal = zVector.length(); 
            mwlValue = distanceToOrigin - (1 / distanceToDiagonal);

            // TODO: verander hier de return value in mwlValue wanneer bekend is hoe de berekening zal gaan.
            return distanceToOrigin;
        }
    }
}
