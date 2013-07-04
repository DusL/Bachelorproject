using CLESMonitor.Model.CL;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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

        public void CallBack()
        {
            ctlModel.startSession(Timeout.Infinite, Timeout.Infinite);
            List<CTLTask> tasksInCalculationFrame = new List<CTLTask>(new CTLTask[] { generatedTask});
            TimeSpan sessionTime = new TimeSpan(0, 0, 15);
            TimeSpan lengthTimeFrame = new TimeSpan(0, 0, 10);

            generatedTask.endTime = new TimeSpan(0, 0, 4);

            ctlModel.updateTimerCallback(null);


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



        #region setTimesForMultitask

        [Test]
        public void setTimesForMultitask_NullInput()
        { 
            
        }


        #endregion




        [TearDown]
        // Removes all objects generated during setUp and the tests so that only the original objects are present
        // after testing
        public void TearDown()
        { 
        
        }
    }
}
