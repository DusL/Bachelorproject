using System;
using System.IO;
using NUnit.Framework;
using Moq;

using CLESMonitor.Model;
using System.Threading;




namespace UnitTest.Model
{
    /// <summary>
    ///
    /// </summary>
    [TestFixture]
    public class XMLFileTaskParserTest
    {
        TimeSpan timeSpan;
        XMLFileTaskParser parser;

        string testInput1;
        string testInput2;
        string testInput3;

        [SetUp]
        public void setUp()
        {
            
            parser = new XMLFileTaskParser();
            timeSpan = new TimeSpan(0, 0, 1); //hours, minutes, seconds

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

            testInput3 =
                "<scenario>" +
                    "<second id=\"1\">" +
                        "<event id=\"1\">" +
                            "<name>TREIN_GESTRAND</name>" +
                            "<action>started</action>" +
                        "</event>"+
                    "</second>" +
                "</scenario>";
        }

        #region eventsForTime

        /// <summary>
        /// When method is called with a invalid timespan, return an empty list.
        /// TimeSpan is a struct and therefore cannot be null.
        /// </summary>
        [Test]
        public void elementsForTime_InvalidTimeSpan()
        {
            parser.loadTextReader(new StringReader(testInput2));
          
            Assert.IsEmpty(parser.elementsForTime(new TimeSpan(0, 0, 3)));
            Assert.IsEmpty(parser.elementsForTime(new TimeSpan(0, 0, -10)));
        }

        /// <summary>
        /// When the timespan is valid than the method returns a list with or without elements.
        /// </summary>
        [Test]
        public void elementsForTime_NoElementsFound()
        {
            parser.loadTextReader(new StringReader(testInput1));

            TimeSpan timeSpan = new TimeSpan(0, 0, 0);
            Assert.IsEmpty(parser.elementsForTime(timeSpan));
        }

        /// <summary>
        /// When the timespan is valid than the method returns a list with or without elements.
        /// </summary>
        [Test]
        public void elementsForTime_EventsFound()
        {
            parser.loadTextReader(new StringReader(testInput2));
            TimeSpan timeSpan = new TimeSpan(0, 0, 0);

            Assert.IsNotEmpty(parser.elementsForTime(timeSpan));

            Assert.AreEqual("TREIN_GESTRAND", parser.elementsForTime(timeSpan)[0].name);
            Assert.IsNull(parser.elementsForTime(timeSpan)[0].secondaryIndentifier);
            Assert.AreEqual("1", parser.elementsForTime(timeSpan)[0].identifier);
            Assert.AreEqual(InputElement.Type.Event, parser.elementsForTime(timeSpan)[0].type);
            Assert.AreEqual(InputElement.Action.Started, parser.elementsForTime(timeSpan)[0].action);
        }

        /// <summary>
        /// When the timespan is valid and one or more task actions occur in one second, these should be set.
        /// </summary>
        [Test]
        public void elementsForTime_TasksFound()
        {
            parser.loadTextReader(new StringReader(testInput2));
            TimeSpan timeSpan = new TimeSpan(0, 0, 0);

            Assert.IsNotEmpty(parser.elementsForTime(timeSpan));

            Assert.AreEqual("ARI_UIT", parser.elementsForTime(timeSpan)[1].name);
            Assert.AreEqual("1",parser.elementsForTime(timeSpan)[1].secondaryIndentifier);
            Assert.AreEqual("2", parser.elementsForTime(timeSpan)[1].identifier);
            Assert.AreEqual(InputElement.Type.Task, parser.elementsForTime(timeSpan)[1].type);
            Assert.AreEqual(InputElement.Action.Started, parser.elementsForTime(timeSpan)[1].action);
        }

        [Test]
        public void elementsForTime_countElementsFound()
        {
            parser.loadTextReader(new StringReader(testInput2));
            TimeSpan timeSpan = new TimeSpan(0, 0, 0);

            Assert.AreEqual(2, parser.elementsForTime(timeSpan).Count);
        }

        #endregion

        [Test]
        public void updateTimerCallback()
        {
            Mock<CTLInputSourceDelegate> mockedDelegate = new Mock<CTLInputSourceDelegate>();
            parser.delegateObject = mockedDelegate.Object;
            parser.loadTextReader(new StringReader(testInput3));

            InputElement receivedElement = null;
            mockedDelegate.Setup(delegateObject => delegateObject.eventHasStarted(It.IsAny<InputElement>()))
                .Callback((InputElement element) => receivedElement = element);

            parser.updateTimerCallback(null);

            mockedDelegate.Verify(delegateObject => delegateObject.eventHasStarted(It.IsAny<InputElement>()), Times.Once());

            InputElement expectedElement = 
                new InputElement("1", "TREIN_GESTRAND", InputElement.Type.Event, InputElement.Action.Started);
            Assert.AreEqual(expectedElement, receivedElement);
        }

        
    }
}
