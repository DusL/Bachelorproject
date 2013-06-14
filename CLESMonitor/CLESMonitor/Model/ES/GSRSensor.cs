using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model.ES
{
    /// <summary>
    /// The way in which the sensor will receive its raw data
    /// </summary>
    public enum GSRSensorType
    {
        /// <summary>Initial unknown value</summary>
        Unknown,
        /// <summary>The sensor value is set from the outside (another class)</summary>
        ManualInput
    }

    /// <summary>
    /// The GSRSensor class represents a Galvanic Skin Response sensor. Also known as skin conductance,
    /// it can be used as a metric to measure emotional state.
    /// </summary>
    public class GSRSensor
    {
        /// <summary>The type of the sensor</summary>
        public GSRSensorType type;
        /// <summary>The value of the sensor, expressed in siemens</summary>
        public double sensorValue;
    }
}
