using System;
using NUnit.Framework;
using NUnit.Mocks;

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
        DynamicMock parserMock;

        [SetUp]
        public void setUp()
        {
            parser = new XMLFileTaskParser();
            timeSpan = new TimeSpan(0, 0, 10); //hours, minutes, seconds
        }

        /// <summary>
        /// When eventsStarted is called with a negative timestamp, return an empty list.
        /// </summary>
        [Test]
        public void eventsStartedNegativeTimeSpan()
        {
            TimeSpan span1 = new TimeSpan(0, 0, 8); //hours, minutes, seconds
            Assert.IsEmpty(parser.eventsStarted(span1 - timeSpan));
        }

        /// <summary>
        /// When the timespan is nill or positive than the method returns a list with or without elements.
        /// </summary>
        [Test]
        public void eventsStartedNonNegativeTimeSpan()
        {
            //TODO: HOE TE TESTEN
           
            TimeSpan span1 = timeSpan;
        }

        /// <summary>
        /// When tasksStarted is called with a negative timestamp, return an empty list.
        /// </summary>
        [Test]
        public void tasksStartedNegativeTimeSpan()
        {
            TimeSpan span1 = new TimeSpan(0, 0, 8); //hours, minutes, seconds
            Assert.IsEmpty(parser.eventsStarted(span1 - timeSpan));
        }

        //TODO: Zelfde fix voor de taken.
    }
}
