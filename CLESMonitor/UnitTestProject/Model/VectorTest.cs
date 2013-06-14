using System;
using NUnit.Framework;
using CLESMonitor.Model.CL;

namespace UnitTest.Model
{
    [TestFixture]
    public class VectorTest
    {
        Vector vector;
        Vector vector2;
        Vector checkVector;

        #region Operator override

        /// <summary>
        /// Test the equality method. If all elements of the vectors are equal, equals returns true.
        /// </summary>
        [Test]
        public void equalityTest_PerValue()
        {
            vector = new Vector(1.0, 2.0, 3.0);
            
            vector2 = new Vector(1.0, 2.0, 3.0);
            Assert.IsTrue(vector.Equals(vector2));

            vector2 = new Vector(0.0, 2.0, 3.0);
            Assert.IsFalse(vector.Equals(vector2));

            vector2 = new Vector(1.0, 0.0, 3.0);
            Assert.IsFalse(vector.Equals(vector2));

            vector2 = new Vector(1.0, 2.0, 0.0);
            Assert.IsFalse(vector.Equals(vector2));
        }
        /// <summary>
        /// Test the logic of the equality method
        /// </summary>
        [Test]
        public void equalityTest_Logic()
        {
            vector = new Vector(1.0, 2.0, 3.0);
            vector2 = new Vector(15.0, 6.0, 0.0);

            Assert.IsTrue(vector.Equals(vector));
            Assert.AreEqual(vector.Equals(vector2), vector2.Equals(vector));
            Assert.IsFalse(vector.Equals(null));
        }

        /// <summary>
        /// Test for the override method for the addition operator
        /// </summary>
        [Test]
        public void additionOperator()
        {
            vector = new Vector(1.0, 2.0, 3.0);
            vector2 = new Vector(15.0, 6.0, 0.0);

            checkVector = new Vector(16.0, 8.0, 3.0);
            Assert.IsTrue((vector + vector2).Equals(checkVector));
            Assert.IsTrue((vector2 + vector).Equals(checkVector));

            vector = new Vector(-1.0, -2.0, -3.0);
            checkVector = new Vector(14.0, 4.0, -3.0);
            Assert.IsTrue((vector + vector2).Equals(checkVector));
            Assert.IsTrue((vector2 + vector).Equals(checkVector));
        }

        /// <summary>
        /// Test for the override method for the substraction operator
        /// </summary>
        [Test]
        public void substractionOperator()
        {
            vector = new Vector(1.0, 2.0, 3.0);
            vector2 = new Vector(15.0, 6.0, 0.0);

            checkVector = new Vector(-14.0, -4.0, 3.0);
            Assert.IsTrue((vector - vector2).Equals(checkVector));
            Assert.IsFalse((vector2 - vector).Equals(checkVector));

            vector = new Vector(-1.0, -2.0, -3.0);
            checkVector = new Vector(16.0, 8.0, 3.0);
            Assert.IsTrue((vector2 - vector).Equals(checkVector));
            Assert.IsFalse((vector - vector2).Equals(checkVector));
        }

        #endregion

        #region dotProduct
        /// <summary>
        /// Test if the method works when invoked with the same vector
        /// </summary>
        [Test]
        public void dotProduct_WithSelf()
        {
            vector = new Vector(1.0, 2.0, 3.0);
            Assert.AreEqual(1+4+9, vector.dotProduct(vector));
        }

        /// <summary>
        /// When the paramater vector is null, we expect the dotPoroduct of the vectors to be 0.0;
        /// When the vector calling the method is null, a nullReferenceException is expected.
        /// </summary>
        [Test]
        [ExpectedException("System.NullReferenceException")]
        public void dotProduct_WithNull()
        {
            vector = new Vector(1.0, 2.0, 3.0);
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
            vector = new Vector(1.0, 2.0, 3.0);
            vector2 = new Vector(-4.0, -25.0, -1.0);
            Assert.AreEqual((-4.0 -50.0 -3.0), vector.dotProduct(vector2));

            vector2 = new Vector(-4.0, 5.0, 1.0);
            Assert.AreEqual((-4.0 + 10.0 + 3.0), vector.dotProduct(vector2));
        }

        /// <summary>
        /// When only all-zero value vectors are used, still a valid result should be produced; the result is 0.0;
        /// </summary>
        [Test]
        public void dotProduct_AllZero()
        {
            vector = new Vector(0.0, 0.0, 0.0);
            
            Assert.AreEqual(0.0, vector.dotProduct(vector));
        }
        /// <summary>
        /// Test if the method works with large numbers
        /// </summary>
        [Test]
        public void dotProduct_AllGreaterThanTen()
        {
            vector = new Vector(12.0, 15.0, 20.0);
            vector2 = new Vector(20.0, 20.0, 20.0);

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
            vector = new Vector(1.0, 2.0, 3.0);
            Assert.IsTrue(vector.Equals(vector.orthogonalProjection(vector)));
        }

        /// <summary>
        /// When one or more values of the vectors are negative, a valid result should still be generated
        /// </summary>
        [Test]
        public void orthogonalProjection_NegativeValues()
        {
            vector = new Vector(1.0, 2.0, 3.0);
            vector2 = new Vector(-4.0, -25.0, -1.0);

            double fraction = vector.dotProduct(vector2)/vector2.dotProduct(vector2);
            Vector newVector =  new Vector (fraction*(-4.0), (-25.0) * fraction, (-1.0)* fraction);
            Assert.IsTrue(newVector.Equals(vector.orthogonalProjection(vector2)));
        }

        /// <summary>
        /// If the current vector (the 'this' vector) contains zero's only, a null-vector should be returned.
        /// </summary>
        [Test]
        public void orthogonalProjection_AllZero()
        {
            vector = new Vector(0.0, 0.0, 0.0);

            Assert.IsNull(vector.orthogonalProjection(vector));

            Assert.IsNotNull(vector.orthogonalProjection((new Vector(1.0, 1.0, 1.0))));
        }

        /// <summary>
        /// When using large values, still a valid result should be generated
        /// </summary>
        [Test]
        public void orthogonalProjection_AllGreaterThanTen()
        {
            vector = new Vector(12.0, 15.0, 20.0);
            vector2 = new Vector(20.0, 20.0, 20.0);

            double fraction = vector.dotProduct(vector2) / vector.dotProduct(vector);
            Vector newVector = new Vector(12 * fraction, 15 * fraction, 20 * fraction);

        }
        #endregion

        #region length
        /// <summary>
        /// When the method is called with all zero values, a return of 0.0 is expected.
        /// </summary>
        [Test]
        public void length_AllZero()
        {
            vector = new Vector(0.0, 0.0, 0.0);

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
            vector = new Vector(-4.0, -25.0, -1.0);
            double expected = Math.Sqrt(vector.dotProduct(vector));

            Assert.AreEqual(expected, vector.length());

            vector2 = new Vector(12.0, 15.0, 20.0);
            expected = Math.Sqrt(vector2.dotProduct(vector2));
            Assert.AreEqual(expected, vector2.length());
        }
        #endregion
    }
}
