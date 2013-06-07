using System;
using System.IO;
using NUnit.Framework;

using CLESMonitor.Model;

namespace UnitTest.Model
{
    /// <summary>
    /// We only test eventsStarted and taskStarted cases since eventsStopped, and tasksStopped use the same methods
    /// </summary>
    [TestFixture]
    public class XMLFileTaskParserTest
    {
        TimeSpan timeSpan;
        XMLFileTaskParser parser;

        string testInput1;
        string testInput2;

        [SetUp]
        public void setUp()
        {
            parser = new XMLFileTaskParser();
            timeSpan = new TimeSpan(0, 0, 1); //hours, minutes, seconds

            testInput1 =
                "<xml>" +
                    "<second>" +
                    "</second>" +
                "</xml>";

            testInput2 =
                "<xml>" +
                    "<second>" +
                        "<event id=\"1\">" +
                            "<name>TREIN_GESTRAND</name>" +
                            "<action>started</action>" +
                        "</event>" +
                        "<task id=\"2\">" +
                            "<name>ARI_UIT</name>" +
                            "<action>started</action>" +
                        "</task>" +
                        "<task id=\"2\">" +
                            "<name>ARI_UIT</name>" +
                            "<action>stopped</action>" +
                        "</task>" +
                        "<event id=\"1\">" +
                            "<name>TREIN_GESTRAND</name>" +
                            "<action>stopped</action>" +
                        "</event>" +
                    "</second>" +
                "</xml>";
        }

        #region eventsForTime

        /// <summary>
        /// When method is called with a invalid timespan, return an empty list.
        /// TimeSpan is a struct and therefore cannot be null.
        /// </summary>
        [Test]
        public void eventsForTime_InvalidTimeSpan()
        {
            parser.loadTextReader(new StringReader(testInput2));
          
            Assert.IsEmpty(parser.eventsForTime(new TimeSpan(0, 0, 3), ActionType.EventStarted));
            Assert.IsEmpty(parser.eventsForTime(new TimeSpan(0, 0, 3), ActionType.EventStopped));

            Assert.IsEmpty(parser.eventsForTime(new TimeSpan(0, 0, -10), ActionType.EventStarted));
            Assert.IsEmpty(parser.eventsForTime(new TimeSpan(0, 0, -10), ActionType.EventStopped));
        }

        /// <summary>
        /// When the timespan is valid than the method returns a list with or without elements.
        /// </summary>
        [Test]
        public void eventsForTime_NoEventsFound()
        {
            parser.loadTextReader(new StringReader(testInput1));

            TimeSpan timeSpan = new TimeSpan(0, 0, 0);
            Assert.IsEmpty(parser.eventsForTime(timeSpan, ActionType.EventStarted));
            Assert.IsEmpty(parser.eventsForTime(timeSpan, ActionType.EventStopped));
        }

        /// <summary>
        /// When the timespan is valid than the method returns a list with or without elements.
        /// </summary>
        [Test]
        public void eventsForTime_EventsFound()
        {
            parser.loadTextReader(new StringReader(testInput2));
            TimeSpan timeSpan = new TimeSpan(0, 0, 0);

            Assert.AreEqual(1, parser.eventsForTime(timeSpan, ActionType.EventStarted).Count);
            Assert.AreEqual(1, parser.eventsForTime(timeSpan, ActionType.EventStopped).Count);

            Assert.AreEqual("TREIN_GESTRAND", parser.eventsForTime(timeSpan, ActionType.EventStarted)[0].type);
            Assert.AreEqual("TREIN_GESTRAND", parser.eventsForTime(timeSpan, ActionType.EventStopped)[0].type);

            Assert.AreEqual("1", parser.eventsForTime(timeSpan, ActionType.EventStarted)[0].identifier);
            Assert.AreEqual("1", parser.eventsForTime(timeSpan, ActionType.EventStopped)[0].identifier);
        }

        #endregion

        #region tasksForTime

        /// <summary>
        /// When method is called with a invalid timespan, return an empty list.
        /// TimeSpan is a struct and therefore cannot be null.
        /// </summary>
        [Test]
        public void tasksForTime_InvalidTimeSpan()
        {
            parser.loadTextReader(new StringReader(testInput2));

            Assert.IsEmpty(parser.tasksForTime(new TimeSpan(0, 0, 3), ActionType.TaskStarted));
            Assert.IsEmpty(parser.tasksForTime(new TimeSpan(0, 0, 3), ActionType.TaskStopped));

            Assert.IsEmpty(parser.tasksForTime(new TimeSpan(0, 0, -10), ActionType.TaskStarted));
            Assert.IsEmpty(parser.tasksForTime(new TimeSpan(0, 0, -10), ActionType.TaskStopped));
        }

        /// <summary>
        /// When the timespan is valid than the method returns a list with or without elements.
        /// </summary>
        [Test]
        public void tasksForTime_NoEventsFound()
        {
            parser.loadTextReader(new StringReader(testInput1));

            TimeSpan timeSpan = new TimeSpan(0, 0, 0);
            Assert.IsEmpty(parser.tasksForTime(timeSpan, ActionType.TaskStarted));
            Assert.IsEmpty(parser.tasksForTime(timeSpan, ActionType.TaskStopped));
        }

        /// <summary>
        /// When the timespan is valid than the method returns a list with or without elements.
        /// </summary>
        [Test]
        public void tasksForTime_EventsFound()
        {
            parser.loadTextReader(new StringReader(testInput2));
            TimeSpan timeSpan = new TimeSpan(0, 0, 0);

            Assert.AreEqual(1, parser.tasksForTime(timeSpan, ActionType.TaskStarted).Count);
            Assert.AreEqual(1, parser.tasksForTime(timeSpan, ActionType.TaskStopped).Count);

            Assert.AreEqual("ARI_UIT", parser.tasksForTime(timeSpan, ActionType.TaskStarted)[0].type);
            Assert.AreEqual("ARI_UIT", parser.tasksForTime(timeSpan, ActionType.TaskStopped)[0].type);

            Assert.AreEqual("2", parser.tasksForTime(timeSpan, ActionType.TaskStarted)[0].identifier);
            Assert.AreEqual("2", parser.tasksForTime(timeSpan, ActionType.TaskStopped)[0].identifier);
        }

        #endregion
    }
}
