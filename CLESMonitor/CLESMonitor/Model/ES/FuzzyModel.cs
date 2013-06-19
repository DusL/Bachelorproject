using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using CLESMonitor;

namespace CLESMonitor.Model.ES
{
    public enum GSRLevel
    {
        Unknown,
        Low,
        MidLow,
        MidHigh,
        High
    }

    public enum HRLevel
    { 
        Unknown,
        Low,
        Mid,
        High
    }

    public enum ArousalLevel
    { 
        Unknown,
        Low,
        MidLow,
        MidHigh,
        High
    }

    /// <summary>
    /// This class is an implementation of an Emotional State model
    /// using Fuzzy Logic.
    /// </summary>
    public class FuzzyModel : ESModel
    {
        // HR = heart rate
        // GSR = skin conductance

        // Sensors
        private HRSensor hrSensor;
        private GSRSensor gsrSensor;

        // Data from calibration periode
        private Timer calibrationTimer;
        public List<double> calibrationHR; //in beats/minute
        public List<double> calibrationGSR; //in siemens
        private double HRMax, HRMin;
        private double GSRMax, GSRMin;
        private double GSRMean, HRMean;
        private double GSRsd, HRsd;

        // Current values
        private double currentHR;
        private double currentGSR;

        // Current normalised values
        private double normalisedGSR;
        private double normalisedHR;

        // The current 'sensor' levels
        public GSRLevel gsrLevel;
        public HRLevel hrLevel;

        // The current arousal level
        private  ArousalLevel[,] arousal;

