using System;
using NUnit.Framework;
using CLESMonitor.Model.ES;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest.Model.ES
{
    [TestFixture]
    public class FuzzyCalculateTest
    {
        double mean;
        double sd;
        double normalised;
        double normalised2;

        double result;
        double result2;

        double expected;
        List<double> inputList;

        FuzzyCalculate calc;

        [SetUp]
        public void setUp()
        {
            mean = 20;
            sd = 4;
            calc = new FuzzyCalculate(); 
        }


        #region GSR

        #region lowGSRValue

        /// <summary>
        /// If the normalised value falls within the boundary, return a value between 0 and 1.
        /// </summary>
        [Test]
        public void lowGSRValue_WithinBounds()
        {
            normalised = mean - 2 * sd;

            result = calc.lowGSRValue(mean, sd, normalised);
            Assert.LessOrEqual(result, 1);
            Assert.GreaterOrEqual(result, 0);
        }

        /// <summary>
        /// If the normalised value is out of bounds, return 0.
        /// </summary>
        [Test]
        public void lowGSRValue_OutOfBounds()
        {
            normalised = mean;
            normalised2 = -20;

            result = calc.lowGSRValue(mean, sd, normalised);
            Assert.AreEqual(0, result);

            result2 = calc.lowGSRValue(mean, sd, normalised);
            Assert.AreEqual(0, result2);
        }

        /// <summary>
        /// If the normalised value is equal to the boundary value, return 0.
        /// </summary>
        [Test]
        public void lowGSRValue_OnBoundary()
        {
            normalised = mean - 1.5 * sd;

            result = calc.lowGSRValue(mean, sd, normalised);
            Assert.AreEqual(0, result);
        }
        #endregion


        #region midLowGSRValue
        /// <summary>
        /// If the normalised value falls within the boundaries, return a value between 0 and 1.
        /// </summary>
        [Test]
        public void midLowGSRValue_WithinLeftBounds()
        {
            normalised = mean - 1.5 * sd;
            normalised2 = mean - 0.5 * sd;

            result = calc.midLowGSRValue(mean, sd, normalised);
            Assert.LessOrEqual(result, 1);
            Assert.GreaterOrEqual(result, 0);

            result2 = calc.midLowGSRValue(mean, sd, normalised2);
            Assert.LessOrEqual(result2, 1);
            Assert.GreaterOrEqual(result2, 0);
        }

        /// <summary>
        /// If the normalised value is out of bounds, return 0.
        /// </summary>
        [Test]
        public void midLowGSRValue_OutOfBounds()
        {
            normalised = mean + 10;
            normalised2 = mean - 3 * sd;

            result = calc.midLowGSRValue(mean, sd, normalised);
            Assert.AreEqual(0, result);

            result2 = calc.midLowGSRValue(mean, sd, normalised2);
            Assert.AreEqual(0, result2);
        }

        /// <summary>
        /// If the normalised value is equal to the joint boundary value, return 1.
        /// </summary>
        [Test]
        public void midLowGSRValue_OnBoundary()
        {
            normalised = mean - sd;

            result = calc.midLowGSRValue(mean, sd, normalised);
            Assert.AreEqual(1, result);
        }
        #endregion


        #region midHighGSRValue
        /// <summary>
        /// If the normalised value falls within the boundaries,
        /// return a value between 0 and 1.
        /// </summary>
        [Test]
        public void midHighGSRValue_WithinBounds()
        {
            normalised = mean - 0.5 * sd;
            normalised2 = mean + 0.5 * sd;

            result = calc.midHighGSRValue(mean, sd, normalised);
            Assert.LessOrEqual(result, 1);
            Assert.GreaterOrEqual(result, 0);

            result2 = calc.midHighGSRValue(mean, sd, normalised2);
            Assert.LessOrEqual(result2, 1);
            Assert.GreaterOrEqual(result2, 0);
        }


        /// <summary>
        /// If the normalised value is out of bounds, return 0.
        /// </summary>
        [Test]
        public void midHighGSRValue_OutOfBounds()
        {
            normalised = -5;
            normalised2 = mean + 2 * sd;

            result = calc.midHighGSRValue(mean, sd, normalised);
            result2 = calc.midHighGSRValue(mean, sd, normalised2);
            Assert.AreEqual(0, result);
            Assert.AreEqual(0, result2);
        }

        /// <summary>
        /// If the normalised value is equal to the joint boundary value, return 1.
        /// </summary>
        [Test]
        public void midHighGSRValue_OnBoundary()
        {
            normalised = mean;

            result = calc.midHighGSRValue(mean, sd, normalised);
            Assert.AreEqual(1, result);
        }
        #endregion


        #region highGSRValue
        /// <summary>
        /// If the normalised value falls within the boundaries,
        /// return a value between 0 and 1.
        /// </summary>
        [Test]
        public void highGSRValue_WithinBounds()
        {
            normalised = mean + 0.5 * sd;
            normalised2 = mean + 3 * sd;

            result = calc.highGSRValue(mean, sd, normalised);
            Assert.LessOrEqual(result, 1);
            Assert.GreaterOrEqual(result, 0);

            result2 = calc.highGSRValue(mean, sd, normalised2);
            Assert.AreEqual(1, result2);
        }


        /// <summary>
        /// If the normalised value is out of bounds, return 0.
        /// </summary>
        [Test]
        public void highGSRValue_OutOfBounds()
        {
            normalised = mean - 1;

            result = calc.highGSRValue(mean, sd, normalised);
            Assert.AreEqual(0, result);
        }
        #endregion

        #endregion

        #region HR
        #region lowHRValue

        /// <summary>
        /// If the normalised value falls within the boundary, return a value between 0 and 1.
        /// </summary>
        [Test]
        public void lowHRValue_WithinBounds()
        {
            normalised = mean - 0.5 * sd;

            result = calc.lowHRValue(mean, sd, normalised);
            Assert.LessOrEqual(result, 1);
            Assert.GreaterOrEqual(result, 0);
        }

        
        /// <summary>
        /// If the normalised value is out of bounds, return 0.
        /// </summary>
        [Test]
        public void lowHRValue_OutOfBounds()
        {
            normalised = mean;
            normalised2 = -20;

            result = calc.lowHRValue(mean, sd, normalised);
            Assert.AreEqual(0, result);
            result2 = calc.lowHRValue(mean, sd, normalised);
            Assert.AreEqual(0, result2);
        }

        /// <summary>
        /// If the normalised value is equal to the boundary value, return 0.
        /// </summary>
        [Test]
        public void lowHRValue_OnBoundary()
        {
            normalised = mean - sd;

            result = calc.lowHRValue(mean, sd, normalised);
            Assert.AreEqual(0, result);
        }

        #endregion

        #region midHR

        /// <summary>
        /// If the normalised value is within the boundary, either in the left or right part,
        /// return a value between 0 and 1
        /// </summary>
        [Test]
        public void midHRValue_WithinBoundaries()
        {
            normalised = mean - 1.5 * sd; //left side
            normalised2 = mean + sd; // right side

            result = calc.midHRValue(mean, sd, normalised);
            Assert.LessOrEqual(result, 1);
            Assert.GreaterOrEqual(result, 0);
            
            result2 = calc.midHRValue(mean, sd, normalised2);
            Assert.LessOrEqual(result2, 1);
            Assert.GreaterOrEqual(result2, 0);
        }

       
        /// <summary>
        /// If the normalised value is out of bounds on either side, return 0.
        /// </summary>
        [Test]
        public void midHRValue_OutOfBounds()
        {
            normalised = mean - 2.5 * sd;
            normalised2 = mean + 2.5 * sd;

            result = calc.midHRValue(mean, sd, normalised);
            result2 = calc.midHRValue(mean, sd, normalised2);
            Assert.AreEqual(0, result);
            Assert.AreEqual(0, result2);
        }

        /// <summary>
        /// If the normalised value is on the middelboundary, return a value between 0 and 1.
        /// </summary>
        [Test]
        public void midHRValue_OnBoundary()
        {
            normalised = mean;

            result = calc.midHRValue(mean, sd, normalised);
            Assert.AreEqual(1, result);
        }

        #endregion

        #region highHR

        /// <summary>
        /// If the normalised value falls within bounds (is greater than the rightbound),
        /// return a value between 0 and 1.
        /// </summary>
        [Test]
        public void highHRValue_WithinBounds()
        {
            normalised = mean + sd;

            result = calc.highHRValue(mean, normalised);
            Assert.LessOrEqual(result, 1);
            Assert.GreaterOrEqual(result, 0);
        }

        /// <summary>
        /// If the normalised value is out of bounds, return 0.
        /// </summary>
        [Test]
        public void highHRValue_OutOfBounds()
        {
            normalised = mean - sd;
            
            result = calc.highHRValue(mean, normalised);
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// If the normalised value is equal to the boundary, return 0.
        /// </summary>
        [Test]
        public void highHRValue_OnBound()
        {
            normalised = mean;

            result = calc.highHRValue(mean, normalised);
            Assert.AreEqual(0, result);
        }
        #endregion

        #endregion

        #region standardDeviation
        /// <summary>
        /// Test the method for lists with both, positive, negative and 0 values.
        /// Expected values gathered through Wolfram Alpha.
        /// </summary>
        [Test]
        public void standardDeviationFromList_FilledList()
        {
            inputList = new List<double>(new double[] { 5.0, 9.0, 4.0, 2.0, 0.0});
            expected = 3.39116; // Made possible by Wolfram Alpha.

            Assert.AreEqual(Math.Round(expected, 2), Math.Round(calc.standardDeviationFromList(inputList),2));

            inputList = new List<double>(new double[] { 30.0, 22.1, -33.0, 6.0, 33.5});
            expected = 27.1517;

            Assert.AreEqual(Math.Round(expected, 2), Math.Round(calc.standardDeviationFromList(inputList), 2));
        }

        /// <summary>
        /// When the method is called with an empty list we expect to return 0.0
        /// </summary>
        [Test]
        public void standardDeviationFromList_EmptyList()
        {
            inputList = new List<double>();
            expected = 0.0;

            Assert.AreEqual(Math.Round(expected, 2), Math.Round(calc.standardDeviationFromList(inputList), 2));
        }

        #endregion

        [TearDown]
        public void tearDown()
        {
            mean = 0;
            sd = 0;
            calc = null;
        }

    }
}
