using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLESMonitor.Model;

namespace CLESMonitor.Model.CL
{
    public class CTLTask : Object, ICloneable
    {
        public string identifier { get; private set; }
        public string name { get; private set; }
        //public string eventIdentifier { get;  set; }
        /// <summary>A short description of the task</summary>
        public string description { get; set; }

        /// <summary>A CTLTask can be performed in the context of a CTLEvent. If so, this will be set.</summary>
        public CTLEvent ctlEvent { get; set; }

        /// <summary>The starting time of the task</summary>
        public TimeSpan startTime { get; set; }
        /// <summary>Denotes whether the task is currently in progress</summary>
        public bool inProgress { get; set; }
        /// <summary>The ending time of the task</summary>
        public TimeSpan endTime { get; set; }
        /// <summary>The duration of the task</summary>
        public TimeSpan duration { get { return endTime - startTime; } }

        /// <summary>Mental Occupancy value</summary>
        public double moValue { get; set; }
        /// <summary>Level of Information Processing value</summary>
        public int lipValue { get; set; }
        /// <summary>The information domains this task belongs to</summary>
        public List<int> informationDomains { get; set; }

        /// <summary>
        /// Constructor method.
        /// </summary>
        /// <param name="identifier">The identifier for this task</param>
        /// <param name="name">The name, which is used as a short description</param>
        /// <param name="eventIdentifier">The identifier of the event linked to this task</param>
        public CTLTask(string identifier, string name)
        {
            this.identifier = identifier;
            this.name = name;
            this.moValue = -1;
            this.lipValue = 0;
        }

        /// <summary>
        /// Returns a clone of the task.
        /// </summary>
        /// <returns>The clone object</returns>
        public Object Clone()
        {
            // NOTE: Structs are always copied on assignment

            CTLTask clone = new CTLTask(identifier, name);

            clone.description = this.description;
            clone.ctlEvent = this.ctlEvent;

            clone.startTime = this.startTime;
            clone.inProgress = this.inProgress;
            clone.endTime = this.endTime;

            clone.moValue = this.moValue;
            clone.lipValue = this.lipValue;
            clone.informationDomains = this.informationDomains;

            return clone;
        }

        /// <summary>
        /// ToString method
        /// </summary>
        /// <returns>A string-representation of the CTLTask object</returns>
        public override string ToString()
        {
            return String.Format("Task: Identifier={0}, startTime={1}, endTime={2}, moValue={3}, lipValue={4}, Type={5}",
                identifier, startTime.TotalSeconds, endTime.TotalSeconds, moValue, lipValue, name);
        }
    }
}
