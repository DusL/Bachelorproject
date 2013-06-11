using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLESMonitor.Model
{
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
        /// Calculate the dotProduct of two vectors
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns>The dotproduct of this vector with vector</returns>
        public double dotProduct(Vector vector)
        { 
            return (this.x * vector.x) + (this.y * vector.y) + (this.z * vector.z);
        }


        /// <summary>
        /// Calculates the orthogonal projection of a vector on a differnet vector
        /// </summary>
        /// <param name="vector">The vector which is projected upon</param>
        /// <returns>The projected vector</returns>
        public Vector orthogonalProjection(Vector vector)
        {
            double fraction = this.dotProduct(vector) / this.dotProduct(vector);

            return (new Vector(fraction * this.x,fraction * this.y, fraction * this.z));  
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

        #endregion

    }
}
