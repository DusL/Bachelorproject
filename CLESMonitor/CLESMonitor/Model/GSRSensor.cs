using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model
{
    /// <summary>
    /// The way in which the HRSensor will recieve its raw data
    /// </summary>
    public enum GSRSensorType
    {
        /// <summary>
        /// Initial value
        /// </summary>
        Unknown,
        /// <summary>
        /// By manual input, sensorValue is set from the outside
        /// </summary>
        ManualInput
    }

    public class GSRSensor
    {
        public GSRSensorType type;
        public double sensorValue; //conductance, in siemens
    }
}
