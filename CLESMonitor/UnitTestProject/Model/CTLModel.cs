using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Moq;

using CLESMonitor.Model;

namespace UnitTest.Model
{
    [TestFixture]
    public class CTLModel
    {
        Mock<CTLInputSource> mockedInputSource;
        Mock<CTLDomain> mockedDomain;
        CLESMonitor.Model.CTLModel ctlModel;

        [SetUp]
        //Initialize any objects needed for the tests contained in this class
        public void Setup()
        {
            mockedInputSource = new Mock<CTLInputSource>();
            mockedDomain = new Mock<CTLDomain>();
            ctlModel = new CLESMonitor.Model.CTLModel(mockedInputSource.Object, mockedDomain.Object);
        }

        [Test]
        public void eventHasStarted()
        {
            InputElement inputElement = new InputElement("1", "TEST", InputElement.Type.Event, InputElement.Action.Started);
            CTLEvent ctlEvent = new CTLEvent("1", "TEST", 0, 0);
            mockedDomain.Setup(domain => domain.generateEvent(inputElement)).Returns(ctlEvent);

            ctlModel.eventHasStarted(inputElement);

            mockedDomain.Verify(domain => domain.generateEvent(inputElement), Times.Once());
            Assert.AreEqual(1, ctlModel.activeEvents.Count());
            Assert.AreEqual(ctlEvent, ctlModel.activeEvents[0]);
        }

        [TearDown]
        // Removes all objects generated during setUp and the tests so that only the original objects are present
        // after testing
        public void TearDown()
        { 
        
        }
    }
}
