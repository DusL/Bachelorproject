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

        // Data from the calibration period
        private int[] calibrationHR; //in beats/minute
        private int[] calibrationGSR; //in siemens
        private int HRMax, HRMin;
        private int GSRMax, GSRMin;

        // Currently measured values
        private double currentHR;
        private double currentGSR;

        /// <summary>
        /// Constructor method
        /// </summary>
        /// <param name="hrSensor"></param>
        /// <param name="gsrSensor"></param>
        public FuzzyModel(HRSensor hrSensor, GSRSensor gsrSensor)
        {
            this.hrSensor = hrSensor;
            this.gsrSensor = gsrSensor;
        }

        public void setupWithCalibrationData(int[] HRValues, int[] GSRValues)
        {
            //Adopt the calibration values
            calibrationHR = HRValues;
            calibrationGSR = GSRValues;

            //Set the min and max values
            HRMin = calibrationHR.Min();
            HRMax = calibrationHR.Max();
            GSRMax = calibrationGSR.Max();
            GSRMax = calibrationGSR.Max();
        }

        public override void startSession()
        {
            hrSensor.setUpSerialPort();
        }

        public override double calculateModelValue()
        {
            //Retrieve the values from the sensors
            currentHR = hrSensor.sensorValue;
            currentGSR = gsrSensor.sensorValue;

            return currentHR;
        }

        private double calculateNormalisedHR(double HRValue)
        {
            return ((HRValue - HRMin)/(HRMax - HRMin))*100;
        }

        private double calculateNormalisedGSR(double GSRValue)
        {
            return ((GSRValue - GSRMin) / (GSRMax - GSRMin)) * 100;
        }
    }
}
