using System;
using NUnit.Framework;
using Moq;
using CLESMonitor.Model.ES;
using System.Collections.Generic;
using System.Threading;

namespace UnitTest.Model.ES
{
    [TestFixture]
    public class FuzzyModelTest
    {
        FuzzyModel model;
        Mock<HRSensor> mockedHRSensor;
        Mock<GSRSensor> mockedGSRSensor;
        HRSensor hrSensor;
        GSRSensor gsrSensor;

        double hrValue, gsrValue;

        [SetUp]
        public void setUp()
        {
            mockedHRSensor = new Mock<HRSensor>();
            mockedGSRSensor = new Mock<GSRSensor>();

            hrSensor = new HRSensor();
            gsrSensor = new GSRSensor();

            hrSensor.type = HRSensorType.ManualInput;
            gsrSensor.type = GSRSensorType.ManualInput;

            model = new FuzzyModel(hrSensor, gsrSensor);
        }

        [TearDown]
        public void tearDown()
        {
            hrSensor = null;
            gsrSensor = null;

            model = null;
        }

        [Test]
        public void CallBack()
        {
            model.startCalibrationWithTimerParameters(Timeout.Infinite, Timeout.Infinite);
                
            List<double> hrList = new List<double>(new double[] { 3.0});
            List<double> gsrList = new List<double>(new double[] {5.0});
            
            hrSensor.sensorValue = hrValue = 3.0;
            gsrSensor.sensorValue = gsrValue = 5.0;
            
            model.calibrationTimerCallback(null);

            Assert.AreEqual(hrList, model.calibrationHR);
            Assert.AreEqual(gsrList, model.calibrationGSR);

            hrSensor.sensorValue = hrValue = 8.0;
            gsrSensor.sensorValue = gsrValue = 6.0;

            hrList.Add(hrValue);
            gsrList.Add(gsrValue);

            model.calibrationTimerCallback(null);
            Assert.AreEqual(hrList, model.calibrationHR);
            Assert.AreEqual(gsrList, model.calibrationGSR);
        }

        /// <summary>
        /// Input values between 0 and 1 result in the expected HRLevel
        /// </summary>
        [Test]
        public void findHRLevel_ValidValues()
        {
            double lowValue = .2;
            double midValue = .8;
            double highValue = .6;

            List<double> inputList = new List<double>(new double[] { lowValue, midValue, highValue });
            model.findHRLevel(inputList);
            Assert.AreEqual(HRLevel.Mid, model.hrLevel);

            lowValue = .0;
            midValue = .5;
            highValue = .5;
            inputList = new List<double>(new double[] { lowValue, midValue, highValue });

            Assert.AreEqual(HRLevel.Mid, model.hrLevel);
        }

        /// <summary>
        /// Input values between 0 and 1, result in the expected GSRLevel
        /// </summary>
        [Test]
        public void findGSRLevel_ValidValues()
        {
            double lowValue = .0;
            double midLowValue = .2;
            double midHighValue = .6;
            double highValue = .3;

            List<double> inputList = new List<double>(new double[] { lowValue, midLowValue, midHighValue, highValue });
            model.findGSRLevel(inputList);
            Assert.AreEqual(GSRLevel.MidHigh, model.gsrLevel);

            lowValue = .0;
            midLowValue = .5;
            midHighValue = .5;
            highValue = .1;

            inputList = new List<double>(new double[] { lowValue, midLowValue, midHighValue, highValue });
            model.findGSRLevel(inputList);
            Assert.AreEqual(GSRLevel.MidLow, model.gsrLevel);
        }

    }
}
