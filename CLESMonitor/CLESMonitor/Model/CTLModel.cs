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

        public CTLModel()
        {
            modelDomain = new PRLDomain();
            lengthTimeFrame = 1;
            //parser = new XMLFileTaskParser(@"D:\vvandertas\Dropbox\Bachelorproject\XMLFile1.xml");
            currentActiveTasks = new ArrayList();
        }

        public override double calculateModelValue(TimeSpan time)
        {
            

            //En splitst deze in events en tasks

            ArrayList tasksBegan = parser.tasksBegan(time);
            ArrayList tasksEnded = parser.tasksEnded(time);
            Console.WriteLine("Hier moet iets komen "+ String.Join(",", tasksBegan));
            Console.WriteLine("Hier moet iets komen " + String.Join(",", tasksEnded));
            ArrayList CTLtasksStartedThisSecond = getCTLTasksPerSecond(tasksBegan);
            currentActiveTasks.AddRange(CTLtasksStartedThisSecond);


            //Bereken alle benodigde waarden
            /*double lip = calculateOverallLip(CTLtasks);
            double mo = calculateOverallMo(CTLtasks);
            double tss = calculateTSS(CTLtasks);*/


            // We genereren op dit moment nog random waarden
            Random random = new Random();
            return random.Next(0, 5);


        }
        public ArrayList getActions(TimeSpan time) 
        {
            //Haal voor de huidige seconde alle gebeurtenissen binnen
            
            //int sec = time.Minute * 60 + time.Second;
            return parser.getActionsForSecond((int)Math.Floor(time.TotalSeconds));
        }
     


        /// <summary>
        /// Maak van een array van string identifiers een ArrayList van CTLtasks
        /// </summary>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public ArrayList getCTLTasksPerSecond(ArrayList tasks)
        { 
            //Zet alle CTLTask objecten in een array
            ArrayList CTLtasks = new ArrayList();
            if (tasks.Count != 0)
            {
                for (int i = 0; i <= tasks.Count-1; i++)
                {
                    //string identifier = parser.taskIdentifier(tasks[i]);
                    CTLtasks.Add(modelDomain.getTaskByIdentifier((string)tasks[i]));
                }
            }
            return CTLtasks;
        }
        //TODO: Wat doen we met de naamgeving in de constructor van de nieuwe taak?
        //TODO: Opsplitsen task1,task2.
        public CTLTask createMultitask(CTLTask task1, CTLTask task2)
        {
            CTLTask newTask = new CTLTask("MultiTask");
            newTask.setMO(multitaskMO(task1, task2));
            newTask.setLip(multitaskLip(task1, task2));
            newTask.setInformationDomain(multitaskDomain(task1, task2));
            //Krijgen nu TimeSpan binnen maar willen double hebben.
            /*newTask.setDuration(multitaskDuration(task1, task2));
            newTask.setEndTime(findEndTimeMultitask);
            newTask.setStartTime(findStartTimeMultitask);*/
            return newTask;

        }
        /// <summary>
        /// Stelt de array van domeinen van een nieuwe taak gelijk aan de domeinen van task1, task2 gecombineerd.
        /// Hierbij worden overlappende domeinen slechts 1 maal in de nieuwe array opgenomen
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns></returns>
        public InformationDomain[] multitaskDomain(CTLTask task1, CTLTask task2)
        {
            InformationDomain[] newDomain = task1.getInfoDomain();
            InformationDomain[] tempDomain = task2.getInfoDomain();
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
        /// <returns></returns>
        public double multitaskMO(CTLTask task1, CTLTask task2)
        {
            double MO1 = task1.getMO();
            double MO2 = task2.getMO();
            return Math.Max(MO1 + MO2, 1); ;
        }
        /// <summary>
        /// Stelt de Lip-waarde van een multitask taak gelijk aan de grootste van de twee lip-waarden van 
        /// de oorspronkelijke taken.
        /// </summary>
        /// <param name="task1"></param>
        /// <param name="task2"></param>
        /// <returns>Een nieuwe Lip waarde voor een nieuwe taak</returns>
        public int multitaskLip(CTLTask task1, CTLTask task2)
        {
            int Lip1 = task1.getLip();
            int Lip2 = task2.getLip();
            return Math.Max(Lip1,Lip2);
        }
        
        //TODO: Maakt gebruik van de start en eind tijden van taken.
        //Deze zijn nu echter nog niet gedefinieerd.
        public TimeSpan multitaskDuration(CTLTask task1, CTLTask task2)
        {
            TimeSpan duration = findEndTimeMultitask(task1, task2) - findStartTimeMultitask(task1, task2);
            return duration;
        }
        
        public DateTime findStartTimeMultitask(CTLTask task1, CTLTask task2)
        {
            if (task1.startTime < task2.startTime)
            {
                return task2.startTime;
            }
            return task1.startTime;
        }

        public DateTime findEndTimeMultitask(CTLTask task1, CTLTask task2)
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
        public double calculateOverallLip(CTLTask[] tasks)
        {
            int i = 0;
            double lipTimesDuration = 0;
            double sum = 0; 
            while (i != tasks.Length)
            {
                lipTimesDuration=tasks[i].getLip() * tasks[i].getDuration();
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
        public double calculateOverallMo(CTLTask[] tasks)
        {
            int i = 0;
            double moTimesDuration = 0;
            double sum = 0;
            while (i != tasks.Length)
            {
                moTimesDuration = tasks[i].getMO() * tasks[i].getDuration();
                sum += moTimesDuration;
                i++;
            }
            return moTimesDuration / lengthTimeFrame;
        }

        //TODO
        public double calculateTSS(CTLTask[] tasks)
        { 
            return 0;
        }
    }
}
