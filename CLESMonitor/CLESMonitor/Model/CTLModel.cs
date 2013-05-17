using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int sumlip = 0;
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
