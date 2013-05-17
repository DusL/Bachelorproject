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
        }

        public override double calculateModelValue()
        {
            // We genereren op dit moment random waarden
            Random random = new Random();
            return random.Next(0, 5);
        }
    }
}
