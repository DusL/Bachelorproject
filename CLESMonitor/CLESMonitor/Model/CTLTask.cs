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
        public string type { get; private set; }
        private string eventIdentifier;
        public TimeSpan startTime { get; set; }
        public TimeSpan endTime { get; set; }
        public double moValue { get; set; } //mental occupancy
        public int lipValue { get; set; } //level of information processing
        public InformationDomain[] informationDomains { get; set; } //an array of enum representations of domains
        public string description { get; set; }

        //TODO: is deze property nog nodig?
        public bool isStarted { get; set; }

        public bool inProgress { get; set; }

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="_type"></param>
        public CTLTask(string _identifier, string _type)
        {
            identifier = _identifier;
            type = _type;
            isStarted = true;
        }

        public Object Clone()
        {
            CTLTask cloneTask = new CTLTask(this.identifier, this.type);
            cloneTask.eventIdentifier = this.eventIdentifier;
            // Structs are always copied on assignment
            cloneTask.startTime = this.startTime;
            cloneTask.endTime = this.endTime;
            cloneTask.moValue = this.moValue;
            cloneTask.lipValue = this.lipValue;
            cloneTask.informationDomains = (InformationDomain[]) this.informationDomains.Clone();

            cloneTask.description = this.description;
            cloneTask.isStarted = this.isStarted;
            cloneTask.inProgress = this.inProgress;

            return cloneTask;
        }

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
            return String.Format("Task: Identifier={0}, Type={1}, startTime={2}, endTime={3}", identifier, type, startTime.TotalSeconds, endTime.TotalSeconds);
        }
    }
}
