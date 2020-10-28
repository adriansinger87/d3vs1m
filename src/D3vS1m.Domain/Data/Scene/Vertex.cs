using System;

namespace D3vS1m.Domain.Data.Scene
{
    /// <summary>
    /// The Vector represents a 3D-vector in the cartesian coordinate system with a float type for the x, y and z dimension.
    /// </summary>
    [Serializable]
    public class Vertex
    {
        #region Constructors (2)
        public Vertex()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }

        public Vertex(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        #endregion

        // -- public methods

        public void Set(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public float[] ToArray()
        {
            return new float[] { X, Y, Z };
        }

        public override string ToString()
        {
            return $"( {X} | {Y} | {Z} )";
        }

        public override bool Equals(object obj)
        {
            Vertex v = obj as Vertex;
            return (this.X == v.X && this.Y == v.Y && this.Z == v.Z);
        }

        // --- properties

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public float Length
        {
            get
            {
                // sqrt( (x)² + (y)² + (z)² )  
                return (float)Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
            }
        }

        // -- static methods

        /// <summary>
        /// Returns a new instance with zero values in all cartesian directions.
        /// </summary>
        public static Vertex Origin => new Vertex();

        /// <summary>
        /// Calculates teh euclidic distance between two vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float GetLength(Vertex a, Vertex b)
        {
            // sqrt( (a.x - b.x)² + (a.y - b.y)² + (a.z - b.z)² )  
            return (float)Math.Sqrt(
                Math.Pow(a.X - b.X, 2) +
                Math.Pow(a.Y - b.Y, 2) +
                Math.Pow(a.Z - b.Z, 2));
        }

        public static Vertex Normalize(Vertex a, Vertex b, Vertex c)
        {
            /*
             * based on three vectors, two directional vectors ab and ac are created and cross product is calculated
             * this one stands orthogonal on the plane
             */ 
            Vertex n = ((a - b) * (a - c));

            // length of normal
            float len = n.Length;

            // normalize
            if (len != 0.0f)
            {
                n.X /= len * -1;
                n.Y /= len * -1;
                n.Z /= len * -1;
            }
            else
            {
                n.Set(0, 0, 0);
            }
            return n;
        }

        public static float Scalar(Vertex a, Vertex b)
        {
            return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }

        public static Vertex RotateRadX(float rad, Vertex a)
        {
            /*
             * 3x3-Matrix for X-Rotation:
             * --------------------------
             *	1			0				0
             *	0		cos(rad)		- sin(rad)
             *	0		sin(rad)		  cos(rad)
             *
             * result of the Matrix-Multiplication:
             * -----------------------------------
             * x = (1 * a.x)	+	(0 * a.y)			+	(0 * a.z) 
             * y = (0 * a.x)	+	(cos(rad) * a.y)	+	(- sin(rad) * a.z)
             * z = (0 * a.x)	+	(sin(rad) * a.y)	+	(cos(rad) * a.z)
             */
            return new Vertex(
                (1 * a.X) + (0 * a.Y) + (0 * a.Z),
                (float)((0 * a.X) + (Math.Cos(rad) * a.Y) + ((-Math.Sin(rad)) * a.Z)),
                (float)((0 * a.X) + (Math.Sin(rad) * a.Y) + (Math.Cos(rad) * a.Z)));
        }

        public static Vertex RotateRadY(float rad, Vertex a)
        {
            /*
             * 3x3-Matrix for Y-Rotation:
             * --------------------------
             *	  cos(rad)		0		  sin(rad)
             *		0			1			0
             *	- sin(rad)		0		  cos(rad)
             *
             * result of the Matrix-Multiplication:
             * -----------------------------------
             * x = (cos(rad) * a.x)		+	(0 * a.y)	+	(sin(rad) * a.z) 
             * y = (0 * a.x)			+	(1 * a.y)	+	(0 * a.z)
             * z = (- sin(rad) * a.x)	+	(0 * a.y)	+	(cos(rad) * a.z)
             */
            return new Vertex(
               (float)((Math.Cos(rad) * a.X) + (0 * a.Y) + (Math.Sin(rad) * a.Z)),
               (0 * a.X) + (1 * a.Y) + (0 * a.Z),
               (float)(((-Math.Sin(rad)) * a.X) + (0 * a.Y) + (Math.Cos(rad) * a.Z)));
        }

        public static Vertex RotateRadZ(float rad, Vertex a)
        {
            /*
              * 3x3-Matrix for Z-Rotation:
              * --------------------------
              *	cos(rad)	- sin(rad)		0
              *	sin(rad)	  cos(rad)		0
              *		0			0			1
              *
              * Ergebnis der Matrix-Multiplikation:
              * -----------------------------------
              * x = (cos(rad) * a.x)	+	(- sin(rad) * a.y)	+	(0 * a.z) 
              * y = (sin(rad) * a.x)	+	(cos(rad) * a.y)	+	(0 * a.z)
              * z = (0 * a.x)		    +	(0 * a.y)			+	(1 * a.z)
              */
            return new Vertex(
              (float)((Math.Cos(rad) * a.X) + ((-Math.Sin(rad)) * a.Y) + (0 * a.Z)),
              (float)((Math.Sin(rad) * a.X) + (Math.Cos(rad) * a.Y) + (0 * a.Z)),
              (0 * a.X) + (0 * a.Y) + (1 * a.Z));
        }

        /// <summary>
        /// Calculates the inner angle of twi vetrices
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float ACosRad(Vertex a, Vertex b)
        {
            float f1 = Vertex.Scalar(a, b) / (a.Length * b.Length);

            if (f1 > 1) f1 = 1;
            else if (f1 < -1) f1 = -1;

            return (float)Math.Acos(f1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float ASinRad(Vertex a, Vertex b)
        {
            float f1 = Vertex.Scalar(a, b) / (a.Length * b.Length);

            if (f1 > 1) f1 = 1;
            else if (f1 < -1) f1 = -1;

            return (float)Math.Asin(f1);
        }

        public override int GetHashCode()
        {
            var hashCode = 612420109;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + Z.GetHashCode();
            hashCode = hashCode * -1521134295 + Length.GetHashCode();
            return hashCode;
        }

        // -- operators

        public static Vertex operator +(Vertex a, Vertex b)
        {
            return new Vertex(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vertex operator -(Vertex a, Vertex b)
        {
            return new Vertex(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        /// <summary>
        /// Cross product
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vertex operator *(Vertex a, Vertex b)
        {
            return new Vertex(
                ((a.Y * b.Z) - (a.Z * b.Y)),
                ((a.Z * b.X) - (a.X * b.Z)),
                ((a.X * b.Y) - (a.Y * b.X)));
        }

    }
}
