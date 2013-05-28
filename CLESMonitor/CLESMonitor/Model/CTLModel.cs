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
        public XMLFileTaskParser parser;
        private ArrayList currentActiveTasks; 

        public CTLModel(XMLFileTaskParser parser)
        {
            modelDomain = new PRLDomain();
            lengthTimeFrame = 1;
            currentActiveTasks = new ArrayList();
            this.parser = parser;
        }

        public override double calculateModelValue(TimeSpan time)
        {
            //Requests a list of tasks that have started as well as a list of tasks that have stopped
            List<string> tasksBegan = parser.tasksBegan(time);
            List<string> tasksEnded = parser.tasksEnded(time);

            List<CTLTask> CTLtasksStartedThisSecond = getCTLTasksPerSecond(tasksBegan);
            List<CTLTask> CTLtasksEndedThisSecond = getCTLTasksPerSecond(tasksEnded);

            //Set the current time as start time for every new task
            foreach (CTLTask t in CTLtasksStartedThisSecond)
            {
                t.startTime = DateTime.Now;
            }

            //TODO: Je wilt eerst de taken aanpassen die er nog in staan. Dan pas dingen toevoegen.


            //Adds all newly started tasks to currentActiveTasks and sets the endtimes of these tasks
            
            currentActiveTasks.AddRange(CTLtasksStartedThisSecond);
            
            adjustStartTimes();
            adjustEndTimes();

            foreach (CTLTask task in CTLtasksEndedThisSecond)
            {
                //TODO: Vind de taks in de currentActiveTask lijst en set isStopped=true;
            }             


            //Calculate all necessary values
            double lip = calculateOverallLip(currentActiveTasks);
            double mo = calculateOverallMo(currentActiveTasks);
            double tss = calculateTSS(currentActiveTasks);


            //For now, we generate random values
            Random random = new Random();
            return random.Next(0, 5);


        }

        /// <summary>
        /// Set endtimes of tasks to be the same as their start time
        /// </summary>
        /// <param name="startedTasks"></param>
        private void adjustEndTimes()
        {
            foreach (CTLTask task in currentActiveTasks)
            {
                if (!task.isStopped)
                {
                    task.endTime = DateTime.Now;
                }
            }
        }


        //Adjusts the starting times to be equal to the start of the timeframe
        private void adjustStartTimes()
        {
            foreach (CTLTask task in currentActiveTasks)
            {
                if (task.startTime < startTimeFrame)
                {
                    task.startTime = startTimeFrame;
                }

            }
        }

        /// <summary>
        /// Create a list of CTLTasks based on a list of string identifiers
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns>A list of CTLTasks</returns>
        private List<CTLTask> getCTLTasksPerSecond(List<string> tasks)
        { 
            //Add all CTLTask objects to a list
            List<CTLTask> CTLtasks = new List<CTLTask>();
            if (tasks.Count != 0)
            {
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine();
                    CTLtasks.Add(modelDomain.getTaskByIdentifier((string)tasks[i]));
                }
            }
            return CTLtasks;
        }


        //TODO: Opsplitsen task1,task2.
        private CTLTask createMultitask(CTLTask task1, CTLTask task2)
        {
            //Creat a new CTLTask
            CTLTask newTask = new CTLTask(task1.getName() + task2.getName());
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
        private DateTime findStartTimeMultitask(CTLTask task1, CTLTask task2)
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
        private DateTime findEndTimeMultitask(CTLTask task1, CTLTask task2)
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
        private double calculateOverallLip(ArrayList tasks)
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
            return lipTimesDuration/lengthTimeFrame;
        }
        /// <summary>
        /// Calculates the average normalized mental occupancy waarde.
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns>The normalized MO-value across 1 time frame </returns>
        private double calculateOverallMo(ArrayList tasks)
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
            return moTimesDuration / lengthTimeFrame;
        }

        //TODO
        private double calculateTSS(ArrayList tasks)
        { 
            return 0;
        }

        public override void setPathForParser(string filePath)
        {
            parser.readPath(filePath);
        }
    }
}
