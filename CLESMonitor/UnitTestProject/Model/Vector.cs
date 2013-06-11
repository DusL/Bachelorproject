using System;
using NUnit.Framework;
using CLESMonitor.Model;

// Use alias
using OriginalVector = CLESMonitor.Model.Vector;

namespace UnitTest.Model
{
    
    [TestFixture]
    public class Vector
    {
        OriginalVector vector;
        OriginalVector vector2;
        [SetUp]
        public void setUp()
        {
            vector = new OriginalVector(1.0, 2.0, 3.0);
            vector2 = new OriginalVector(-4.0, -25.0, -1.0);
        }

        [Test]
        public void dotProductWithSelf()
        {
            Assert.AreEqual(new OriginalVector(1.0, 4.0, 9.0), vector.dotProduct(vector));
        }

        public void dotProductNegativeValue()
        {
            Assert.AreEqual(new OriginalVector(-4.0, -50.0, -3.0), vector.dotProduct(vector2));
        }

        public void dotProductAllZero()
        {
            vector = new OriginalVector(0.0, 0.0, 0.0);
            vector2 = vector;

            Assert.AreEqual(new OriginalVector(0.0, 0.0, 0.0), vector.dotProduct(vector2));
        }

        public void allGreaterThanTen()
        {
            vector = new OriginalVector(12.0, 15.0, 20.0);
            vector2 = new OriginalVector(20.0, 20.0, 20.0);

            Assert.AreEqual(new OriginalVector((12.0 * 20.0), (15.0 * 20.0), (20.0 * 20.0)), vector.dotProduct(vector2));
           
        }
    }
}
