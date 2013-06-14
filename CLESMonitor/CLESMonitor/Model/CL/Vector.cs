using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model.CL
{
    /// <summary>
    /// This is a vector class, representing a vector in 3D space. It contains methods to
    /// calculate with vectors including dot product, vector length and orthogonal projection.
    /// </summary>
    public class Vector
    {
        public double x {get; private set;}
        public double y {get; private set;}
        public double z {get; private set;}

        public Vector(double _x, double _y, double _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        /// <summary>
        /// Calculate the dotProduct between this vector and another vector.
        /// </summary>
        /// <param name="vector">The other vector</param>
        /// <returns>The dotproduct</returns>
        public double dotProduct(Vector vector)
        {
            double returnValue = 0.0;
            if (vector != null)
            {
                returnValue = (this.x * vector.x) + (this.y * vector.y) + (this.z * vector.z);
            }
            return returnValue;
        }

        /// <summary>
        /// Calculates the orthogonal projection of a vector on a differnet vector
        /// </summary>
        /// <param name="toVector">The vector which is projected upon</param>
        /// <returns>The projected vector</returns>
        public Vector orthogonalProjection(Vector toVector)
        {  
            Vector returnVector = null;
            if (!((toVector.x == 0) && (toVector.y == 0) && (toVector.z == 0)))
            {
                double fraction = this.dotProduct(toVector) / toVector.dotProduct(toVector);
                returnVector = new Vector(fraction * toVector.x, fraction * toVector.y, fraction * toVector.z);
            }
            
            return returnVector;  
        }

        /// <summary>
        /// Calculate the length of a vector
        /// </summary>
        /// <returns>The length of a vector</returns>
        public double length()
        {
            return Math.Sqrt(this.dotProduct(this));
        }

        #region operator overload

        /// <summary>
        /// Overlaod the + operator
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns>vector1 + vector2</returns>
        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return (new Vector(vector1.x + vector2.x, vector1.y + vector2.y, vector1.z + vector2.z));
        }
        /// <summary>
        /// Overload the - operator
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns>vector1-vector2</returns>
        public static Vector operator -(Vector vector1, Vector vector2)
        {
            return (new Vector(vector1.x - vector2.x, vector1.y - vector2.y, vector1.z - vector2.z));
        }

        /// <summary>
        /// Override the Equals method so that it can easily check if two vectors contain the same elements. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(Object obj)
        {
            bool equals = false;

            if (obj != null && obj.GetType().Equals(typeof(Vector)))
            {
                Vector vector = (Vector)obj;

                if (this.x == vector.x
                    && this.y == vector.y
                    && this.z == vector.z)
                {
                    equals = true;
                }
            }
            
            return equals;
        } 

        #endregion

        public override string ToString()
        {
            return String.Format("Vector: x={0}, y={1}, z={2}", x, y, z);
        }
    }      
}
