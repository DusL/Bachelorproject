using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model
{
    abstract class ESModel
    {
        /// <summary>
        /// Bereken opnieuw de model-waarde
        /// </summary>
        /// <returns>De model-waarde</returns>
        public abstract int calculateModelValue();
    }
}
