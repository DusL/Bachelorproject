using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model
{
    public abstract class CLModel
    {
        //Time frame in seconden
        public int lengthTimeFrame;

        /// <summary>
        /// Bereken opnieuw de model-waarde
        /// </summary>
        /// <returns>De model-waarde</returns>
        public abstract double calculateModelValue(DateTime time);
        
    }
}
