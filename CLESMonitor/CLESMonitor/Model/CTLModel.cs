using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

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
        /// The constructor method.
        /// </summary>
        /// <param name="parser">A XMLFileTaskParser to use as input for the model</param>
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
            double lip = calculateOverallLip(tasksInCalculationFrame);
            double mo = calculateOverallMo(tasksInCalculationFrame);
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
            activeEvents.Remove(getEventFromIdentifier(eventElement.identifier));
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
                taskStarted.startTime = sessionTime;
               // taskStarted.endTime = sessionTime;

                setMoAndLip(taskStarted);
                activeTasks.Add(taskStarted);

                Console.WriteLine(taskStarted.ToString());

                //TODO: deze bool wisselen voor direct herberekenen (?)
                activeTasksHaveChanged = true;
            }
        }

        public void taskHasStopped(InputElement taskElement)
        {
            Console.WriteLine("CTLModel.taskHasStopped()");
            activeTasks.Remove(getTaskFromIdentifier(taskElement.identifier));

            //TODO: deze bool wisselen voor direct herberekenen (?)
            activeTasksHaveChanged = true;
        }

        #endregion

        private void updateTimerCallback(Object stateInfo)
        {
            //TODO: debug code
            foreach (CTLTask task in activeTasks)
            {
                Console.WriteLine(task.ToString());
            }

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
                Console.WriteLine(sessionTime.TotalSeconds + ": activeTasksHaveChanged = true");

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

        private CTLTask getTaskFromIdentifier(string identifier)
        {
            CTLTask taskToReturn = null;
            foreach (CTLTask task in activeTasks)
            {
                if (task.identifier.Equals(identifier))
                {
                    taskToReturn = task;
                }
            }
            return taskToReturn;
        }

        private CTLEvent getEventFromIdentifier(string identifier)
        {
            CTLEvent eventToReturn = null;
            foreach (CTLEvent ctlEvent in activeEvents)
            {
                if (ctlEvent.identifier.Equals(identifier))
                {
                    eventToReturn = ctlEvent;
                }
            }
            return eventToReturn;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns>A new task; the multitask</returns>
        private CTLTask createMultitask(CTLTask task1, CTLTask task2)
        {
            //Creat a new CTLTask
            CTLTask multiTask = new CTLTask(task1.identifier + "+" + task2.identifier, task1.name + task2.name, null);
            //and set its values
            multiTask.moValue = multitaskMO(task1, task2);
            multiTask.lipValue = multitaskLip(task1, task2);
            multiTask.informationDomains = multitaskDomain(task1, task2);
            setTimesForMultitask(task1, task2, multiTask);
            return multiTask;
        }

        /// <summary>
        /// Sets the array of domains of the new tasks to be the combined domains of both task1 and task2
        /// Domains that occur in both tasks are only added once.
        /// </summary>
        /// <param name="task1">The first task that overlaps</param>
        /// <param name="task2">The task that overlaps with task1</param>
        /// <returns>An array of informationDomains</returns>
        private List<int> multitaskDomain(CTLTask task1, CTLTask task2)
        {
            List<int> newDomain = task1.informationDomains;
            List<int> tempDomain = task2.informationDomains;
            for (int i = 0; i <= tempDomain.Count - 1; i++)
            {
                if (!newDomain.Contains(tempDomain[i]))      
                {
                    newDomain.Add(tempDomain[i]);
                }
            }
            return newDomain;
        }

        /// <summary>
        /// Sets the MO-value of a multitaks to be the sum of the MO-values of the original tasks when this sum is greater than 1.
        /// </summary>
        /// <param name="task1">The first task that overlaps</param>
        /// <param name="task2">The task that overlaps with task1</param>
        /// <returns>A double representing the MO value of the new multitask</returns>
        private double multitaskMO(CTLTask task1, CTLTask task2)
        {
            double MO1 = task1.moValue;
            double MO2 = task2.moValue;
            return Math.Max(MO1 + MO2, 1); 
        }

        /// <summary>
        /// Sets the Lip-value of a multitask: the largest of the two lip-values of the original tasks
        /// </summary>
        /// <param name="task1">The first task that overlaps</param>
        /// <param name="task2">The task that overlaps with task1</param>
        /// <returns>The Lip value for a new task</returns>
        private int multitaskLip(CTLTask task1, CTLTask task2)
        {
            int Lip1 = task1.lipValue;
            int Lip2 = task2.lipValue;
            return Math.Max(Lip1,Lip2);
        }
        
        /// <summary>
        /// Determines the start- and endtime of a multitask by means of the start- and endtimes of two tasks.
        /// </summary>
        /// <param name="task1">The first task that overlaps</param>
        /// <param name="task2">The task that overlaps with task1</param>
        /// <param name="multiTask">The multitask for which the start and end time will be set</param>
        private void setTimesForMultitask(CTLTask task1, CTLTask task2, CTLTask multiTask)
        {
            // When task1 begins first, the overlap starts when task2 starts
            if (task1.startTime < task2.startTime)
            {
                multiTask.startTime = task2.startTime;
                multiTask.endTime = task1.startTime;
            }
            // When task2 begins first, or when they begin at the same time
            else
            {
                multiTask.startTime = task1.startTime;

                // Endtime is set to be the moment one of both tasks ends
                if (task1.endTime < task2.endTime) {
                    multiTask.endTime = task1.endTime;
                }
                else {
                    multiTask.endTime = task2.startTime;
                }
            }
        }

        /// <summary>
        /// Implements the overall Level of Information Processing (LIP) formula as defined in the scientific literature.
        /// </summary>
        /// <param name="tasks">A list of task that are currently in the timeframe</param>
        /// <returns>Average Lip-value (not rounded). 
        /// It can attain values between 1 and 3 (only without overlapping tasks!).</returns>
        private double calculateOverallLip(List<CTLTask> tasks)
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
        private double calculateOverallMo(List<CTLTask> tasks)
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
        private double calculateTSS(List<CTLTask> tasks)
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

            // Define the bounds as tuples (lower bound, upper bound)
            Tuple<int, int> lipBounds = Tuple.Create(1, 3);
            Tuple<int, int> moBounds = Tuple.Create(0, 1);
            Tuple<int, int> tssBounds = Tuple.Create(0, tasksInCalculationFrame.Count-1);

            // Project all metrics on the [0, 1] interval
            double normalizedLipValue = (lipValue - lipBounds.Item1) / lipBounds.Item2;
            double normalizedMoValue = (moValue - moBounds.Item1) / moBounds.Item2;
            double normalizedTssValue = (tssValue - tssBounds.Item1) / tssBounds.Item2;

            // There is no Vector class (in Windows Forms) available, therefore a triple is used
            // This produces very, very, complicated code..
            Tuple<double, double, double> diagonalVector = Tuple.Create(1.0, 1.0, 1.0);
            Tuple<double, double, double> mwlVector = Tuple.Create(normalizedLipValue, normalizedMoValue, normalizedTssValue);
            double mwlDotProduct = Math.Pow(mwlVector.Item1, 2) + Math.Pow(mwlVector.Item2, 2) + Math.Pow(mwlVector.Item3, 2);
            double distanceToOrigin = Math.Sqrt(mwlDotProduct);
            // Calculate the orthogonal projection
            double topOfFraction = (mwlVector.Item1 * diagonalVector.Item1) 
                + (mwlVector.Item2 * diagonalVector.Item2) 
                + (mwlVector.Item3 * diagonalVector.Item3);
            // The dot product diagonalVector.diagonalVector
            double bottomOfFraction = (diagonalVector.Item1 * diagonalVector.Item1) 
                + (diagonalVector.Item2 * diagonalVector.Item2) 
                + (diagonalVector.Item3 * diagonalVector.Item3);
            double fraction = topOfFraction / bottomOfFraction;

            Tuple<double, double, double> mwlProjDiagonal = Tuple.Create(fraction * diagonalVector.Item1,
                fraction * diagonalVector.Item2, fraction * diagonalVector.Item3);
            Tuple<double, double, double> zVector = Tuple.Create(mwlVector.Item1 - mwlProjDiagonal.Item1,
                mwlVector.Item2 - mwlProjDiagonal.Item2, mwlVector.Item3 - mwlProjDiagonal.Item3);

            double distanceToDiagonal = Math.Sqrt(Math.Pow(zVector.Item1, 2) + Math.Pow(zVector.Item2, 2) + Math.Pow(zVector.Item3, 2));
            mwlValue = distanceToOrigin - (1 / distanceToDiagonal);

            return mwlValue;
        }

        //TODO: Hier nog even kijken of we de mo en lip values anders kunnen kiezen zodat deze varieren per taak.
        /// <summary>
        /// Set the mo and lip values of a task by adopting these values from the event it belongs to.
        /// </summary>
        /// <param name="task"></param>
        private void setMoAndLip(CTLTask task)
        {
            Console.WriteLine("Aantal active events: " + activeEvents.Count);
            
            foreach (CTLEvent ctlEvent in activeEvents)
            {
                Console.WriteLine(ctlEvent.ToString());
                if (ctlEvent.identifier.Equals(task.eventIdentifier))
                {
                    task.moValue = ctlEvent.moValue;
                    task.lipValue = ctlEvent.lipValue;
                }
            }

        }
    }
}
