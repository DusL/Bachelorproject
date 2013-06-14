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
        public string eventIdentifier { get;  set; }
        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }
        public double moValue { get; set; } //mental occupancy
        public int lipValue { get; set; } //level of information processing

        /// <summary>A list of integers representing different information domains.</summary>
        public List<int> informationDomains { get; set; }

        public string description { get; set; }

        public bool inProgress { get; set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="_name"></param>
        public CTLTask(string identifier, string name, string eventIdentifier)
        {
            this.identifier = identifier;
            this.name = name;
            this.eventIdentifier = eventIdentifier;
            this.moValue = -1;
            this.lipValue = 0;
        }

        /// <summary>
        /// Implementation of the clone methode
        /// </summary>
        /// <returns>The clopne (copy) of a CTLTask</returns>
        public Object Clone()
        {
            CTLTask cloneTask = new CTLTask(identifier, name, eventIdentifier);
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
            return String.Format("Task: Identifier={0}, Type={1}, startTime={2}, endTime={3}, eventID={4}, moValue={5}, lipValue={6}", identifier, name, startTime.TotalSeconds, endTime.TotalSeconds, eventIdentifier, moValue, lipValue);
        }
    }
}
