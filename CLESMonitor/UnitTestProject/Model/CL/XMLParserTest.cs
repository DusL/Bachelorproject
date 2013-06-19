using CLESMonitor.Model.CL;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading;

namespace UnitTest.Model.CL
{
    [TestFixture]
    public class XMLParserTest
    {
        XMLParser xmlParser;
        Mock<CTLInputSourceDelegate> mockedDelegate;
        string testInput1, testInput2;

        [SetUp]
        public void setUp()
        {
            xmlParser = new XMLParser();

            testInput1 =
                "<scenario>" +
                    "<second>" +
                    "</second>" +
                "</scenario>";

            testInput2 =
                "<scenario>" +
                    "<second>" +
                        "<event id=\"1\">" +
                            "<name>TREIN_GESTRAND</name>" +
                            "<action>started</action>" +
                        "</event>" +
                        "<task id=\"2\" eventID=\"1\">" +
                            "<name>ARI_UIT</name>" +
                            "<action>started</action>" +
                        "</task>" +
                    "</second>"+
                    "<second>"+
                        "<task id=\"2\" eventID=\"1\">" +
                            "<name>ARI_UIT</name>" +
                            "<action>stopped</action>" +
                        "</task>" +
                        "<event id=\"1\">" +
                            "<name>TREIN_GESTRAND</name>" +
                            "<action>stopped</action>" +
                        "</event>" +
                    "</second>" +
                "</scenario>";

            mockedDelegate = new Mock<CTLInputSourceDelegate>();
            xmlParser.delegateObject = mockedDelegate.Object;
        }

        #region eventsForSecond

        /// <summary>
        /// When method is called with a invalid timespan, return an empty list.
        /// TimeSpan is a struct and therefore cannot be null.
        /// </summary>
        [Test]
        public void eventsForSecond_InvalidTimeSpan()
        {
            xmlParser.loadTextReader(new StringReader(testInput2));
          
            Assert.IsEmpty(xmlParser.elementsForSecond(3));
            Assert.IsEmpty(xmlParser.elementsForSecond(-10));
        }

        /// <summary>
        /// When the timespan is valid than the method returns a list with or without elements.
        /// </summary>
        [Test]
        public void eventsForSecond_NoElementsFound()
        {
            xmlParser.loadTextReader(new StringReader(testInput1));

            Assert.IsEmpty(xmlParser.elementsForSecond(0));
        }

        /// <summary>
        /// When the timespan is valid than the method returns a list with or without elements.
        /// </summary>
        [Test]
        public void eventsForSecond_EventsFound()
        {
            xmlParser.loadTextReader(new StringReader(testInput2));
            int second = 0;

            Assert.IsNotEmpty(xmlParser.elementsForSecond(second));

            Assert.AreEqual("TREIN_GESTRAND", xmlParser.elementsForSecond(second)[0].name);
            Assert.IsNull(xmlParser.elementsForSecond(second)[0].secondaryIndentifier);
            Assert.AreEqual("1", xmlParser.elementsForSecond(second)[0].identifier);
            Assert.AreEqual(InputElement.Type.Event, xmlParser.elementsForSecond(second)[0].type);
            Assert.AreEqual(InputElement.Action.Started, xmlParser.elementsForSecond(second)[0].action);
        }

        /// <summary>
        /// When the timespan is valid and one or more task actions occur in one second, these should be set.
        /// </summary>
        [Test]
        public void eventsForSecond_TasksFound()
        {
            xmlParser.loadTextReader(new StringReader(testInput2));
            int second = 0;

            Assert.IsNotEmpty(xmlParser.elementsForSecond(second));

            Assert.AreEqual("ARI_UIT", xmlParser.elementsForSecond(second)[1].name);
            Assert.AreEqual("1",xmlParser.elementsForSecond(second)[1].secondaryIndentifier);
            Assert.AreEqual("2", xmlParser.elementsForSecond(second)[1].identifier);
            Assert.AreEqual(InputElement.Type.Task, xmlParser.elementsForSecond(second)[1].type);
            Assert.AreEqual(InputElement.Action.Started, xmlParser.elementsForSecond(second)[1].action);
        }

