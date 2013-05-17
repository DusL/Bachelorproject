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
    }
}
