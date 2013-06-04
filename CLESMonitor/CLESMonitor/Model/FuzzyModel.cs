using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;


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

        // Sensors
        private HRSensor hrSensor;
        private GSRSensor gsrSensor;

        // Data from calibration periode
        Timer calibrationTimer;
        private List<double> calibrationHR; //in beats/minute
        private List<double> calibrationGSR; //in siemens
        private int HRMax, HRMin;
        private int GSRMax, GSRMin;
        private int GSRMean, HRMean;
        private double GSRStandardDeviation, HRStandardDeviation;

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
            HRMin = (int)calibrationHR.Min();
            HRMax = (int)calibrationHR.Max();
            GSRMin = (int)calibrationGSR.Min();
            GSRMax = (int)calibrationGSR.Max();

            HRMean = (int)calibrationHR.Average();
            GSRMean = (int)calibrationGSR.Average();
            HRStandardDeviation = standardDeviationFromList(calibrationHR);
            GSRStandardDeviation = standardDeviationFromList(calibrationGSR);

            foreach (double value in calibrationHR)
            {
                Console.WriteLine("HR - " + value);
            }
            foreach (double value in calibrationGSR)
            {
                Console.WriteLine("GSR - " + value);
            }
            Console.WriteLine("HRMin={0} HRMax={1} GSRMin={2} GSRMax={3} HRMean={4} GSRMean={5} HRsd={6} GSRsd={7}", HRMin, HRMax, GSRMin, GSRMax, HRMean, GSRMean, HRStandardDeviation, GSRStandardDeviation);
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
        /// <returns>The model value</returns>
        public override double calculateModelValue()
        {
            // Get the values from the sensors
            currentHR = hrSensor.sensorValue;
            currentGSR = gsrSensor.sensorValue;

            normalisedGSR = calculateNormalisedGSR(currentGSR);
            normalisedHR = calculateNormalisedHR(currentHR);

            return currentHR;
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
            double lowValue = calculateLowGSRValue();
            double midLowValue = calculateLowGSRValue();
            double midHighValue = calculateMidHighGSRValue();
            double highValue = calculateHighGSRValue();

            List<double> GSRValueList = new List<double>(new double[] { lowValue, midLowValue, midHighValue, highValue });

            return GSRValueList;
        }

        /// <summary>
        /// Based on the normalised HRValue, set the fuzzy values fir the 3 HR levels, low, mid and high
        /// </summary>
        /// <returns>>A list of the fuzzy values for each level of HR</returns>
        public List<double> fuzzyHR()
        {
            double lowValue = calculateLowHRValue();
            double midValue = calculateMidHRValue();
            double highValue = calculateHighHRValue();

            List<double> HRValueList = new List<double>(new double[] { lowValue, midValue, highValue });

            return HRValueList;
        }

        /// <summary>
        /// Calculate the Fuzzy value of GSRLow
        /// </summary>
        /// <returns></returns>
        private double calculateLowGSRValue() 
        {
            double value = 0;
            double rightBoudary = GSRMean - 1.5 * GSRStandardDeviation;

            // If the normalised value falls within the boundaries, calculate the value
            if (normalisedGSR <= rightBoudary)
            {
                value = (rightBoudary - normalisedGSR) / rightBoudary;
            }

            return value;
        }

        /// <summary>
        /// Calculate the Fuzzy value of GSRMidLow
        /// </summary>
        /// <returns></returns>
        private double calculateMidLowGSRValue()
        {
            double value = 0;
            double leftBoundary = GSRMean - 2 * GSRStandardDeviation;
            double rightBoundary = GSRMean;

            // Since the midLow fuzzyArea is triangular, two different calculations are necessary
            // If the normalised value falls on the left side of the triangle
            if (leftBoundary <= normalisedGSR && normalisedGSR <= (GSRMean - GSRStandardDeviation))
            {
                //(GSRMean - GSRStandardDeviation) - leftBoundary = -3 * GSRStandardDeviation
                value = (normalisedGSR - leftBoundary) / ((GSRMean - GSRStandardDeviation) - leftBoundary);
            }
            // If the value falls on the right side
            else if (normalisedGSR >= (GSRMean - GSRStandardDeviation) && rightBoundary >= normalisedGSR)
            {
                // (rightBoundary - (GSRMean - GSRStandardDeviation) = GSRMean - GSRMean - GSRStandardDeviation = GSRStandardDeviation
                value = (rightBoundary - normalisedGSR) / (rightBoundary - (GSRMean - GSRStandardDeviation));
            }

            return value;
        }

        /// <summary>
        /// Calculate the Fuzzy value of GSRMidHigh
        /// </summary>
        /// <returns></returns>
        private double calculateMidHighGSRValue()
        {
            double value = 0;
            double leftBoundary = GSRMean - GSRStandardDeviation;
            double rightBoundary = GSRMean + GSRStandardDeviation;

            // Since the midHigh fuzzyArea is triangular, two different calculations are necessary
            // If the normalised value falls on the left side of the triangle
            if (leftBoundary <= normalisedGSR && normalisedGSR <= GSRMean )
            {
                value = (normalisedGSR - leftBoundary) / (GSRMean - leftBoundary);
            }
            // If the value falls on the right side
            else if (normalisedGSR >= GSRMean && rightBoundary >= normalisedGSR)
            {
                value = (rightBoundary - normalisedGSR) / (rightBoundary - GSRMean);
            }

            return value;
        }

        /// <summary>
        /// Calculate the Fuzzy value of GSRHigh
        /// </summary>
        /// <returns></returns>
        private double calculateHighGSRValue()
        {
            double value = 0;
            double leftBoundary = GSRMean;
            /* If the normalised value is not greater than the mean +1SD but within the boundaries of "high"
               calculate the value*/
            if (normalisedGSR >= leftBoundary && normalisedGSR <= (GSRMean + GSRStandardDeviation))
            {
                value = (normalisedGSR - leftBoundary) / ((GSRMean + GSRStandardDeviation) - leftBoundary);
            }
            // If the value is greater the mean + 1SD, the value high = 1
            else if (normalisedGSR >= (GSRMean + GSRStandardDeviation))
            {
                value = 1;
            }

            return value;
        }
        /// <summary>
        /// Calculates the fuzzy value for the 'low' level of HR
        /// </summary>
        /// <returns>The truth value of 'low' (double)</returns>
        private double calculateLowHRValue()
        {
            double value = 0;
            double rightBoudary = HRMean - HRStandardDeviation;

            // If the normalised value falls within the boundaries, calculate the value
            if (normalisedHR <= rightBoudary)
            {
                value = (rightBoudary - normalisedHR) / rightBoudary;
            }

            return value;
        }

        /// <summary>
        /// Calculates the fuzzy value for the 'mid' level of HR
        /// </summary>
        /// <returns>The truth value of 'mid' (double)</returns>
        private double calculateMidHRValue()
        {
            double value = 0;
            double leftBoundary = HRMean - 2 * GSRStandardDeviation;
            double rightBoundary = HRMean + 2 * GSRStandardDeviation;

            // Since the midLow fuzzyArea is triangular, two different calculations are necessary
            // If the normalised value falls on the left side of the triangle
            if (leftBoundary <= normalisedHR && normalisedHR <= (HRMean - leftBoundary))
            {
                value = (normalisedHR - leftBoundary) / (HRMean - leftBoundary);
            }
            // If the value falls on the right side
            else if (normalisedHR >= HRMean && rightBoundary >= normalisedHR)
            {
                value = (rightBoundary - normalisedHR) / (rightBoundary - HRMean);
            }

            return value;
        }

        /// <summary>
        /// Calculates the fuzzy value for the 'high' level of HR
        /// </summary>
        /// <returns>The truth value of 'high' (double)</returns>
        private double calculateHighHRValue()
        {
            double value = 0;
            double leftBoundary = HRMean;

            // If the normalised value falls withing teh boundaries of high
            if (normalisedHR >= leftBoundary)
            {
                //The maximum value HRNormalised can get = 100;
                value = (normalisedHR - leftBoundary) / (100 - leftBoundary);
            }
            
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HRValue"></param>
        /// <returns>The normalised hartrate (double)</returns>
        private double calculateNormalisedHR(double HRValue)
        {
            return ((HRValue - HRMin)/(HRMax - HRMin))*100;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GSRValue"></param>
        /// <returns>The normalised skin conductance (double)</returns>
        private double calculateNormalisedGSR(double GSRValue)
        {
            return ((GSRValue - GSRMin) / (GSRMax - GSRMin)) * 100;
        }
    }
}
