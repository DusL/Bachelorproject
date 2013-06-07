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
            Assert.IsNull(domain.generateEvent(new InputElement(null, "ANY_TYPE")));
            Assert.IsNull(domain.generateEvent(new InputElement("ANY_IDENTIFIER", null)));
            Assert.IsNull(domain.generateEvent(new InputElement(null, null)));
        }

        /// <summary>
        /// When InputElement contains an valid type, a valid CTLEvent should be generated
        /// </summary>
        [Test]
        public void generateEvent_ExistingType()
        {
            InputElement validInputElement = new InputElement("0", "VERTRAAGDE_TREIN");
            CTLEvent ctlEvent = domain.generateEvent(validInputElement);
            Assert.IsNotNull(ctlEvent);
            Assert.AreEqual(validInputElement.identifier, ctlEvent.identifier);
            Assert.AreEqual(validInputElement.type, ctlEvent.name);
        }

        /// <summary>
        /// When InputElement contains an non-existing type, method should return null
        /// </summary>
        [Test]
        public void generateEvent_NonExistingType()
        {
            InputElement invalidInputElement = new InputElement("0", "NONEXISTING_TYPE");
            Assert.IsNull(domain.generateEvent(invalidInputElement));
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
            Assert.IsNull(domain.generateTask(new InputElement(null, "ANY_TYPE")));
            Assert.IsNull(domain.generateTask(new InputElement("ANY_IDENTIFIER", null)));
            Assert.IsNull(domain.generateTask(new InputElement(null, null)));
        }

        /// <summary>
        /// Generate a task with identifier != null and an existing type
        /// </summary>
        [Test]
        public void generateTask_ExistingType()
        {
            InputElement validInputElement = new InputElement("0", "ARI_UIT");
            CTLTask ctlTask = domain.generateTask(validInputElement);
            Assert.IsNotNull(ctlTask);
            Assert.AreEqual(validInputElement.identifier, ctlTask.identifier);
            Assert.AreEqual(validInputElement.type, ctlTask.name);
        }

        /// <summary>
        /// When InputElement contains an non-existing type, method should return null
        /// </summary>
        [Test]
        public void generateTask_NonExistingType()
        {
            InputElement invalidInputElement = new InputElement("0", "NONEXISTING_TYPE");
            Assert.IsNull(domain.generateTask(invalidInputElement));
        }

        #endregion

        [TearDown]
        public void tearDown()
        {
            domain = null;    
        }
    }
}
