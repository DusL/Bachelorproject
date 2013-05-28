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
        private string type;
        private string eventIdentifier;
        public int lipValue { get; set; } //level of information processing
        public double moValue { get; set; } //mental occupancy
        public InformationDomain[] informationDomains { get; set; } //an array of enum representations of domains
        public double duration { get; set; } //in seconds
        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }
        public string description { get; set; }
        public bool isStopped { get; set; }
        public string identifier { get; set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="_type"></param>
        public CTLTask(string _type, string _identifier)
        {
            type = _type;
            isStopped = false;
            identifier = _identifier;
        }

        public string getType()
        {
            return this.type;
        }

        /// <summary>
        /// ToString method
        /// </summary>
        /// <returns>A string-representation of the CTLTask object</returns>
        public override string ToString()
        {
            return String.Format("Name = {0}, startTime = {1}, endTime = {2}", type, startTime.TotalSeconds, endTime.TotalSeconds);
        }
    }
}
