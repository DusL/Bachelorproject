using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLESMonitor.Model;

namespace CLESMonitor.Model
{
    public class CTLTask : ICloneable
    {
        public string identifier {get; private set;}
        public string name { get; private set; }
        private string eventIdentifier;
        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }
        public double moValue { get; set; } //mental occupancy
        public int lipValue { get; set; } //level of information processing
        /// <summary>
        /// A list of integers representing different information domains.
        /// </summary>
        public List<int> informationDomains { get; set; } //an array of enum representations of domains
        public string description { get; set; }

        public bool inProgress { get; set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="_name"></param>
        public CTLTask(string _identifier, string _name)
        {
            identifier = _identifier;
            name = _name;
        }

        /// <summary>
        /// Implementation of the clone methode
        /// </summary>
        /// <returns>The clopne (copy) of a CTLTask</returns>
        public Object Clone()
        {
            CTLTask cloneTask = new CTLTask(this.identifier, this.name);
            cloneTask.eventIdentifier = this.eventIdentifier;
            // Structs are always copied on assignment
            cloneTask.startTime = this.startTime;
            cloneTask.endTime = this.endTime;
            cloneTask.moValue = this.moValue;
            cloneTask.lipValue = this.lipValue;
            cloneTask.informationDomains = this.informationDomains;
            cloneTask.description = this.description;
            cloneTask.inProgress = this.inProgress;

            return cloneTask;
        }
        /// <summary>
        /// Calculates the duration of a taks, using its start- and endtime
        /// </summary>
        /// <returns></returns>
        public TimeSpan getDuration()
        {
            return endTime - startTime;
        }

        /// <summary>
        /// ToString method
        /// </summary>
        /// <returns>A string-representation of the CTLTask object</returns>
        public override string ToString()
        {
            return String.Format("Task: Identifier={0}, Type={1}, startTime={2}, endTime={3}", identifier, name, startTime.TotalSeconds, endTime.TotalSeconds);
        }
    }
}