        /// <summary>
        /// Constructor method that sets the sensors immediately
        /// </summary>
        /// <param name="hrSensor"></param>
        /// <param name="gsrSensor"></param>
        public FuzzyModel(HRSensor hrSensor, GSRSensor gsrSensor)
        {
            this.hrSensor = hrSensor;
            this.gsrSensor = gsrSensor;
            arousal = createFuzzyMatrix(); // set the matrix for the arousal values

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
        /// Creates a timerCallback for calibration and resets teh Lists of any previous calibration sessions
        /// </summary>
        /// <param name="dueTime">The time of delay before the callback is invoked</param>
        /// <param name="period">The time interval between invokes</param>
        public void startCalibrationWithTimerParameters(int dueTime, int period)
        {
            Console.WriteLine("FuzzyModel.startCalibration()");

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

            //TODO: Ongetest
            Console.WriteLine("FuzzyModel.stopCalibration()");

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

            foreach (double value in calibrationHR)
            {
                Console.WriteLine("HR - " + value);
            }
            foreach (double value in calibrationGSR)
            {
                Console.WriteLine("GSR - " + value);
            }
            Console.WriteLine("HRMin={0} HRMax={1} GSRMin={2} GSRMax={3} HRMean={4} GSRMean={5} HRsd={6} GSRsd={7}", HRMin, HRMax, GSRMin, GSRMax, HRMean, GSRMean, HRsd, GSRsd);
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

            // TODO: Blijft 0 totdat je de slider een keer beweegt.
            normalisedHR = FuzzyMath.normalisedHR(currentHR, HRMin, HRMax);
            normalisedGSR = FuzzyMath.normalisedGSR(currentGSR, GSRMin, GSRMax);
            
            // Find the current GSR and HR Levels
            findGSRLevel(fuzzyGSR());
            findHRLevel(fuzzyHR());

            return (double)getArousalLevel(gsrLevel, hrLevel); 
        }

        /// <summary>
        /// Sets the ArousalLevel for each possible combination of gsr and hr levels.
        /// </summary>
        public static ArousalLevel[,] createFuzzyMatrix()
        {
            // Create a 2-dimensional array of length 5, 4
            ArousalLevel[,] arousal = new ArousalLevel[(int)GSRLevel.High +1, (int)HRLevel.High+1];

            // Set al values for which GSRLevel is unknown
            arousal[(int)GSRLevel.Unknown, (int)HRLevel.Unknown] = ArousalLevel.Unknown;
            arousal[(int)GSRLevel.Unknown, (int)HRLevel.Low] = ArousalLevel.Unknown;
            arousal[(int)GSRLevel.Unknown, (int)HRLevel.Mid] = ArousalLevel.Unknown;
            arousal[(int)GSRLevel.Unknown, (int)HRLevel.High] = ArousalLevel.Unknown;

            // Set al values for which GSRLevel is Low
            arousal[(int)GSRLevel.Low, (int)HRLevel.Unknown] = ArousalLevel.Unknown;
            arousal[(int)GSRLevel.Low, (int)HRLevel.Low] = ArousalLevel.Low;
            arousal[(int)GSRLevel.Low, (int)HRLevel.Mid] = ArousalLevel.Low;
            arousal[(int)GSRLevel.Low, (int)HRLevel.High] = ArousalLevel.MidLow;

            // Set al values for which GSRLevel is MidLow
            arousal[(int)GSRLevel.MidLow, (int)HRLevel.Unknown] = ArousalLevel.Unknown;
            arousal[(int)GSRLevel.MidLow, (int)HRLevel.Low] = ArousalLevel.MidLow;
            arousal[(int)GSRLevel.MidLow, (int)HRLevel.Mid] = ArousalLevel.MidLow;
            arousal[(int)GSRLevel.MidLow, (int)HRLevel.High] = ArousalLevel.MidLow;

            // Set al values for which GSRLevel is MidHigh
            arousal[(int)GSRLevel.MidHigh, (int)HRLevel.Unknown] = ArousalLevel.Unknown;
            arousal[(int)GSRLevel.MidHigh, (int)HRLevel.Low] = ArousalLevel.MidHigh;
            arousal[(int)GSRLevel.MidHigh, (int)HRLevel.Mid] = ArousalLevel.MidHigh;
            arousal[(int)GSRLevel.MidHigh, (int)HRLevel.High] = ArousalLevel.MidHigh;

            // Set al values for which GSRLevel is High
            arousal[(int)GSRLevel.High, (int)HRLevel.Unknown] = ArousalLevel.Unknown;
            arousal[(int)GSRLevel.High, (int)HRLevel.Low] = ArousalLevel.MidHigh;
            arousal[(int)GSRLevel.High, (int)HRLevel.Mid] = ArousalLevel.High;
            arousal[(int)GSRLevel.High, (int)HRLevel.High] = ArousalLevel.High;

            return arousal;

        }

        /// <summary>
        /// Returns the arousal level based on the current gsr and hr values
        /// </summary>
        /// <param name="gsrLevel">The current GSRLevel in terms of the enum</param>
        /// <param name="hrLevel">The current HRLevel in terms of the enum</param>
        /// <returns>The ArousalLevel</returns>
        public ArousalLevel getArousalLevel(GSRLevel gsrLevel, HRLevel hrLevel)
        {
            return arousal[(int)gsrLevel, (int)hrLevel];
        }

        /// <summary>
        /// Based on the fuzzy values it receives, determines which 'level' GSR is at.
        /// Sets the GSR enum
        /// </summary>
        /// <param name="GSRValueList"></param>
        public void findGSRLevel(List<double> GSRValueList)
        {
            double lowValue = GSRValueList[0];
            double midLowValue = GSRValueList[1];
            double midHighValue = GSRValueList[2];
            double highValue = GSRValueList[3];

            double maxTemp = lowValue;
            GSRLevel tempLevel = GSRLevel.Low;

            if (midLowValue > maxTemp)
            {
                maxTemp = midLowValue;
                tempLevel = GSRLevel.MidLow;
            }

            if (midHighValue > maxTemp)
            {
                maxTemp = midHighValue;
                tempLevel = GSRLevel.MidHigh;
            }
            if (highValue > maxTemp)
            {
                maxTemp = highValue;
                tempLevel = GSRLevel.High;
            }

            gsrLevel = tempLevel;

        }

        /// <summary>
        /// Based on the fuzzy values it receives, determines which 'level' HR is at.
        /// Sets the HR enum
        /// </summary>
        /// <param name="HRValueList"></param>
        public void findHRLevel(List<double> HRValueList) 
        {
            double lowValue = HRValueList[0];
            double midValue = HRValueList[1];
            double highValue = HRValueList[2];

            double maxTemp = lowValue;
            HRLevel tempLevel = HRLevel.Low;

            if (midValue > maxTemp)
            {
                maxTemp = midValue;
                tempLevel = HRLevel.Mid;
            }

            if (highValue > maxTemp)
            {
                maxTemp = highValue;
                tempLevel = HRLevel.High;
            }

            hrLevel = tempLevel;
        }

        /// <summary>
        /// Based on the normalised GSRValue, set the fuzzy values for the 4 levels of GSR: low, midlow, midhigh and high.
        /// </summary>
        /// <returns>A list of the fuzzy values for each level of GSR</returns>
        public List<double> fuzzyGSR()
        {
            double lowValue = FuzzyMath.lowGSRValue(GSRMean, GSRsd, normalisedGSR);
            double midLowValue = FuzzyMath.midLowGSRValue(GSRMean, GSRsd, normalisedGSR);
            double midHighValue = FuzzyMath.midHighGSRValue(GSRMean, GSRsd, normalisedGSR);
            double highValue = FuzzyMath.highGSRValue(GSRMean, GSRsd, normalisedGSR);

            List<double> GSRValueList = new List<double>(new double[] { lowValue, midLowValue, midHighValue, highValue });

            return GSRValueList;
        }

        /// <summary>
        /// Based on the normalised HRValue, set the fuzzy values fir the 3 HR levels, low, mid and high
        /// </summary>
        /// <returns>>A list of the fuzzy values for each level of HR</returns>
        public List<double> fuzzyHR()
        {
            double lowValue = FuzzyMath.lowHRValue(HRMean, HRsd, normalisedHR);
            double midValue = FuzzyMath.midHRValue(HRMean, HRsd, normalisedHR);
            double highValue = FuzzyMath.highHRValue(HRMean, normalisedHR);

            List<double> HRValueList = new List<double>(new double[] { lowValue, midValue, highValue });

            return HRValueList;
        }

    }
}
