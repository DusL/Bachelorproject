using System;
using NUnit.Framework;
using CLESMonitor.Model.ES;
using System.Collections.Generic;
using System.Linq;

namespace UnitTest.Model.ES
{
    [TestFixture]
    public class FuzzyMathTest
    {
        double mean;
        double sd;
        double normalised;
        double normalised2;

        double result;
        double result2;

        double expected;
        List<double> inputList;


        [SetUp]
        public void setUp()
        {
            mean = 20;
            sd = 4;
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

            result = FuzzyMath.lowGSRValue(mean, sd, normalised);
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

            result = FuzzyMath.lowGSRValue(mean, sd, normalised);
            Assert.AreEqual(0, result);

            result2 = FuzzyMath.lowGSRValue(mean, sd, normalised);
            Assert.AreEqual(0, result2);
        }

        /// <summary>
        /// If the normalised value is equal to the boundary value, return 0.
        /// </summary>
        [Test]
        public void lowGSRValue_OnBoundary()
        {
            normalised = mean - 1.5 * sd;

            result = FuzzyMath.lowGSRValue(mean, sd, normalised);
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

            result = FuzzyMath.midLowGSRValue(mean, sd, normalised);
            Assert.LessOrEqual(result, 1);
            Assert.GreaterOrEqual(result, 0);

            result2 = FuzzyMath.midLowGSRValue(mean, sd, normalised2);
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

            result = FuzzyMath.midLowGSRValue(mean, sd, normalised);
            Assert.AreEqual(0, result);

            result2 = FuzzyMath.midLowGSRValue(mean, sd, normalised2);
            Assert.AreEqual(0, result2);
        }

        /// <summary>
        /// If the normalised value is equal to the joint boundary value, return 1.
        /// </summary>
        [Test]
        public void midLowGSRValue_OnBoundary()
        {
            normalised = mean - sd;

            result = FuzzyMath.midLowGSRValue(mean, sd, normalised);
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

            result = FuzzyMath.midHighGSRValue(mean, sd, normalised);
            Assert.LessOrEqual(result, 1);
            Assert.GreaterOrEqual(result, 0);

            result2 = FuzzyMath.midHighGSRValue(mean, sd, normalised2);
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

            result = FuzzyMath.midHighGSRValue(mean, sd, normalised);
            result2 = FuzzyMath.midHighGSRValue(mean, sd, normalised2);
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

            result = FuzzyMath.midHighGSRValue(mean, sd, normalised);
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

            result = FuzzyMath.highGSRValue(mean, sd, normalised);
            Assert.LessOrEqual(result, 1);
            Assert.GreaterOrEqual(result, 0);

            result2 = FuzzyMath.highGSRValue(mean, sd, normalised2);
            Assert.AreEqual(1, result2);
        }


        /// <summary>
        /// If the normalised value is out of bounds, return 0.
        /// </summary>
        [Test]
        public void highGSRValue_OutOfBounds()
        {
            normalised = mean - 1;

            result = FuzzyMath.highGSRValue(mean, sd, normalised);
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

            result = FuzzyMath.lowHRValue(mean, sd, normalised);
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

            result = FuzzyMath.lowHRValue(mean, sd, normalised);
            Assert.AreEqual(0, result);
            result2 = FuzzyMath.lowHRValue(mean, sd, normalised);
            Assert.AreEqual(0, result2);
        }

        /// <summary>
        /// If the normalised value is equal to the boundary value, return 0.
        /// </summary>
        [Test]
        public void lowHRValue_OnBoundary()
        {
            normalised = mean - sd;

            result = FuzzyMath.lowHRValue(mean, sd, normalised);
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

            result = FuzzyMath.midHRValue(mean, sd, normalised);
            Assert.LessOrEqual(result, 1);
            Assert.GreaterOrEqual(result, 0);

            result2 = FuzzyMath.midHRValue(mean, sd, normalised2);
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

            result = FuzzyMath.midHRValue(mean, sd, normalised);
            result2 = FuzzyMath.midHRValue(mean, sd, normalised2);
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

            result = FuzzyMath.midHRValue(mean, sd, normalised);
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

            result = FuzzyMath.highHRValue(mean, normalised);
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

            result = FuzzyMath.highHRValue(mean, normalised);
            Assert.AreEqual(0, result);
        }

        /// <summary>
        /// If the normalised value is equal to the boundary, return 0.
        /// </summary>
        [Test]
        public void highHRValue_OnBound()
        {
            normalised = mean;

            result = FuzzyMath.highHRValue(mean, normalised);
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

            Assert.AreEqual(Math.Round(expected, 2), Math.Round(FuzzyMath.standardDeviationFromList(inputList), 2));

            inputList = new List<double>(new double[] { 30.0, 22.1, -33.0, 6.0, 33.5});
            expected = 27.1517;

            Assert.AreEqual(Math.Round(expected, 2), Math.Round(FuzzyMath.standardDeviationFromList(inputList), 2));
        }

        /// <summary>
        /// When the method is called with an empty list we expect to return 0.0
        /// </summary>
        [Test]
        public void standardDeviationFromList_EmptyList()
        {
            inputList = new List<double>();
            expected = 0.0;

            Assert.AreEqual(Math.Round(expected, 2), Math.Round(FuzzyMath.standardDeviationFromList(inputList), 2));
        }

        #endregion

        [TearDown]
        public void tearDown()
        {
            mean = 0;
            sd = 0;
        }

    }
}
