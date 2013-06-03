using System.Linq;

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
        /// Constructor methode waarin direct de sensoren geset worden
        /// </summary>
        /// <param name="hrSensor"></param>
        /// <param name="gsrSensor"></param>
        public FuzzyModel(HRSensor hrSensor, GSRSensor gsrSensor)
        {
            this.hrSensor = hrSensor;
            this.gsrSensor = gsrSensor;
        }

        /// <summary>
        /// Krijgt de waarden uit de calibratie periode binnen en set op basis daarvan 
        /// de minimum en maximum waarden per sensor.
        /// </summary>
        /// <param name="HRValues"></param>
        /// <param name="GSRValues"></param>
        private void setupWithCalibrationData(int[] HRValues, int[] GSRValues)
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

        /// <summary>
        /// Start a model session
        /// </summary>
        public override void startSession()
        {
            hrSensor.startMeasuring();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override double calculateModelValue()
        {
            // Pak de waarden uit de sensoren
            currentHR = hrSensor.sensorValue;
            currentGSR = gsrSensor.sensorValue;

            return currentHR;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="HRValue"></param>
        /// <returns>De genormaliseerde hartrate (double)</returns>
        private double calculateNormalisedHR(double HRValue)
        {
            return ((HRValue - HRMin)/(HRMax - HRMin))*100;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="GSRValue"></param>
        /// <returns>De genormaliseerde skikn conductance (double)</returns>
        private double calculateNormalisedGSR(double GSRValue)
        {
            return ((GSRValue - GSRMin) / (GSRMax - GSRMin)) * 100;
        }
    }
}
