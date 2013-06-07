using CLESMonitor.Controller;
using CLESMonitor.Model;
using NUnit.Framework;
using System;

namespace UnitTest.Model
{
    [TestFixture]
    public class PRLDomainTest
    {
        PRLDomain domain;

        [SetUp]
        public void setUp()
        {
            domain = new PRLDomain();
        }

        #region generateEvent

        /// <summary>
        /// If any null values are given, method should return null
        /// </summary>
        [Test]
        public void generateEvent_NullValues()
        {
            Assert.IsNull(domain.generateEvent(null));
            Assert.IsNull(domain.generateEvent(new ParsedEvent(null, "ANY_TYPE")));
            Assert.IsNull(domain.generateEvent(new ParsedEvent("ANY_IDENTIFIER", null)));
            Assert.IsNull(domain.generateEvent(new ParsedEvent(null, null)));
        }

        /// <summary>
        /// When parsedEvent contains an valid type, a valid CTLEvent should be generated
        /// </summary>
        [Test]
        public void generateEvent_ExistingType()
        {
            ParsedEvent validParsedEvent = new ParsedEvent("0", "VERTRAAGDE_TREIN");
            CTLEvent ctlEvent = domain.generateEvent(validParsedEvent);
            Assert.IsNotNull(ctlEvent);
            Assert.AreEqual(validParsedEvent.identifier, ctlEvent.identifier);
            Assert.AreEqual(validParsedEvent.type, ctlEvent.type);
        }

        /// <summary>
        /// When parsedEvent contains an non-existing type, method should return null
        /// </summary>
        [Test]
        public void generateEvent_NonExistingType()
        {
            ParsedEvent invalidParsedEvent = new ParsedEvent("0", "NONEXISTING_TYPE");
            Assert.IsNull(domain.generateEvent(invalidParsedEvent));
        }

        #endregion

        #region generateTask

        /// <summary>
        /// If any null values are given, method should return null
        /// </summary>
        [Test]
        public void generateTask_NullValues()
        {
            Assert.IsNull(domain.generateTask(null));
            Assert.IsNull(domain.generateTask(new ParsedTask(null, "ANY_TYPE")));
            Assert.IsNull(domain.generateTask(new ParsedTask("ANY_IDENTIFIER", null)));
            Assert.IsNull(domain.generateTask(new ParsedTask(null, null)));
        }

        /// <summary>
        /// Generate a task with identifier != null and an existing type
        /// </summary>
        [Test]
        public void generateTask_ExistingType()
        {
            ParsedTask validParsedTask = new ParsedTask("0", "ARI_UIT");
            CTLTask ctlTask = domain.generateTask(validParsedTask);
            Assert.IsNotNull(ctlTask);
            Assert.AreEqual(validParsedTask.identifier, ctlTask.identifier);
            Assert.AreEqual(validParsedTask.type, ctlTask.type);
        }

        /// <summary>
        /// When parsedTask contains an non-existing type, method should return null
        /// </summary>
        [Test]
        public void generateTask_NonExistingType()
        {
            ParsedTask invalidParsedTask = new ParsedTask("0", "NONEXISTING_TYPE");
            Assert.IsNull(domain.generateTask(invalidParsedTask));
        }

        #endregion

        [TearDown]
        public void tearDown()
        {
            domain = null;    
        }
    }
}
