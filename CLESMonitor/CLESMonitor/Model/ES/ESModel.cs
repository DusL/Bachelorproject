using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model.ES
{
    /// <summary>
    /// This abstract class represents an Emotional State model.
    /// The method calculateModelValue() is the core method that will return
    /// calculated values from the model.
    /// </summary>
    public abstract class ESModel
    {
        /// <summary>
        /// Starts a new session, calculateModelValue() will
        /// now produce valid values.
        /// </summary>
        public abstract void startSession();

        /// <summary>
        /// Stops the current session.
        /// </summary>
        public abstract void stopSession();

        /// <summary>
        /// (Re)calculates the model value
        /// </summary>
        /// <returns>The model value</returns>
        public abstract double calculateModelValue();

        /// <summary>
        /// Start a calibration session.
        /// </summary>
        public abstract void startCalibration();

        /// <summary>
        /// Stops the current calibration session.
        /// </summary>
        public abstract void stopCalibration();
    }
}
