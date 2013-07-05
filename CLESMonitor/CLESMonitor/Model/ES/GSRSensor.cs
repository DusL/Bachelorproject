using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model.ES
{
    /// <summary>
    /// The GSRSensor class represents a Galvanic Skin Response sensor. Also known as skin conductance,
    /// it can be used as a metric to measure emotional state.
    /// </summary>
    public class GSRSensor
    {
        /// <summary>
        /// The way in which the sensor will receive its raw data
        /// </summary>
        public enum Type
        {
            /// <summary>Initial unknown value</summary>
            Unknown,
            /// <summary>The sensor value is set from the outside (another class)</summary>
            ManualInput
        }

        /// <summary>The type of the sensor</summary>
        public Type type;
        /// <summary>The value of the sensor, expressed in siemens</summary>
        public double sensorValue;
    }
}
