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

        // Sensoren
        private HRSensor hrSensor;
        private GSRSensor gsrSensor;

        // Data uit de calibratieperiode
        private int[] calibrationHR; //in slagen/minuut
        private int[] calibrationGSR; //in siemens
        private int HRMax, HRMin;
        private int GSRMax, GSRMin;

        // Huidige gemeten waardes
        private double currentHR;
        private double currentGSR;

        /// <summary>
        /// Constructor methode
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
            // Neem de calibratie waarden over
            calibrationHR = HRValues;
            calibrationGSR = GSRValues;

            // Herleid nieuwe waarden
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
            // Pak de waarden uit de sensoren
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
