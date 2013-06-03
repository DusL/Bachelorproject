using System;

using NUnit.Framework;
using NUnit.Mocks;

using CLESMonitor.Model;
using CLESMonitor.Controller;

namespace UnitTest.Model
{
    [TestFixture]
    public class PRLDomainTest
    {
        PRLDomain domain;
        ParsedEvent parsedEvent;
        ParsedTask parsedTask;

        [SetUp]
        public void setUp()
        {
            domain = new PRLDomain();
        }
        
        /// <summary>
        /// If a null struct is presented, generateEvent should return null aswell
        /// </summary>
        [Test]     
        public void generateEventNull()
        {
            parsedEvent = new ParsedEvent(null,null);
            Assert.IsNull(domain.generateEvent(parsedEvent));
        }

        /// <summary>
        /// When the identifier is null, a null-object is returned 
        /// </summary>
        [Test]
        public void generateEventNullIdentifier()
        {
           parsedEvent = new ParsedEvent(null, "VERTRAAGDE_TREIN");
           Assert.IsNull(domain.generateEvent(parsedEvent));
        }

        /// <summary>
        /// The struct contains an existing event type; this should return an event that is not null
        /// </summary>
        [Test]
        public void generateEventExistingType()
        {
            parsedEvent = new ParsedEvent("0", "VERTRAAGDE_TREIN");
            Assert.AreEqual(parsedEvent.type, domain.generateEvent(parsedEvent).type);
        }

        /// <summary>
        /// If the type does not exist an Event object with value null is returned.
        /// </summary>
        [Test]
        public void generateEventNonExistingType()
        {
            parsedEvent = new ParsedEvent("0", "NON_EXISTING");
            Assert.IsNull(domain.generateEvent(parsedEvent));
        }

        /// <summary>
        /// When trying to generate a taks with a struct that has not been set, retur null
        /// </summary>
        [Test]
        public void generateTaskNull()
        {
            parsedTask = new ParsedTask(null,null);
            Assert.IsNull(domain.generateTask(parsedTask));
        }

        /// <summary>
        /// When the identifier is null, a null-object is returned 
        /// </summary>
        [Test]
        public void generateTaskNullIdentifier()
        {
            parsedTask = new ParsedTask(null, "ARI_IN");
            Assert.IsNull(domain.generateTask(parsedTask));
        }

        /// <summary>
        /// Generate a task with identifier != null and an existing type
        /// </summary>
        [Test]
        public void generateTaskExistingType()
        {
            parsedTask = new ParsedTask("0", "ARI_IN");
            Assert.AreEqual(parsedTask.type, domain.generateTask(parsedTask).type);
        }

        /// <summary>
        /// Given a struct that has not been created (thus null), return null
        /// </summary>
        [Test]
        public void generateTaskNonExistingType()
        {
            parsedTask = new ParsedTask("0", "NON_EXISTING");
            Assert.IsNull(domain.generateTask(parsedTask));
        }

        [TearDown]
        public void tearDown()
        {
            domain = null;    
        }
    }
}
