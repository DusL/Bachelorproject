using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CLESMonitor.Model
{
    class CTLModel : CLModel
    {
        private PRLDomain modelDomain;
        private XMLFileTaskParser parser;
        private List<CTLTask> currentActiveTasks; 

        public CTLModel(XMLFileTaskParser parser)
        {
            modelDomain = new PRLDomain();
            lengthTimeFrame = new TimeSpan(0, 0, 10); //uur, minuten, seconden
            currentActiveTasks = new List<CTLTask>();
            this.parser = parser;
        }

        public override double calculateModelValue(TimeSpan timeSpan)
        {
            // Request parsed data with tasks that have started as well as stopped
            List<ParsedTask> tasksBegan = parser.tasksStarted(timeSpan);
            List<ParsedTask> tasksEnded = parser.tasksStopped(timeSpan);
            // Use the domain to turn this into CTLTasks
            List<CTLTask> CTLtasksStartedThisSecond = getCTLTasksPerSecond(tasksBegan);
            List<CTLTask> CTLtasksEndedThisSecond = getCTLTasksPerSecond(tasksEnded);

            // Set the current time as start time for every new task and add it
            foreach (CTLTask t in CTLtasksStartedThisSecond)
            {
                t.startTime = timeSpan;
            }
            currentActiveTasks.AddRange(CTLtasksStartedThisSecond);

            // Proces the tasks that have ended
            // TODO: code werkt nog niet voor meerdere events
            foreach (CTLTask task1 in CTLtasksEndedThisSecond)
            {
                foreach (CTLTask task2 in currentActiveTasks)
                {
                    //TODO: werkt dit nu correct?
                    if (task1.identifier.Equals(task2.identifier))
                    {
                        task2.isStopped = true;
                    }
                }
            }

            adjustStartTimes(timeSpan);
            adjustEndTimes(timeSpan);
            this.clearOldTasks();

            // Calculate all necessary values
            double lip = calculateOverallLip(currentActiveTasks);
            double mo = calculateOverallMo(currentActiveTasks);
            double tss = calculateTSS(currentActiveTasks);

            // TODO: dit staat hier slechts voor debug
            foreach (CTLTask task in currentActiveTasks)
            {
                Console.WriteLine("Nieuwe seconde");
                Console.WriteLine(task.ToString());
            }

            // For now, we generate random values
            Random random = new Random();

            return random.Next(0, 5);
        }

        /// <summary>
        /// Set endtimes of tasks to be the same as their start time
        /// </summary>
        /// <param name="startedTasks"></param>
        private void adjustEndTimes(TimeSpan timeSpan)
        {
            foreach (CTLTask task in currentActiveTasks)
            {
                if (!task.isStopped)
                {
                    task.endTime = timeSpan;
                }
            }
        }

        /// <summary>
        /// Adjusts the starting times to be equal to the start of the timeframe
        /// </summary>
        /// <param name="timeSpan"></param>
        private void adjustStartTimes(TimeSpan timeSpan)
        {
            foreach (CTLTask task in currentActiveTasks)
            {
                if (task.startTime < (timeSpan - lengthTimeFrame))
                {
                    task.startTime = (timeSpan - lengthTimeFrame);
                }
            }
        }

        /// <summary>
        /// Delete all tasks that are completed and have dropped outside of the timeframe
        /// </summary>
        private void clearOldTasks()
        {
            List<CTLTask> tasksToRemove = new List<CTLTask>();
            foreach (CTLTask task in currentActiveTasks)
            {
                if (task.startTime > task.endTime)
                {
                    tasksToRemove.Add(task);
                }
            }
            foreach (CTLTask task in tasksToRemove)
            {
                currentActiveTasks.Remove(task);
            }
        }

        /// <summary>
        /// Create a list of CTLTasks based on a list of string identifiers
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns>A list of CTLTasks</returns>
        private List<CTLTask> getCTLTasksPerSecond(List<ParsedTask> tasks)
        { 
            //Add all CTLTask objects to a list
            List<CTLTask> CTLtasks = new List<CTLTask>();
            if (tasks.Count != 0)
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    CTLtasks.Add(modelDomain.getCTLTaskFromParsedTask(tasks[i]));
                }
            }
            return CTLtasks;
        }

        //TODO: Opsplitsen task1,task2.
        private CTLTask createMultitask(CTLTask task1, CTLTask task2)
        {
            //Creat a new CTLTask
            CTLTask newTask = new CTLTask(task1.identifier + "+" + task2.identifier, task1.getType() + task2.getType());
            //and set its values
            newTask.moValue = multitaskMO(task1, task2);
            newTask.lipValue = multitaskLip(task1, task2);
            newTask.informationDomains = multitaskDomain(task1, task2);
            newTask.duration = multitaskDuration(task1, task2);
            newTask.startTime = findStartTimeMultitask(task1, task2);
            newTask.endTime = findEndTimeMultitask(task1, task2);
            return newTask;
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
        /// 
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns>The duration of the multitask represented by a TimeSpan</returns>
        private double multitaskDuration(CTLTask task1, CTLTask task2)
        {
            TimeSpan duration = findEndTimeMultitask(task1, task2) - findStartTimeMultitask(task1, task2);
            return duration.TotalSeconds; 
        }

        /// <summary>
        /// Determines the starting time of a multitask by means of the start- and endtimes of two tasks.
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns>DateTime: starttime multitask</returns>
        private TimeSpan findStartTimeMultitask(CTLTask task1, CTLTask task2)
        {
            if (task1.startTime < task2.startTime)
            {
                return task2.startTime;
            }
            return task1.startTime;
        }

        /// <summary>
        /// Determines the end time of a multitask by means of the start- and endtimes of two tasks.
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns>DateTime: endtime multitask</returns>
        private TimeSpan findEndTimeMultitask(CTLTask task1, CTLTask task2)
        {
            if (task1.endTime < task2.endTime)
            {
                return task1.endTime;
            }
            return task2.endTime;
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
                lipTimesDuration= t.lipValue * t.duration;
                sum += lipTimesDuration;
                i++;
            }

            //TODO: Afronden of niet?
            return lipTimesDuration/lengthTimeFrame.TotalSeconds;
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
                moTimesDuration = t.moValue * t.duration;
                sum += moTimesDuration;
                i++;
            }
            return moTimesDuration / lengthTimeFrame.TotalSeconds;
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
            parser.readPath(filePath);
        }
    }
}
