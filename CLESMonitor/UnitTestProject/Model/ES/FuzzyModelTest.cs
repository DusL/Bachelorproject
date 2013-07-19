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

        double hrValue, gsrValue;

        [SetUp]
        public void setUp()
        {
            mockedHRSensor = new Mock<HRSensor>();
            mockedGSRSensor = new Mock<GSRSensor>();

            model = new FuzzyModel();
            model.hrSensor.type = HRSensor.Type.ManualInput;
            model.gsrSensor.type = GSRSensor.Type.ManualInput;
        }

        [TearDown]
        public void tearDown()
        {
            model = null;
        }

        [Test]
        public void CallBack()
        {
            model.startCalibrationWithTimerParameters(Timeout.Infinite, Timeout.Infinite);
                
            List<double> hrList = new List<double>(new double[] { 3.0});
            List<double> gsrList = new List<double>(new double[] {5.0});

            model.hrSensor.sensorValue = hrValue = 3.0;
            model.gsrSensor.sensorValue = gsrValue = 5.0;
            
            model.calibrationTimerCallback(null);

            Assert.AreEqual(hrList, model.calibrationHR);
            Assert.AreEqual(gsrList, model.calibrationGSR);

            model.hrSensor.sensorValue = hrValue = 8.0;
            model.gsrSensor.sensorValue = gsrValue = 6.0;

            hrList.Add(hrValue);
            gsrList.Add(gsrValue);

            model.calibrationTimerCallback(null);
            Assert.AreEqual(hrList, model.calibrationHR);
            Assert.AreEqual(gsrList, model.calibrationGSR);
        }

        [Test]
        public void getArousalLevel()
        {
            List<double> arousalFuzzySet = new List<double>() { 0.1, 0.2, 0.3, 0.4 };
            Assert.AreEqual(FuzzyModel.ArousalLevel.MidHigh, model.getArousalLevel(arousalFuzzySet));

            arousalFuzzySet = new List<double>() { 0.4, 0.3, 0.2, 0.1 };
            Assert.AreEqual(FuzzyModel.ArousalLevel.MidLow, model.getArousalLevel(arousalFuzzySet));

            arousalFuzzySet = new List<double>() { 0.5, 0.0, 0.0, 0.5 };
            Assert.AreEqual(FuzzyModel.ArousalLevel.MidHigh, model.getArousalLevel(arousalFuzzySet));

            arousalFuzzySet = new List<double>() { 0.1, 0.0, 0.0, 0.0 };
            Assert.AreEqual(FuzzyModel.ArousalLevel.Low, model.getArousalLevel(arousalFuzzySet));
        }
    }
}
