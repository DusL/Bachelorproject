using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace CLESMonitor.Model
{
    public class CTLModel : CLModel
    {
        private PRLDomain modelDomain;
        private XMLFileTaskParser parser;

        private List<CTLTask> tasksInTimeframe;
        private List<CTLTask> currentStartedTasks;

        private List<CTLEvent> currentEvents;

        public CTLModel(XMLFileTaskParser parser)
        {
            modelDomain = new PRLDomain();
            lengthTimeframe = new TimeSpan(0, 0, 10); //hours, minutes, seconds
            tasksInTimeframe = new List<CTLTask>();
            currentStartedTasks = new List<CTLTask>();
            currentEvents = new List<CTLEvent>();
            this.parser = parser;
        }

        public override double calculateModelValue(TimeSpan currentSessionTime)
        {
            // TODO: code werkt nog niet voor meerdere events
            // Proces the event that have started
            List<ParsedEvent> eventsBegan = parser.eventsStarted(currentSessionTime);
            List<CTLEvent> eventsStartedThisSecond = generateEvents(eventsBegan, currentSessionTime);
            currentEvents.AddRange(eventsStartedThisSecond);

            // Proces the tasks that have started
            List<ParsedTask> tasksBegan = parser.tasksStarted(currentSessionTime);
            List<CTLTask> tasksStartedThisSecond = generateTasks(tasksBegan, currentSessionTime);
            currentStartedTasks.AddRange(tasksStartedThisSecond);
            //TODO: verwerken naar tasksInTimeframe!

            // Update all task times
            updateTaskTimes(currentSessionTime);

            // Proces the tasks that have ended
            List<ParsedTask> tasksEnded = parser.tasksStopped(currentSessionTime);
            List<CTLTask> tasksEndedThisSecond = new List<CTLTask>();
            foreach (ParsedTask parsedTask in tasksEnded)
            {
                tasksEndedThisSecond.Add(getTaskFromIdentifier(parsedTask.identifier));
            }
            foreach (CTLTask task in tasksEndedThisSecond)
            {
                task.isStarted = false;
            }
            clearOldTasks();

            // Proces the events that have ended
            List<ParsedEvent> eventsEnded = parser.eventsStopped(currentSessionTime);
            List<CTLEvent> eventsStoppedThisSecond = new List<CTLEvent>();
            foreach (ParsedEvent parsedEvent in eventsEnded)
            {
                eventsStoppedThisSecond.Add(getEventFromIdentifier(parsedEvent.identifier));
            }
            foreach (CTLEvent ctlEvent in eventsStoppedThisSecond)
            {
                currentEvents.Remove(ctlEvent);
            }

            //TODO: multitasking implementeren

            // Calculate all necessary values
            double lip = calculateOverallLip(tasksInTimeframe);
            double mo = calculateOverallMo(tasksInTimeframe);
            double tss = calculateTSS(tasksInTimeframe);

            //TODO: dit staat hier slechts voor debug
            foreach (CTLTask task in currentStartedTasks)
            {
                Console.WriteLine("Nieuwe seconde");
                Console.WriteLine(task.ToString());
            }
            foreach (CTLEvent ctlEvent in currentEvents)
            {
                Console.WriteLine(ctlEvent.ToString());
            }

            // For now, we generate random values
            Random random = new Random();

            return random.Next(0, 5);
        }

        private CTLTask getTaskFromIdentifier(string identifier)
        {
            CTLTask taskToReturn = null;
            foreach (CTLTask task in currentStartedTasks)
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
            foreach (CTLEvent ctlEvent in currentEvents)
            {
                if (ctlEvent.identifier.Equals(identifier))
                {
                    eventToReturn = ctlEvent;
                }
            }
            return eventToReturn;
        }

        /// <summary>
        /// Updates start- and endtimes of tasks
        /// </summary>
        /// <param name="startedTasks"></param>
        private void updateTaskTimes(TimeSpan timeSpan)
        {
            foreach (CTLTask task in tasksInTimeframe)
            {
                // As long as a task is still active, update the endTime
                if (task.isStarted)
                {
                    task.endTime = timeSpan;
                }

                // When the startTime of a task moves outside of the timeframe, crop it
                if (task.startTime < (timeSpan - lengthTimeframe))
                {
                    task.startTime = (timeSpan - lengthTimeframe);
                }
            }
        }

        /// <summary>
        /// Delete all tasks that are completed and have dropped outside of the timeframe
        /// </summary>
        private void clearOldTasks()
        {
            List<CTLTask> tasksToRemove = new List<CTLTask>();
            foreach (CTLTask task in tasksInTimeframe)
            {
                if (task.startTime > task.endTime)
                {
                    tasksToRemove.Add(task);
                }
            }
            foreach (CTLTask task in tasksToRemove)
            {
                tasksInTimeframe.Remove(task);
            }
        }

        /// <summary>
        /// Create a list of CTLEvent based on a list of string identifiers
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns>A list of CTLEvent</returns>
        private List<CTLEvent> generateEvents(List<ParsedEvent> parsedEvents, TimeSpan currentSessionTime)
        {
            // Add all CTLEvent objects to a list
            List<CTLEvent> events = new List<CTLEvent>();

            foreach (ParsedEvent parsedEvent in parsedEvents)
            {
                CTLEvent ctlEvent = modelDomain.generateEvent(parsedEvent);
                ctlEvent.startTime = currentSessionTime;
                events.Add(ctlEvent);
            }

            return events;
        }

        /// <summary>
        /// Create a list of CTLTasks based on a list of string identifiers
        /// </summary>
        /// <param name="parsedTasks"></param>
        /// <returns>A list of CTLTasks</returns>
        private List<CTLTask> generateTasks(List<ParsedTask> parsedTasks, TimeSpan currentSessionTime)
        { 
            // Add all CTLTask objects to a list
            List<CTLTask> tasks = new List<CTLTask>();

            foreach (ParsedTask parsedTask in parsedTasks)
            {
                CTLTask task = modelDomain.generateTask(parsedTask);
                task.startTime = currentSessionTime;
                tasks.Add(task);
            }

            return tasks;
        }

        //TODO: Opsplitsen task1,task2.
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
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns>An array of informationDomains</returns>
        private InformationDomain[] multitaskDomain(CTLTask task1, CTLTask task2)
        {
            InformationDomain[] newDomain = task1.informationDomains;
            InformationDomain[] tempDomain = task2.informationDomains;
            for (int i = 0; i <= tempDomain.Length - 1; i++)
            {
                if (Array.IndexOf(newDomain, tempDomain[i]) == -1)
                {
                    Array.Resize(ref newDomain, newDomain.Length + 1);
                    newDomain[newDomain.Length] = tempDomain[i];
                }
            }
            return newDomain;
        }

        /// <summary>
        /// Sets the MO-value of a multitaks to be the sum of the MO-values of the original tasks when this sum is greater than 1.
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns>A double representing the MO value of the new multitask</returns>
        private double multitaskMO(CTLTask task1, CTLTask task2)
        {
            double MO1 = task1.moValue;
            double MO2 = task2.moValue;
            return Math.Max(MO1 + MO2, 1); ;
        }

        /// <summary>
        /// Sets the Lip-value of a multitask: the largest of the two lip-values of the original tasks
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
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
        /// <param name="task1"></param>
        /// <param name="task2"></param>
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
        /// <param name="tasks"></param>
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
        /// <param name="tasks"></param>
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
