using CLESMonitor.Model.CL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UnitTest.Model
{
    [TestFixture]
    public class CTLMathTest
    {
        CTLTask validTask1, validTask2;
        List<int> usedDomains;

        [SetUp]
        // Initialize any objects needed for the tests contained in this class
        public void Setup()
        {
            // Setup validTasks with which to calculate multitask elements
            validTask1 = new CTLTask("2", "ARI_IN");
            validTask2 = new CTLTask("3", "COMMUNICATIE");

            usedDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUnknown });
        }
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

            Assert.AreEqual(usedDomains, CTLMath.multitaskDomain(validTask1, validTask2));

            // validTask1 contains the same domains as validTask2, with one extra domain 
            validTask1.informationDomains.Add((int)InformationDomain.InformationDomainUsingInterface);
            usedDomains.Add((int)InformationDomain.InformationDomainUsingInterface);

            Assert.AreEqual(usedDomains, CTLMath.multitaskDomain(validTask1, validTask2));
        }
        /// <summary>
        /// When no infomationdomain is present in the list of informationDomains of both tasks, the method returns a list 
        /// of domains from both tasks combined.
        /// </summary>
        [Test]
        public void multitaskDomain_DifferentDomains()
        {
            // The tasks have no domains in common, both domains are in usedDomains
            usedDomains.Add((int)InformationDomain.InformationDomainExternalContact);
            validTask1.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUnknown });
            validTask2.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainExternalContact });

            Assert.AreEqual(CTLMath.multitaskDomain(validTask1, validTask2), usedDomains);

            // The tasks have 1 domain in common, all 3 domains are in usedDomains
            usedDomains.Add((int)InformationDomain.InformationDomainUsingInterface);
            CTLTask validTask3 = new CTLTask("4", "ARI_UIT");
            validTask3.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface, (int)InformationDomain.InformationDomainExternalContact });

            Assert.AreEqual(usedDomains, CTLMath.multitaskDomain(validTask1, validTask3));
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

            Assert.AreEqual(null, CTLMath.multitaskDomain(validTask1, validTask2));
            Assert.AreEqual(null, CTLMath.multitaskDomain(validTask2, validTask1));
            Assert.AreEqual(null, CTLMath.multitaskDomain(validTask1, null));
            Assert.AreEqual(null, CTLMath.multitaskDomain(null, validTask2));
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

            Assert.AreEqual(0.0, CTLMath.multitaskMO(validTask1, validTask2));
            Assert.AreEqual(0.0, CTLMath.multitaskMO(validTask1, null));
        }

        /// <summary>
        /// Test for valid values of MO for each task.
        /// </summary>
        [Test]
        public void multitaskMO_ValidMO()
        {
            validTask1.moValue = .5;
            validTask2.moValue = .2;

            Assert.AreEqual(0.7, CTLMath.multitaskMO(validTask1, validTask2));

            validTask2.moValue = .7;
            Assert.AreEqual(1, CTLMath.multitaskMO(validTask1, validTask2));
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

            Assert.AreEqual(0, CTLMath.multitaskLip(validTask1, validTask2));
            Assert.AreEqual(0, CTLMath.multitaskLip(validTask1, null));
        }

        /// <summary>
        /// Test for valid Lip values
        /// </summary>
        [Test]
        public void multitaskLip_ValidLip()
        {
            validTask1.lipValue = 1;
            validTask2.lipValue = 1;

            Assert.AreEqual(1, CTLMath.multitaskLip(validTask1, validTask2));

            validTask2.lipValue = 3;
            Assert.AreEqual(3, CTLMath.multitaskLip(validTask1, validTask2));
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

            double lip = ((2.0 + 6.0) / 10.0);

            List<CTLTask> tasks = new List<CTLTask>(new CTLTask[] { validTask1, validTask2 });
            Assert.AreEqual(lip, CTLMath.calculateOverallLip(tasks, lengthTimeFrame));
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

            double mo = ((0.6 * 2.0 + 0.3 * 2.0) / 10.0);

            List<CTLTask> tasks = new List<CTLTask>(new CTLTask[] { validTask1, validTask2 });
            Assert.AreEqual(mo, CTLMath.calculateOverallMo(tasks, lengthTimeFrame));
        }

        [Test]
        public void calculateTSS_ValidInput()
        {
            validTask1.startTime = new TimeSpan(0, 0, 1);
            validTask1.endTime = new TimeSpan(0, 0, 3);
            validTask1.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUsingInterface });

            validTask2.startTime = new TimeSpan(0, 0, 2);
            validTask2.endTime = new TimeSpan(0, 0, 4);
            validTask2.informationDomains = new List<int>(new int[] { (int)InformationDomain.InformationDomainUnknown });

            double tss = 2.0 / 2.0;

            List<CTLTask> tasks = new List<CTLTask>(new CTLTask[] { validTask1, validTask2 });
            Assert.AreEqual(tss, CTLMath.calculateOverallTSS(tasks));
        }


        #region calculateMentalWorkLoad

        /// <summary>
        /// Test with all values set tp the lowest possible value and a variable amount of tasks in the calculationFrame
        /// </summary>
        [Test]
        public void calculateMentalWorkLoad_AllLowest()
        {
            double lip = 1.0;
            double mo = 0.0;
            double tss = 0.0;
       
            double MWL0 = CTLMath.calculateMentalWorkLoad(lip, mo, tss, 0);
            double MWL1 = CTLMath.calculateMentalWorkLoad(lip, mo, tss, 1);
            double MWL2 = CTLMath.calculateMentalWorkLoad(lip, mo, tss, 2);

            Assert.AreEqual(0, MWL0);
            Assert.AreEqual(0, MWL1);
            Assert.AreEqual(0, MWL2);

        }

        [Test]
        public void calculateMentalWorkLoad_ValidInput()
        {
            double lip = 2.0;
            double mo = 0.5;
            double tss = 0.0;

            double MWL1 = CTLMath.calculateMentalWorkLoad(lip, mo, tss, 1);
            Assert.AreEqual(0.4209, Math.Round(MWL1, 4));

            lip = 3.0;
            mo = 0.5;
            tss = 1.0;

            double MWL2 = CTLMath.calculateMentalWorkLoad(lip, mo, tss, 2);
            Assert.AreEqual(1.1217, Math.Round(MWL2, 4));
        }


        #endregion
    }
}
