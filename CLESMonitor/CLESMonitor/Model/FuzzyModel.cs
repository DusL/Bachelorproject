using System.Collections.Generic;
using System.Linq;

namespace CLESMonitor.Model
{
    public class FuzzyModel : ESModel
    {
        // HR = heart rate
        // GSR = skin conductance

        // Sensors
        private HRSensor hrSensor;
        private GSRSensor gsrSensor;

        // Data from calibration periode
        private int[] calibrationHR; //in beats/minute
        private int[] calibrationGSR; //in siemens
        private int HRMax, HRMin;
        private int GSRMax, GSRMin;
        private int GSRMean, HRMean;
        private int GSRStandardDeviation, HRStandardDeviation;

        // Current values
        private double currentHR;
        private double currentGSR;

        // Current normalised values
        private double normalisedGSR;
        private double normalisedHR;

        // Current values in terms of high-mid-low 
        private string GSR;
        private string HR;

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
        /// Receives the calibration values and sets the minimum and maximum values per sensor
        /// </summary>
        /// <param name="HRValues"></param>
        /// <param name="GSRValues"></param>
        private void setupWithCalibrationData(int[] HRValues, int[] GSRValues)
        {
            // Adopts the calibration values
            calibrationHR = HRValues;
            calibrationGSR = GSRValues;

            // Set the wanted values
            HRMin = calibrationHR.Min();
            HRMax = calibrationHR.Max();
            GSRMax = calibrationGSR.Max();
            GSRMax = calibrationGSR.Max();
        }

        public override void startSession()
        {
            hrSensor.startMeasuring();
        }

        public override void stopSession()
        {
            throw new System.NotImplementedException();
        }

        public override void startCalibration()
        {
            throw new System.NotImplementedException();
        }

        public override void stopCalibration()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override double calculateModelValue()
        {
            // Get the values from the sensors
            currentHR = hrSensor.sensorValue;
            currentGSR = gsrSensor.sensorValue;

            normalisedGSR = calculateNormalisedGSR(currentGSR);
            normalisedHR = calculateNormalisedHR(currentHR);

            return currentHR;
        }

        public void findGSRLevel(List<double> GSRValueList)
        {
            double lowValue = GSRValueList[0];
            double midLowValue = GSRValueList[1];
            double midHighValue = GSRValueList[2];
            double highValue = GSRValueList[3];

            
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

            List<double> GSRValueList = new List<double>(new double[] {lowValue, midLowValue, midHighValue, highValue});

            return GSRValueList;
        }
        /// <summary>
        /// Calculate the Fuzzy value of GSRLow
        /// </summary>
        /// <returns></returns>
        private double calculateLowGSRValue() 
        {
            double value = 0;
            double rightLowBoudary = GSRMean - 1.5 * GSRStandardDeviation;

            // If the normalised value falls within the boundaries, calculate the value
            if (normalisedGSR <= rightLowBoudary)
            {
                value = (rightLowBoudary - normalisedGSR) / rightLowBoudary;
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
            double leftMidLowBoundary = GSRMean - 2 * GSRStandardDeviation;
            double rightMidLowBoundary = GSRMean;

            // Since the midLow fuzzyArea is triangular, two different calculations are necessary
            // If the normalised value falls on the left side of the triangle
            if (leftMidLowBoundary <= normalisedGSR && normalisedGSR <= (GSRMean - GSRStandardDeviation))
            {
                value = (normalisedGSR - leftMidLowBoundary) / ((GSRMean - GSRStandardDeviation) - leftMidLowBoundary);
            }
            // If the value falls on the right side
            else if (normalisedGSR >= (GSRMean - GSRStandardDeviation) && rightMidLowBoundary >= normalisedGSR)
            {
                value = (rightMidLowBoundary - normalisedGSR) / (rightMidLowBoundary - (GSRMean - GSRStandardDeviation));
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
            double leftMidHighBoundary = GSRMean - GSRStandardDeviation;
            double rightMidHighBoundary = GSRMean + GSRStandardDeviation;

            // Since the midHigh fuzzyArea is triangular, two different calculations are necessary
            // If the normalised value falls on the left side of the triangle
            if (leftMidHighBoundary <= normalisedGSR && normalisedGSR <= (GSRMean - GSRStandardDeviation))
            {
                value = (normalisedGSR - leftMidHighBoundary) / (GSRMean - leftMidHighBoundary);
            }
            // If the value falls on the right side
            else if (normalisedGSR >= (GSRMean - GSRStandardDeviation) && rightMidHighBoundary >= normalisedGSR)
            {
                value = (rightMidHighBoundary - normalisedGSR) / (rightMidHighBoundary - GSRMean);
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
            double leftHighBoundary = GSRMean;
            double rightHighBoundary = GSRMax;
            /* If the normalised value is not greater than the mean +1SD but within the boundaries of "high"
               calculate the value*/
            if (normalisedGSR >= leftHighBoundary && normalisedGSR <= (GSRMean + GSRStandardDeviation))
            {
                value = (normalisedGSR - GSRMean) / ((GSRMean + GSRStandardDeviation) - GSRMean);
            }
            // If the value is greater the mean + 1SD, the value high = 1
            else if (normalisedGSR >= (GSRMean + GSRStandardDeviation))
            {
                value = 1;
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