        [Test]
        public void eventsForSecond_countElementsFound()
        {
            xmlParser.loadTextReader(new StringReader(testInput2));

            Assert.AreEqual(2, xmlParser.elementsForSecond(0).Count);
        }

        #endregion

        #region updateTimerCallback

        [Test]
        public void updateTimerCallback_eventHasStarted()
        {
            xmlParser.loadTextReader(new StringReader(testInput2));

            // Check the delegate is called correctly
            InputElement receivedElement = null;
            mockedDelegate.Setup(delegateObject => delegateObject.eventHasStarted(It.IsAny<InputElement>()))
                .Callback((InputElement element) => receivedElement = element);
            xmlParser.updateTimerCallback(null);
            mockedDelegate.Verify(delegateObject => delegateObject.eventHasStarted(It.IsAny<InputElement>()), Times.Once());

            // Check the values passed to the delegate are correct
            InputElement expectedElement = 
                new InputElement("1", "TREIN_GESTRAND", InputElement.Type.Event, InputElement.Action.Started);
            Assert.AreEqual(expectedElement, receivedElement);
        }

        [Test]
        public void updateTimerCallback_eventHasStopped()
        {
            xmlParser.loadTextReader(new StringReader(testInput2));

            // Check the delegate is called correctly
            InputElement receivedElement = null;
            mockedDelegate.Setup(delegateObject => delegateObject.eventHasStopped(It.IsAny<InputElement>()))
                .Callback((InputElement element) => receivedElement = element);
            // Process two seconds
            xmlParser.updateTimerCallback(null);
            xmlParser.updateTimerCallback(null);
            mockedDelegate.Verify(delegateObject => delegateObject.eventHasStopped(It.IsAny<InputElement>()), Times.Once());

            // Check the values passed to the delegate are correct
            InputElement expectedElement =
                new InputElement("1", "TREIN_GESTRAND", InputElement.Type.Event, InputElement.Action.Stopped);
            Assert.AreEqual(expectedElement, receivedElement);
        }

        [Test]
        public void updateTimerCallback_taskHasStarted()
        {
            xmlParser.loadTextReader(new StringReader(testInput2));

            // Check the delegate is called correctly
            InputElement receivedElement = null;
            mockedDelegate.Setup(delegateObject => delegateObject.taskHasStarted(It.IsAny<InputElement>()))
                .Callback((InputElement element) => receivedElement = element);
            xmlParser.updateTimerCallback(null);
            mockedDelegate.Verify(delegateObject => delegateObject.taskHasStarted(It.IsAny<InputElement>()), Times.Once());

            // Check the values passed to the delegate are correct
            InputElement expectedElement =
                new InputElement("2", "ARI_UIT", InputElement.Type.Task, InputElement.Action.Started);
            expectedElement.secondaryIndentifier = "1";
            Assert.AreEqual(expectedElement, receivedElement);
        }

        [Test]
        public void updateTimerCallback_taskHasStopped()
        {
            xmlParser.loadTextReader(new StringReader(testInput2));

            // Check the delegate is called correctly
            InputElement receivedElement = null;
            mockedDelegate.Setup(delegateObject => delegateObject.taskHasStopped(It.IsAny<InputElement>()))
                .Callback((InputElement element) => receivedElement = element);
            // Process two seconds
            xmlParser.updateTimerCallback(null);
            xmlParser.updateTimerCallback(null);
            mockedDelegate.Verify(delegateObject => delegateObject.taskHasStopped(It.IsAny<InputElement>()), Times.Once());

            // Check the values passed to the delegate are correct
            InputElement expectedElement =
                new InputElement("2", "ARI_UIT", InputElement.Type.Task, InputElement.Action.Stopped);
            expectedElement.secondaryIndentifier = "1";
            Assert.AreEqual(expectedElement, receivedElement);
        }

        #endregion
    }
}
