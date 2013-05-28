using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLESMonitor.Model;

namespace CLESMonitor.Model
{
    public class CTLTask
    {
        private string name;
        public int lipValue { get; set; } //level of information processing
        public double moValue { get; set; } //mental occupancy
        public InformationDomain[] informationDomains { get; set; } //an array of enum representations of domains
        public double duration { get; set; } //in seconds
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string description { get; set; }
        public bool isStopped { get; set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="_name"></param>
        public CTLTask(string _name)
        {
            name = _name;
            isStopped = false;
        }

        public string getName()
        {
            return this.name;
        }

        /// <summary>
        /// ToString method
        /// </summary>
        /// <returns>A string-representation of the CTLTask object</returns>
        public string toString()
        {
            return String.Format("Name = {0}", name);
        }
    }
}
