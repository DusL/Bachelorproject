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
        public DateTime startTimeFrame;
        public DateTime endTimeFrame;

        /// <summary>
        /// Bereken opnieuw de model-waarde
        /// </summary>
        /// <returns>De model-waarde</returns>
        public abstract double calculateModelValue(TimeSpan time);
        public abstract void setPathForParser(string filePath);
        //public abstract string getLog();
        
    }
}
