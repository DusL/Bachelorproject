using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model.CL
{
    /// <summary>
    /// Represents an event in the context of the CTLModel. An CTLEvent can be
    /// associated with multiple CTLTasks.
    /// </summary>
    public class CTLEvent
    {
        public string identifier {get; private set;}
        public string name { get; private set; }
        /// <summary>The mental ocupancy value</summary>
        public double moValue { get; set; }
        /// <summary>The Level of Informationprocessing </summary>
        public int lipValue { get; set; }   
        public TimeSpan startTime { get; private set; }
        public bool inProgress { get; private set; }
        public TimeSpan endTime { get; private set; }

        /// <summary>
        /// Constructor method.
        /// </summary>
        /// <param name="identifier">The unique id of the task</param>
        /// <param name="name">The name of the task (not unique per se)</param>
        /// <param name="moValue">The mental occupancy value of the task</param>
        /// <param name="lipValue">The Level of Informationprocessing for the task</param>
        public CTLEvent(string identifier, string name, double moValue, int lipValue)
        {
            this.identifier = identifier;
            this.name = name;
            this.moValue = moValue;
            this.lipValue = lipValue;
            this.inProgress = false;
        }

        /// <summary>
        /// Starts the event.
        /// </summary>
        /// <param name="startTime">The starting time</param>
        public void startEvent(TimeSpan startTime)
        {
            inProgress = true;
            this.startTime = startTime;
        }

        /// <summary>
        /// Stops the event.
        /// </summary>
        /// <param name="endTime">The ending time</param>
        public void stopEvent(TimeSpan endTime)
        {
            inProgress = false;
            this.endTime = endTime;
        }

        /// <summary>
        /// Returns a string representation of the event.
        /// </summary>
        /// <returns>The string representation</returns>
        public override string ToString()
        {
            return String.Format("Event: Identifier={0}, Type={1}, startTime={2}, endTime={3}, moValue={4}, lipValue={5}", identifier, name, startTime.TotalSeconds, endTime.TotalSeconds, moValue, lipValue);
        }
    }
}
