using System;
using NUnit.Framework;

using CLESMonitor.Model;

namespace UnitTest.Model
{
    [TestFixture]
    public class FuzzyCalculateTest
    {
        double mean;
        double sd;
        double normalised;

        double result;

        FuzzyCalculate calc;

        [SetUp]
        public void setUp()
        {
            mean = 20;
            sd = 2;
            calc = new FuzzyCalculate(); 
        }

        [Test]
        public void lowGSRValueWithinBounds()
        {
            normalised = 10;

            result = calc.lowGSRValue(mean, sd, normalised);
            Assert.LessOrEqual(result, 1);
        }

        [Test]
        public void lowGSRValueOutOfBounds()
        {
            normalised = 20;

            result = calc.lowGSRValue(mean, sd, normalised);
            Assert.AreEqual(0, result);
        }

        [Test]
        public void lowGSRValueOnBoundary()
        {
            normalised = mean - 1.5 * sd;

            result = calc.lowGSRValue(mean, sd, normalised);
            Assert.AreEqual(0, result);
        }
    }
}
