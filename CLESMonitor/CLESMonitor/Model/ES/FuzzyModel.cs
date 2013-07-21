using CLESMonitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CLESMonitor.Model.ES
{
    /// <summary>
    /// This class is an implementation of an Emotional State model
    /// using Fuzzy Logic.
    /// </summary>
    public class FuzzyModel : ESModel
    {
        /// <summary>
        /// Represents a level of Galvanic Skin Response.
        /// </summary>
        public enum GSRLevel
        {
            /// <summary>Default unknown value</summary>
            Unknown,
            /// <summary>A low level of skin conductance</summary>
            Low,
            /// <summary>A low to medium level of skin conductance</summary>
            MidLow,
            /// <summary>A medium to high level of skin conductance</summary>
            MidHigh,
            /// <summary>A high level of skin conductance</summary>
            High
        }

        /// <summary>
        /// Represents a Heart Rate level.
        /// </summary>
        public enum HRLevel
        {
            /// <summary>Default unknown value</summary>
            Unknown,
            /// <summary>A low heart rate</summary>
            Low,
            /// <summary>A medium heart rate</summary>
            Mid,
            /// <summary>A high heart rate</summary>
            High
        }

        /// <summary>
        /// Represents a level of arousal.
        /// </summary>
        public enum ArousalLevel
        {
            /// <summary>Default unknown value</summary>
            Unknown,
            Low,
            MidLow,
            MidHigh,
            High
        }

        /// <summary>The Heart Rate sensor object</summary>
        public HRSensor hrSensor { get; private set; } 
        /// <summary>The Galvanic Skin Response sensor object</summary>
        public GSRSensor gsrSensor { get; private set; }

        // Data from calibration periode
        private Timer calibrationTimer;
        public List<double> calibrationHR; //in beats/minute
        public List<double> calibrationGSR; //in siemens
        public double HRMax, HRMin;
        public double GSRMax, GSRMin;
        public double GSRMean, HRMean;
        public double GSRsd, HRsd; 

        // Current values
        private double currentHR;
        private double currentGSR;

        // Current normalised values
        private double normalisedGSR;
        private double normalisedHR;

        /// <summary>The current Galvanic Skin Response sensor level</summary>
        public GSRLevel gsrLevel;
        /// <summary>The current Heart Rate sensor level</summary>
        public HRLevel hrLevel;

        /// <summary>
        /// Constructor method that sets the sensors immediately
        /// </summary>
        public FuzzyModel()
        {
            this.hrSensor = new HRSensor(HRSensor.Type.Unknown);
            this.gsrSensor = new GSRSensor();
        }

        /// <summary>
        /// Starts a new session, calculateModelValue() will
        /// now produce valid values.
        /// </summary>
        public override void startSession()
        {
            hrSensor.startMeasuring();
        }

        /// <summary>
        /// Stops the current session.
        /// </summary>
        public override void stopSession()
        {
            hrSensor.stopMeasuring();
        }

        /// <summary>
        /// Start a calibration session. This will reset any data
        /// from previous sessions.
        /// </summary>
        public override void startCalibration()
        {
            startCalibrationWithTimerParameters(0, 1000);
        }

        /// <summary>
        /// Creates a timerCallback for calibration and resets the Lists of any previous calibration sessions
        /// </summary>
        /// <param name="dueTime">The time of delay before the callback is invoked</param>
        /// <param name="period">The time interval between invokes</param>
        public void startCalibrationWithTimerParameters(int dueTime, int period)
        {
            calibrationHR = new List<double>();
            calibrationGSR = new List<double>();

            // Create and start a timer to poll the sensors
            TimerCallback timerCallback = calibrationTimerCallback;
            calibrationTimer = new Timer(timerCallback, null, dueTime, period);
        }

        /// <summary>
        /// Stops the current calibration session.
        /// </summary>
        public override void stopCalibration()
        {
            calibrationTimer.Dispose();

            // Set values derived from calibration
            HRMin = calibrationHR.Min();
            HRMax = calibrationHR.Max();
            GSRMin = calibrationGSR.Min();
            GSRMax = calibrationGSR.Max();

            HRMean = calibrationHR.Average();
            GSRMean = calibrationGSR.Average();
            HRsd = FuzzyMath.standardDeviationFromList(calibrationHR);
            GSRsd = FuzzyMath.standardDeviationFromList(calibrationGSR);
        }
        
        /// <summary>
        /// When calibrating this callback function adds an GSR and HR value every x time
        /// </summary>
        /// <param name="stateInfo"></param>
        public void calibrationTimerCallback(Object stateInfo)
        {
            Console.WriteLine("Calibratie: hrSensor={0} gsrSensor={1}", hrSensor.sensorValue, gsrSensor.sensorValue);
            calibrationHR.Add(hrSensor.sensorValue);
            calibrationGSR.Add(gsrSensor.sensorValue);
        }

        /// <summary>
        /// (Re)calculates the model value
        /// </summary>
        /// <returns>The model value casted to a double</returns>
        public override double calculateModelValue()
        {
            // Get the values from the sensors
            currentHR = hrSensor.sensorValue;
            currentGSR = gsrSensor.sensorValue;

            if (currentHR < HRMin || currentHR > HRMax)
            {
                displayLogMessage("Hartslag valt buiten de waarden gemeten tijdens calibratie");
            }
            if (currentGSR < GSRMin || currentGSR > GSRMax)
            {
                displayLogMessage("Huidgeleiding valt buiten de waarden gemeten tijdens calibratie");
            }

            normalisedHR = FuzzyMath.normalised(currentHR, HRMin, HRMax);
            normalisedGSR = FuzzyMath.normalised(currentGSR, GSRMin, GSRMax);

            // Check to see if the normalised values are valid (not NaN or otherwise invalid)
            List<double> arousalFuzzySet = new List<double>();
            if(normalisedGSR >= 0 && normalisedGSR <= 100 && normalisedHR >= 0 && normalisedHR <= 100)
            {
                arousalFuzzySet = inferToArousal(getGSRMembershipValues(), getHRMembershipValues());
            }

            return (double)getArousalLevel(arousalFuzzySet);
        }

        /// <summary>
        /// Performs fuzzy inferance on the gsr and hr fuzzy sets.
        /// </summary>
        /// <param name="gsrFuzzySet">The fuzzy set for gsr</param>
        /// <param name="hrFuzzySet">The fuzzy set for hr</param>
        /// <returns>The arousal fuzzy set, declaring membership to the values: 
        /// low, midlow, midhigh, high</returns>
        private List<double> inferToArousal(List<double> gsrFuzzySet, List<double> hrFuzzySet)
        {
            List<double> arousalFuzzySet = new List<double>();
            // These lists are used to store all values used to calculate the final fuzzy set per membership
            List<double> arousalHighList, arousalMidHighList, arousalMidLowList, arousalLowList;
            arousalHighList = new List<double>();
            arousalMidHighList = new List<double>();
            arousalMidLowList = new List<double>();
            arousalLowList = new List<double>();

            // Fuzzy rules
            arousalHighList.Add(gsrFuzzySet[(int)GSRLevel.High]);
            arousalHighList.Add(hrFuzzySet[(int)HRLevel.High]);
            arousalHighList.Add(Math.Min(gsrFuzzySet[(int)GSRLevel.High], hrFuzzySet[(int)HRLevel.Mid]));

            arousalMidHighList.Add(gsrFuzzySet[(int)GSRLevel.MidHigh]);
            arousalMidHighList.Add(Math.Min(gsrFuzzySet[(int)GSRLevel.High], hrFuzzySet[(int)HRLevel.Low]));
            arousalMidHighList.Add(Math.Min(gsrFuzzySet[(int)GSRLevel.MidHigh], hrFuzzySet[(int)HRLevel.Mid]));

            arousalMidLowList.Add(gsrFuzzySet[(int)GSRLevel.MidLow]);
            arousalMidLowList.Add(Math.Min(gsrFuzzySet[(int)GSRLevel.Low], hrFuzzySet[(int)HRLevel.High]));
            arousalMidLowList.Add(Math.Min(gsrFuzzySet[(int)GSRLevel.MidLow], hrFuzzySet[(int)HRLevel.Mid]));

            arousalLowList.Add(gsrFuzzySet[(int)GSRLevel.Low]);
            arousalLowList.Add(hrFuzzySet[(int)HRLevel.Low]);

            // Add the average values to the arousal fuzzy set. These averages represent the membership values.
            arousalFuzzySet.Add(arousalLowList.Average());
            arousalFuzzySet.Add(arousalMidLowList.Average());
            arousalFuzzySet.Add(arousalMidHighList.Average());
            arousalFuzzySet.Add(arousalHighList.Average());

            return arousalFuzzySet;
        }

        /// <summary>
        /// Defuzzify the arousal fuzzy set inorder to get the arousal level
        /// </summary>
        /// <param name="arousalFuzzySet">The arousal fuzzy set, at least one 
        /// membership value must be nonzero. Also, all values must be in the interval [0, 1].</param>
        /// <returns>The arousal level</returns>
        public ArousalLevel getArousalLevel(List<double> arousalFuzzySet)
        {
            ArousalLevel arousalLevel = ArousalLevel.Unknown;

            double weightedSum = 0;
            for (int i = 0; i < arousalFuzzySet.Count; i++)
            {
                weightedSum += arousalFuzzySet[i] * (i + 1);
            }
            double weightedAverage = weightedSum / arousalFuzzySet.Sum();
            arousalLevel = (ArousalLevel)Math.Round(weightedAverage, MidpointRounding.AwayFromZero);

            return arousalLevel;
        }

        /// <summary>
        /// Based on the normalised GSRValue, set the fuzzy values for the 4 levels of GSR: low, midlow, midhigh and high.
        /// </summary>
        /// <returns>A list of the fuzzy values for each level of GSR</returns>
        public List<double> getGSRMembershipValues()
        {
            double lowValue = FuzzyMath.lowGSRValue(GSRMean, GSRsd, normalisedGSR);
            double midLowValue = FuzzyMath.midLowGSRValue(GSRMean, GSRsd, normalisedGSR);
            double midHighValue = FuzzyMath.midHighGSRValue(GSRMean, GSRsd, normalisedGSR);
            double highValue = FuzzyMath.highGSRValue(GSRMean, GSRsd, normalisedGSR);

            List<double> GSRValueList = new List<double>(new double[] { 0, lowValue, midLowValue, midHighValue, highValue });

            return GSRValueList;
        }

        /// <summary>
        /// Based on the normalised HRValue, set the fuzzy values for the 3 HR levels, low, mid and high
        /// </summary>
        /// <returns>>A list of the fuzzy values for each level of HR</returns>
        public List<double> getHRMembershipValues()
        {
            double lowValue = FuzzyMath.lowHRValue(HRMean, HRsd, normalisedHR);
            double midValue = FuzzyMath.midHRValue(HRMean, HRsd, normalisedHR);
            double highValue = FuzzyMath.highHRValue(HRMean, normalisedHR);

            List<double> HRValueList = new List<double>(new double[] { 0, lowValue, midValue, highValue });

            return HRValueList;
        }

        /// <summary>
        /// Reloads the HRSensor with another one. Do not call HRSensor.startMeasuring() on the
        /// passed sensor as this method will already do this.
        /// </summary>
        /// <param name="hrSensor">The HRSensor to replace with</param>
        public void reloadWithHRSensor(HRSensor hrSensor)
        {
            this.hrSensor.stopMeasuring();
            this.hrSensor = hrSensor;
            this.hrSensor.startMeasuring();
        }

        private void displayLogMessage(string message)
        {
            if (delegateObject != null)
            {
                delegateObject.displayLogMessage("[FuzzyModel] " + message);
            }
        }
    }
}
