using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model
{
    public abstract class ESModel
    {
        /// <summary>
        /// ReCalculate the model-value
        /// </summary>
        /// <returns>De model-waarde</returns>
        public abstract double calculateModelValue();

        public abstract void startSession();
    }
}
