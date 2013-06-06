using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using CLESMonitor;

namespace CLESMonitor.Model
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
        //
        private FuzzyCalculate calculate;
        // Sensors
        private HRSensor hrSensor;
        private GSRSensor gsrSensor;

        // Data from calibration periode
        Timer calibrationTimer;
        private List<double> calibrationHR; //in beats/minute
        private List<double> calibrationGSR; //in siemens
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
        private GSRLevel gsrLevel;
        private HRLevel hrLevel;

        // The current arousal level
        private ArousalLevel arousalLevel;

        /// <summary>
        /// Constructor method that sets the sensors immediately
        /// </summary>
        /// <param name="hrSensor"></param>
        /// <param name="gsrSensor"></param>
        public FuzzyModel(HRSensor hrSensor, GSRSensor gsrSensor)
        {
            this.hrSensor = hrSensor;
            this.gsrSensor = gsrSensor;
            calculate = new FuzzyCalculate();
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
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Start a calibration session. This will reset any data
        /// from previous sessions.
        /// </summary>
        public override void startCalibration()
        {
            Console.WriteLine("FuzzyModel.startCalibration()");

            calibrationHR = new List<double>();
            calibrationGSR = new List<double>();

            // Create and start a timer to poll the sensors
            TimerCallback timerCallback = calibrationTimerCallback;
            calibrationTimer = new Timer(timerCallback, null, 0, 1000);
        }

        /// <summary>
        /// Stops the current calibration session.
        /// </summary>
        public override void stopCalibration()
        {
            Console.WriteLine("FuzzyModel.stopCalibration()");

            calibrationTimer.Dispose();

            // Set values derived from calibration
            HRMin = calibrationHR.Min();
            HRMax = calibrationHR.Max();
            GSRMin = calibrationGSR.Min();
            GSRMax = calibrationGSR.Max();

            HRMean = calibrationHR.Average();
            GSRMean = calibrationGSR.Average();
            HRsd = standardDeviationFromList(calibrationHR);
            GSRsd = standardDeviationFromList(calibrationGSR);

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

        private double standardDeviationFromList(List<double> list)
        {
            double sumOfSquares = 0;
            foreach (double value in list)
            {
                sumOfSquares += Math.Pow(value - list.Average(), 2); 
            }
            return Math.Sqrt(sumOfSquares / list.Count-1);
        }

        private void calibrationTimerCallback(Object stateInfo)
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

            normalisedGSR = calculate.normalisedGSR(currentGSR, GSRMin, GSRMax);
            normalisedHR = calculate.normalisedHR(currentHR, HRMin, HRMax);

            return (double)arousalLevel;
        }

        /// <summary>
        /// Sets the arousal level based on the fuzzy logic model
        /// </summary>
        public void fuzzyArousalRules()
        {
            if (gsrLevel.Equals(GSRLevel.High) && hrLevel.Equals(HRLevel.Low))
            {
                arousalLevel = ArousalLevel.MidHigh;
            }
            else if (gsrLevel.Equals(GSRLevel.High) && hrLevel.Equals(HRLevel.Mid))
            {
                arousalLevel = ArousalLevel.High;
            }
            else if (gsrLevel.Equals(GSRLevel.MidHigh) && hrLevel.Equals(HRLevel.Mid))
            {
                arousalLevel = ArousalLevel.MidHigh;
            }
            else if (gsrLevel.Equals(GSRLevel.MidLow) && hrLevel.Equals(HRLevel.Mid))
            {
                arousalLevel = ArousalLevel.MidLow;
            }
            else if (gsrLevel.Equals(GSRLevel.Low) && hrLevel.Equals(HRLevel.High))
            {
                arousalLevel = ArousalLevel.MidLow;
            }

            else if (gsrLevel.Equals(GSRLevel.High))
            {
                arousalLevel = ArousalLevel.High;
            }
            else if (gsrLevel.Equals(GSRLevel.MidHigh))
            {
                arousalLevel = ArousalLevel.MidHigh;
            }
            else if (gsrLevel.Equals(GSRLevel.MidLow))
            {
                arousalLevel = ArousalLevel.MidLow;
            }
            else if (gsrLevel.Equals(GSRLevel.Low))
            {
                arousalLevel = ArousalLevel.Low;
            }
            else if (hrLevel.Equals(HRLevel.Low))
            {
                arousalLevel = ArousalLevel.Low;
            }
            else if (hrLevel.Equals(HRLevel.High))
            {
                arousalLevel = ArousalLevel.High;
            }
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
            double lowValue = calculate.lowGSRValue(GSRMean, GSRsd, normalisedGSR);
            double midLowValue = calculate.midLowGSRValue(GSRMean, GSRsd, normalisedGSR);
            double midHighValue = calculate.midHighGSRValue(GSRMean, GSRsd, normalisedGSR);
            double highValue = calculate.highGSRValue(GSRMean, GSRsd, normalisedGSR);

            List<double> GSRValueList = new List<double>(new double[] { lowValue, midLowValue, midHighValue, highValue });

            return GSRValueList;
        }

        /// <summary>
        /// Based on the normalised HRValue, set the fuzzy values fir the 3 HR levels, low, mid and high
        /// </summary>
        /// <returns>>A list of the fuzzy values for each level of HR</returns>
        public List<double> fuzzyHR()
        {
            double lowValue = calculate.lowHRValue(HRMean, HRsd, normalisedHR);
            double midValue = calculate.midHRValue(HRMean, HRsd, normalisedHR);
            double highValue = calculate.highHRValue(HRMean, normalisedHR);

            List<double> HRValueList = new List<double>(new double[] { lowValue, midValue, highValue });

            return HRValueList;
        }

    }
}
