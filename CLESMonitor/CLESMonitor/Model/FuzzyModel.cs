using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model
{
    class FuzzyModel : ESModel
    {
        private int heartRate; //in slagen/minuut?
        private int skinConductance; //in Ohm?

        public override double calculateModelValue()
        {
            // We genereren op dit moment random waarden
            Random random = new Random();
            return random.Next(5, 10);
        }
    }
}
