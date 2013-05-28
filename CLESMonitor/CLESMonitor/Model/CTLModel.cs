using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

//using System.

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
            //Vraagt een lijst van taken die begonnen en een lijst van taken die gestopt zijn op.
            List<string> tasksBegan = parser.tasksBegan(time);
            List<string> tasksEnded = parser.tasksEnded(time);

            List<CTLTask> CTLtasksStartedThisSecond = getCTLTasksPerSecond(tasksBegan);
            List<CTLTask> CTLtasksEndedThisSecond = getCTLTasksPerSecond(tasksEnded);

            //Stel voor iedere niew binnengekomen taak de huidige tijd in als start tijd
            foreach (CTLTask t in CTLtasksStartedThisSecond)
            {
                t.startTime = DateTime.Now;
            }

            //TODO: Je wilt eerst de taken aanpassen die er nog in staan. Dan pas dingen toevoegen.


            //Voegt alle gestarte taken toe aan de currentActiveTasks lijst en stelt de eindtijden van deze taken in
            
            currentActiveTasks.AddRange(CTLtasksStartedThisSecond);
            
            adjustStartTimes();
            adjustEndTimes();

            foreach (CTLTask task in CTLtasksEndedThisSecond)
            {
                //Vind de taks in de currentActiveTask lijst en set isStopped=true;
            }             


            //Bereken alle benodigde waarden
            double lip = calculateOverallLip(currentActiveTasks);
            double mo = calculateOverallMo(currentActiveTasks);
            double tss = calculateTSS(currentActiveTasks);


            // We genereren op dit moment nog random waarden
            Random random = new Random();
            return random.Next(0, 5);


        }

        /// <summary>
        /// Stel de eindtijden van de taken gelijk aan de starttijd van de taken.
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


        //Past de starttijden aan op het begin van het timeframe
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
        /// Maak van een array van string identifiers een ArrayList van CTLtasks
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns>Een arrayList van CTLTasks</returns>
        private List<CTLTask> getCTLTasksPerSecond(List<string> tasks)
        { 
            //Zet alle CTLTask objecten in een array
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
            //Maak een nieuwe CTLTask
            CTLTask newTask = new CTLTask(task1.getName() + task2.getName());
            //En set zijn waarden
            newTask.moValue = multitaskMO(task1, task2);
            newTask.lipValue = multitaskLip(task1, task2);
            newTask.informationDomains = multitaskDomain(task1, task2);
            newTask.duration = multitaskDuration(task1, task2);
            newTask.startTime = findStartTimeMultitask(task1, task2);
            newTask.endTime = findEndTimeMultitask(task1, task2);
            return newTask;
        }
        /// <summary>
        /// Stelt de array van domeinen van een nieuwe taak gelijk aan de domeinen van task1, task2 gecombineerd.
        /// Hierbij worden overlappende domeinen slechts 1 maal in de nieuwe array opgenomen
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns>Een array van informationDomains</returns>
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
        /// Stelt de MO-waarde voor een multitask taak gelijk aan het de som van de MO-waarden van de oorspronkelijke taken
        /// als deze groter is dan 1.
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns>Een double die de MO waarde van de nieuwe multitaks taak representeert</returns>
        private double multitaskMO(CTLTask task1, CTLTask task2)
        {
            double MO1 = task1.moValue;
            double MO2 = task2.moValue;
            return Math.Max(MO1 + MO2, 1); ;
        }
        /// <summary>
        /// Stelt de Lip-waarde van een multitask taak gelijk aan de grootste van de twee lip-waarden van 
        /// de oorspronkelijke taken.
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns>Een nieuwe Lip waarde voor een nieuwe taak</returns>
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
        /// <returns>De duration van de nieuwe multitask in de vorm van een TimeSpan</returns>
        private double multitaskDuration(CTLTask task1, CTLTask task2)
        {
            TimeSpan duration = findEndTimeMultitask(task1, task2) - findStartTimeMultitask(task1, task2);
            return duration.TotalSeconds; 
        }
        /// <summary>
        /// Bepaald aan de hand van de start en eindtijden van twee taken vanaf welk moment
        /// ze overlappen. Dat punt is de start tijd van de multitask
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns>DateTime: starttijd multitask</returns>
        private DateTime findStartTimeMultitask(CTLTask task1, CTLTask task2)
        {
            if (task1.startTime < task2.startTime)
            {
                return task2.startTime;
            }
            return task1.startTime;
        }
        /// <summary>
        /// Bepaald aan de hand van de start en eindtijden van twee taken vanaf welk moment
        /// ze niet meer overlappen. Dat punt is de eindtijd van de multitask
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns>DateTime: eindtijd multitask</returns>
        private DateTime findEndTimeMultitask(CTLTask task1, CTLTask task2)
        {
            if (task1.endTime < task2.endTime)
            {
                return task1.endTime;
            }
            return task2.endTime;
        }

        /// <summary>
        /// Berekent de gemiddelde genormaliseerde lip-waarde over het huidige time frame.
        /// Hiervoor wordt eerst gemiddeld over duration en vervolgens het totaal gedeeld door 
        /// de lengte van het huidige time frame.
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns>Gemiddelde Lip waarde (onafgerond) </returns>
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
        /// Berekent de gemiddelde genormaliseerde mental occupancy waarde.
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns>De genormaliseerde MO-waarde over 1 time frame </returns>
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
