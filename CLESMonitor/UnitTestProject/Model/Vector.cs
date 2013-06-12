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

        }

        #region dotProduct
        /// <summary>
        /// Test if the method works when invoked with the same vector
        /// </summary>
        [Test]
        public void dotProduct_WithSelf()
        {
            vector = new OriginalVector(1.0, 2.0, 3.0);
            Assert.AreEqual(1+4+9, vector.dotProduct(vector));
        }

        [Test]
        [ExpectedException("System.NullReferenceException")]
        public void dotProduct_WithNull()
        {
            vector = new OriginalVector(1.0, 2.0, 3.0);
            vector2 = null;

            Assert.AreEqual(0.0, vector.dotProduct(vector2));

            vector2.dotProduct(vector);
        }

        /// <summary>
        /// Test if the method works with negative values
        /// </summary>
        [Test]
        public void dotProduct_NegativeValue()
        {
            vector = new OriginalVector(1.0, 2.0, 3.0);
            vector2 = new OriginalVector(-4.0, -25.0, -1.0);
            Assert.AreEqual((-4.0 -50.0 -3.0), vector.dotProduct(vector2));

            vector2 = new OriginalVector(-4.0, 5.0, 1.0);
            Assert.AreEqual((-4.0 + 10.0 + 3.0), vector.dotProduct(vector2));
        }

        /// <summary>
        /// When only all-zero value vectors are used, still a valid result should be produced; the result is 0.0;
        /// </summary>
        [Test]
        public void dotProduct_AllZero()
        {
            vector = new OriginalVector(0.0, 0.0, 0.0);
            
            Assert.AreEqual(0.0, vector.dotProduct(vector));
        }
        /// <summary>
        /// Test if the method works with large numbers
        /// </summary>
        [Test]
        public void dotProduct_AllGreaterThanTen()
        {
            vector = new OriginalVector(12.0, 15.0, 20.0);
            vector2 = new OriginalVector(20.0, 20.0, 20.0);

            Assert.AreEqual(((12.0 * 20.0) + (15.0 * 20.0) + (20.0 * 20.0)), vector.dotProduct(vector2));
        }

        #endregion

        #region orthogonalProjection
        /// <summary>
        /// An orthogonalProjection on self should result in the same vector.
        /// </summary>
        [Test]
        public void orthogonalProjection_Self()
        {
            vector = new OriginalVector(1.0, 2.0, 3.0);
            Assert.AreEqual(vector.x, vector.orthogonalProjection(vector).x);
            Assert.AreEqual(vector.y, vector.orthogonalProjection(vector).y);
            Assert.AreEqual(vector.z, vector.orthogonalProjection(vector).z);
        }

        /// <summary>
        /// When one or more values of the vectors are negative, a valid result should still be generated
        /// </summary>
        [Test]
        public void orthogonalProjection_NegativeValues()
        {
            vector = new OriginalVector(1.0, 2.0, 3.0);
            vector2 = new OriginalVector(-4.0, -25.0, -1.0);

            double fraction = vector.dotProduct(vector2)/vector.dotProduct(vector);

            Assert.AreEqual(vector.x * fraction, vector.orthogonalProjection(vector2).x);
            Assert.AreEqual(vector.y * fraction, vector.orthogonalProjection(vector2).y);
            Assert.AreEqual(vector.z * fraction, vector.orthogonalProjection(vector2).z);
        }

        /// <summary>
        /// If the current vector (the 'this' vector) contains zero's only, a null-vector should be returned.
        /// </summary>
        [Test]
        public void orthogonalProjection_AllZero()
        {
            vector = new OriginalVector(0.0, 0.0, 0.0);

            Assert.IsNull(vector.orthogonalProjection(vector));
        }

        /// <summary>
        /// When using large values, still a valid result should be generated
        /// </summary>
        [Test]
        public void orthogonalProjection_AllGreaterThanTen()
        {
            vector = new OriginalVector(12.0, 15.0, 20.0);
            vector2 = new OriginalVector(20.0, 20.0, 20.0);

            double fraction = vector.dotProduct(vector2) / vector.dotProduct(vector);

            Assert.AreEqual(vector.x * fraction, vector.orthogonalProjection(vector2).x);
            Assert.AreEqual(vector.y * fraction, vector.orthogonalProjection(vector2).y);
            Assert.AreEqual(vector.z * fraction, vector.orthogonalProjection(vector2).z);

        }
        #endregion


        /// <summary>
        /// When the method is called with all zero values, a return of 0.0 is expected.
        /// </summary>
        [Test]
        public void length_AllZero()
        {
            vector = new OriginalVector(0.0, 0.0, 0.0);

            Assert.AreEqual(0.0, vector.length());
        }

        /// <summary>
        /// When the vector used to call length is null, a null reference exception is expected.
        /// </summary>
        [Test]
        [ExpectedException("System.NullReferenceException")]
        public void length_NullInput()
        {
            vector = null;
            vector.length();
        }

        /// <summary>
        /// Any given value not equal to 0 or null should return the expected value
        /// </summary>
        [Test]
        public void length_ValidInput()
        {
            vector = new OriginalVector(-4.0, -25.0, -1.0);
            double expected = Math.Sqrt(vector.dotProduct(vector));

            Assert.AreEqual(expected, vector.length());

            vector2 = new OriginalVector(12.0, 15.0, 20.0);
            expected = Math.Sqrt(vector2.dotProduct(vector2));
            Assert.AreEqual(expected, vector2.length());

        }
    }
}
