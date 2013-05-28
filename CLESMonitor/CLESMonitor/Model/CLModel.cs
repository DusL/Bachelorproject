using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model
{
    public abstract class CLModel
    {
        //Time frame in seconds
        public int lengthTimeFrame;
        public DateTime startTimeFrame;
        public DateTime endTimeFrame;

        /// <summary>
        /// Recalculate model-value
        /// </summary>
        /// <returns>The model-waarde</returns>
        public abstract double calculateModelValue(TimeSpan time);
        public abstract void setPathForParser(string filePath);
        //public abstract string getLog();
        
    }
}
