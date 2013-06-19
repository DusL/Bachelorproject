using CLESMonitor.Model.CL;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.Model.CL
{
    [TestFixture]
    public class CTLModelTest
    {
        Mock<CTLInputSource> mockedInputSource;
        Mock<CTLDomain> mockedDomain;
        CTLModel ctlModel;

        InputElement startedEvent;
        InputElement stoppedEvent;
        InputElement startedTask;
        InputElement stoppedTask;
        CTLEvent generatedEvent;
        CTLTask generatedTask;

        CTLTask validTask1, validTask2;
        List<int> usedDomains;

        [SetUp]
        // Initialize any objects needed for the tests contained in this class
        public void Setup()
        {
            mockedInputSource = new Mock<CTLInputSource>();
            mockedDomain = new Mock<CTLDomain>();
            ctlModel = new CTLModel(mockedInputSource.Object, mockedDomain.Object);

            // Setup event-related data
            startedEvent = new InputElement("1", "TEST", InputElement.Type.Event, InputElement.Action.Started);
            generatedEvent = new CTLEvent("1", "TEST", 0, 0);
            mockedDomain.Setup(domain => domain.generateEvent(startedEvent)).Returns(generatedEvent);
            stoppedEvent = new InputElement("1", "TEST", InputElement.Type.Event, InputElement.Action.Stopped);

            // Setup task-related data
            startedTask = new InputElement("2", "TEST", InputElement.Type.Task, InputElement.Action.Started);
            startedTask.secondaryIndentifier = "1";
            generatedTask = new CTLTask("2", "TEST", "1");
            mockedDomain.Setup(domain => domain.generateTask(startedTask)).Returns(generatedTask);
            stoppedTask = new InputElement("2", "TEST", InputElement.Type.Task, InputElement.Action.Stopped);

            // Setup validTasks with which to calculate multitask elements
            validTask1 = new CTLTask("2", "ARI_IN", "1");
            validTask2 = new CTLTask("3", "COMMUNICATIE", "1");

            usedDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUnknown });
        }

        #region CTLInputSourceDelegate methods

        [Test]
        public void eventHasStarted()
        {
            ctlModel.eventHasStarted(startedEvent);

            mockedDomain.Verify(domain => domain.generateEvent(startedEvent), Times.Once());
            Assert.AreEqual(1, ctlModel.activeEvents.Count());
            Assert.AreEqual(generatedEvent, ctlModel.activeEvents[0]);
        }

        [Test]
        public void eventHasStopped()
        {
            ctlModel.eventHasStarted(startedEvent);            
            Assert.IsTrue(ctlModel.activeEvents.Contains(generatedEvent));

            ctlModel.eventHasStopped(stoppedEvent);
            Assert.IsFalse(ctlModel.activeEvents.Contains(generatedEvent));
        }

        [Test]
        public void taskHasStarted()
        {
            ctlModel.eventHasStarted(startedEvent);

            ctlModel.taskHasStarted(startedTask);

            mockedDomain.Verify(domain => domain.generateTask(startedTask), Times.Once());
            Assert.AreEqual(1, ctlModel.activeTasks.Count());
            Assert.AreEqual(generatedTask, ctlModel.activeTasks[0]);
        }

        [Test]
        public void taskHasEnded()
        {
            ctlModel.eventHasStarted(startedEvent);

            ctlModel.taskHasStarted(startedTask);
            Assert.IsTrue(ctlModel.activeTasks.Contains(generatedTask));

            ctlModel.taskHasStopped(stoppedTask);
            Assert.IsFalse(ctlModel.activeTasks.Contains(generatedTask));
        }

        #endregion

        #region multitaskDomain
        /// <summary>
        /// When there is overlap in domains, only add them once
        /// </summary>
        [Test]
        public void multitaskDomain_OverlappingDomains()
        {
            // Both tasks contain the same domains
            validTask1.informationDomains = usedDomains;
            validTask2.informationDomains = usedDomains;

            Assert.AreEqual(usedDomains, CTLModel.multitaskDomain(validTask1, validTask2));

            // validTask1 contains the same domains as validTask2, with one extra domain 
            validTask1.informationDomains.Add((int)InformationDomain.InformationDomainUsingInterface);
            usedDomains.Add((int)InformationDomain.InformationDomainUsingInterface);

            Assert.AreEqual(usedDomains, CTLModel.multitaskDomain(validTask1, validTask2));
        }
        /// <summary>
        /// When no infomationdomain is present in the list of informationDomains of both tasks, the method returns a list 
        /// of domains from both tasks combined.
        /// </summary>
        [Test]
        public void multitaskDomain_DifferentDomains()
        {
            // The tasks have no domains in common, both domains are in usedDomains
            usedDomains.Add((int)InformationDomain.InformationDomainExternalContact );
            validTask1.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUnknown });
            validTask2.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainExternalContact});

            Assert.AreEqual(CTLModel.multitaskDomain(validTask1, validTask2), usedDomains);

            // The tasks have 1 domain in common, all 3 domains are in usedDomains
            usedDomains.Add((int)InformationDomain.InformationDomainUsingInterface);
            CTLTask validTask3 = new CTLTask("4", "ARI_UIT", "1");
            validTask3.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface, (int)InformationDomain.InformationDomainExternalContact });
            
            Assert.AreEqual(usedDomains, CTLModel.multitaskDomain(validTask1, validTask3));
        }
        /// <summary>
        /// A task should always have a set information domain, so when this is not the case,
        /// return null. Also return null when at least one of the tasks is null.
        /// </summary>
        [Test]
        public void multitaskDomain_Null()
        {
            validTask1.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface });
            validTask2.informationDomains = null;

            Assert.AreEqual(null, CTLModel.multitaskDomain(validTask1, validTask2));
            Assert.AreEqual(null, CTLModel.multitaskDomain(validTask2, validTask1));
            Assert.AreEqual(null, CTLModel.multitaskDomain(validTask1, null));
            Assert.AreEqual(null, CTLModel.multitaskDomain(null, validTask2));
        }

        #endregion

        #region multitaksMO

        /// <summary>
        /// When one of two tasks is null, return 0.0
        /// </summary>
        [Test]
        public void multitaskMO_Null()
        {
            validTask1 = null;

            Assert.AreEqual(0.0, CTLModel.multitaskMO(validTask1,validTask2));
            Assert.AreEqual(0.0, CTLModel.multitaskMO(validTask1, null));
        }

        /// <summary>
        /// Test for valid values of MO for each task.
        /// </summary>
        [Test]
        public void multitaskMO_ValidMO()
        {
            validTask1.moValue = .5;
            validTask2.moValue = .2;

            Assert.AreEqual(1.0, CTLModel.multitaskMO(validTask1, validTask2));

            validTask2.moValue = .7;
            Assert.AreEqual(1.2, CTLModel.multitaskMO(validTask1, validTask2));
        }

        #endregion

        #region multitaskLip
        /// <summary>
        /// Test for null input
        /// </summary>
        [Test]
        public void multitaskLip_Null()
        {
            validTask1 = null;

            Assert.AreEqual(0, CTLModel.multitaskLip(validTask1, validTask2));
            Assert.AreEqual(0, CTLModel.multitaskLip(validTask1, null));
        }
        
        /// <summary>
        /// Test for valid Lip values
        /// </summary>
        [Test]
        public void multitaskLip_ValidLip()
        {
            validTask1.lipValue = 1;
            validTask2.lipValue = 1;

            Assert.AreEqual(1, CTLModel.multitaskLip(validTask1, validTask2));

            validTask2.lipValue = 3;
            Assert.AreEqual(3, CTLModel.multitaskLip(validTask1, validTask2));
        }

        #endregion

        #region setTimesForMultitask

        [Test]
        public void setTimesForMultitask_NullInput()
        { 
            
        }


        #endregion


        [Test]
        public void calculateOverallLip_ValidInput()
        {
            validTask1.startTime = new TimeSpan(0, 0, 1);
            validTask1.endTime = new TimeSpan(0, 0, 3);
            validTask1.lipValue = 1;

            validTask2.startTime = new TimeSpan(0, 0, 2);
            validTask2.endTime = new TimeSpan(0, 0, 4);
            validTask2.lipValue = 3;

            TimeSpan lengthTimeFrame = new TimeSpan(0, 0, 10);

            double lip = ((2.0 + 6.0)/10.0);

            List<CTLTask> tasks = new List<CTLTask> (new CTLTask[] {validTask1, validTask2});
            Assert.AreEqual(lip, CTLModel.calculateOverallLip(tasks, lengthTimeFrame));
        }

        [Test]
        public void calculateOverallMo_ValidInput()
        {
            validTask1.startTime = new TimeSpan(0, 0, 1);
            validTask1.endTime = new TimeSpan(0, 0, 3);
            validTask1.moValue = .6;

            validTask2.startTime = new TimeSpan(0, 0, 2);
            validTask2.endTime = new TimeSpan(0, 0, 4);
            validTask2.moValue = .3;

            TimeSpan lengthTimeFrame = new TimeSpan(0, 0, 10);

            double mo = ((0.6*2.0 + 0.3 * 2.0) / 10.0);

            List<CTLTask> tasks = new List<CTLTask>(new CTLTask[] { validTask1, validTask2 });
            Assert.AreEqual(mo, CTLModel.calculateOverallMo(tasks, lengthTimeFrame));
        }

        [Test]
        public void calculateTSS_ValidInput()
        {
            validTask1.startTime = new TimeSpan(0, 0, 1);
            validTask1.endTime = new TimeSpan(0, 0, 3);
            validTask1.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface});

            validTask2.startTime = new TimeSpan(0, 0, 2);
            validTask2.endTime = new TimeSpan(0, 0, 4);
            validTask2.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUnknown});

            double tss = 2.0 / 2.0;

            List<CTLTask> tasks = new List<CTLTask>(new CTLTask[] { validTask1, validTask2 });
            Assert.AreEqual(tss, CTLModel.calculateTSS(tasks));
        }

        [Test]
        public void calculateModelValue_AllZero()
        {
            double lip = 1.0;
            double mo = 0.0;
            double tss = 0.0;


        }

        [TearDown]
        // Removes all objects generated during setUp and the tests so that only the original objects are present
        // after testing
        public void TearDown()
        { 
        
        }
    }
}
