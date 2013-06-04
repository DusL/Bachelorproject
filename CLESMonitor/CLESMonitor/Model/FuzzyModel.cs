using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model
{
    class FuzzyModel : ESModel
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

        /// <summary>
        /// Connects to ComPort
        /// </summary>
        public override void startSession()
        {
            hrSensor.setUpSerialPort();
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


        public void fuzzyRules()
        { 
            //if(normalisedGSR GSR)
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
