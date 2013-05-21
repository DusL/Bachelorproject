using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.

namespace CLESMonitor.Model
{
    class CTLModel : CLModel
    {
        private PRLDomain modelDomain;

        public CTLModel()
        {
            modelDomain = new PRLDomain();
            lengthTimeFrame = 1;
        }

        public override double calculateModelValue()
        {
            // We genereren op dit moment random waarden
            Random random = new Random();
            return random.Next(0, 5);
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
        /// <returns></returns>
        public int multitaskLip(CTLTask task1, CTLTask task2)
        {
            int Lip1 = task1.getLip();
            int Lip2 = task2.getLip();
            return Math.Max(Lip1,Lip2);
        }
        
        //TODO: Maakt gebruik van de start en eind tijden van taken.
        //Deze zijn nu echter nog niet gedefinieerd.
        public double multitaskDuration(CTLTask task1, CTLTask task2)
        {
            return 0;
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
    }
}
