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
        private XMLFileTaskParser parser;
        private CTLDomain modelDomain;
        private Timer updateTimer;
        private DateTime startSessionTime;
        public TimeSpan sessionTime
        {
            get { return (DateTime.Now - startSessionTime); }
        }

        // Lists of events and tasks that are active right now
        public List<CTLEvent> activeEvents { get; private set; }
        public List<CTLTask> activeTasks { get; private set; }
        private bool activeTasksHaveChanged; 

        // List of tasks that are used for model calculation
        private List<CTLTask> tasksInCalculationFrame;

        /// <summary>
        /// The constructor method.
        /// </summary>
        /// <param name="parser">A XMLFileTaskParser to use as input for the model</param>
        public CTLModel(XMLFileTaskParser parser)
        {
            this.parser = parser;
            modelDomain = new PRLDomain();
            lengthTimeframe = new TimeSpan(0, 0, 10); //hours, minutes, seconds
            activeEvents = new List<CTLEvent>();
            activeTasks = new List<CTLTask>();
            tasksInCalculationFrame = new List<CTLTask>();
        }

        /// <summary>
        /// Starts a new session, calculateModelValue() will
        /// now produce valid values.
        public override void startSession()
        {
            Console.WriteLine("CTLModel.startSession()");
            startSessionTime = DateTime.Now;

            // Create and start a timer to update the model input values
            //FIXME, implementatie is afhankelijk van de frequentie!
            updateTimer = new Timer(updateTimerCallback, null, 0, 500);
        }

        /// <summary>
        /// Stops the current session.
        /// </summary>
        public override void stopSession()
        {
            Console.WriteLine("CTLModel.stopSession()");

            updateTimer.Dispose();
        }

        public void eventHasStarted(InputElement eventElement)
        {

        }

        public void eventHasStopped(InputElement eventElement)
        {

        }

        public void taskHasStarted(InputElement taskElement)
        {

        }

        public void taskHasStopped(InputElement taskElement)
        {

        }

        private void updateTimerCallback(Object stateInfo)
        {
            updateActiveEvents(sessionTime);
            updateActiveTasks(sessionTime);
            updateTasksInCalculationFrame(sessionTime);
        }

        public override double calculateModelValue()
        {
            // Calculate all necessary values
            double lip = calculateOverallLip(tasksInCalculationFrame);
            double mo = calculateOverallMo(tasksInCalculationFrame);
            double tss = calculateTSS(tasksInCalculationFrame);

            // TODO: For now, we generate random values
            Random random = new Random();

            return random.Next(0, 5);
        }

        private void updateActiveEvents(TimeSpan sessionTime)
        {
            // Proces events that have started
            List<ParsedEvent> parsedEventsStarted = parser.eventsStarted(sessionTime);
            List<CTLEvent> eventsStarted = modelDomain.generateEvents(parsedEventsStarted, sessionTime);
            activeEvents.AddRange(eventsStarted);

            // Update the 'end time' for all active events
            foreach (CTLEvent ctlEvent in activeEvents)
            {
                ctlEvent.endTime = sessionTime;
            }

            // Proces events that have stopped
            List<ParsedEvent> parsedEventsStopped = parser.eventsStopped(sessionTime);
            foreach (ParsedEvent parsedEvent in parsedEventsStopped)
            {
                activeEvents.Remove(getEventFromIdentifier(parsedEvent.identifier));
            }
        }

        private void updateActiveTasks(TimeSpan sessionTime)
        {
            activeTasksHaveChanged = false;

            // Proces the tasks that have started
            List<ParsedTask> parsedTasksStarted = parser.tasksStarted(sessionTime);
            if (parsedTasksStarted.Count > 0)
            {
                List<CTLTask> tasksStarted = modelDomain.generateTasks(parsedTasksStarted, sessionTime);
                activeTasks.AddRange(tasksStarted);
                activeTasksHaveChanged = true;
            }

            // Update the 'end time' for all active tasks
            foreach (CTLTask task in activeTasks)
            {
                task.endTime = sessionTime;
            }

            // Proces the tasks that have stopped
            List<ParsedTask> parsedTasksStopped = parser.tasksStopped(sessionTime);
            if (parsedTasksStopped.Count > 0)
            {
                foreach (ParsedTask parsedTask in parsedTasksStopped)
                {
                    activeTasks.Remove(getTaskFromIdentifier(parsedTask.identifier));
                }
                activeTasksHaveChanged = true;
            }
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

        //TODO: Opsplitsen task1,task2.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns>A new task; the multitask</returns>
        private CTLTask createMultitask(CTLTask task1, CTLTask task2)
        {
            //Creat a new CTLTask
            CTLTask multiTask = new CTLTask(task1.identifier + "+" + task2.identifier, task1.type + task2.type);
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
        /// Calculates the average normalized lip-values across the current time frame.
        /// </summary>
        /// <param name="tasks">A list of task that are currently in the timeframe</param>
        /// <returns>Average Lip-value (not rounded) </returns>
        private double calculateOverallLip(List<CTLTask> tasks)
        {
            int i = 0;
            double lipTimesDuration = 0;
            double sum = 0; 
            while (i != tasks.Count)
            {
                CTLTask t = (CTLTask)tasks[i];
                lipTimesDuration= t.lipValue * t.getDuration().TotalSeconds;
                sum += lipTimesDuration;
                i++;
            }

            //TODO: Afronden of niet?
            return lipTimesDuration/lengthTimeframe.TotalSeconds;
        }

        /// <summary>
        /// Calculates the average normalized mental occupancy waarde.
        /// </summary>
        /// <param name="tasks">A list of task that are currently in the timeframe</param>
        /// <returns>The normalized MO-value across 1 time frame </returns>
        private double calculateOverallMo(List<CTLTask> tasks)
        {
            int i = 0;
            double moTimesDuration = 0;
            double sum = 0;
            while (i != tasks.Count)
            {
                CTLTask t = (CTLTask)tasks[i];
                moTimesDuration = t.moValue * t.getDuration().TotalSeconds;
                sum += moTimesDuration;
                i++;
            }
            return moTimesDuration / lengthTimeframe.TotalSeconds;
        }

        //TODO: methode implementeren
        private double calculateTSS(List<CTLTask> tasks)
        { 
            return 0;
        }

        /// <summary>
        /// Gets the selected path from ViewController and sets that path for the XMLTaskParser
        /// </summary>
        /// <param name="filePath"></param>
        public override void setPathForParser(string filePath)
        {
            StreamReader streamReader = new StreamReader(File.Open(filePath, FileMode.Open));
            parser.loadTextReader(streamReader);
        }
    }
}
