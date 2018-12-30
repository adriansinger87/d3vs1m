using System;

namespace D3vS1m.Domain.Data.Scene
{
    /// <summary>
    /// The Vector represents a 3D-vector in the cartesian coordinate system with a float type for the x, y and z dimension.
    /// </summary>
    public class Vector
    {
        #region Constructors (2)
        public Vector()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }

        public Vector(float x, float y, float z)
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
        /// Calculates teh euclidic distance between two vectors
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float GetLength(Vector a, Vector b)
        {
            // sqrt( (a.x - b.x)² + (a.y - b.y)² + (a.z - b.z)² )  
            return (float)Math.Sqrt(
                Math.Pow(a.X - b.X, 2) +
                Math.Pow(a.Y - b.Y, 2) +
                Math.Pow(a.Z - b.Z, 2));
        }

        public static Vector Normalize(Vector a, Vector b, Vector c)
        {
            /*
             * based on three vectors, two directional vectors ab and ac are created and cross product is calculated
             * this one stands orthogonal on the plane
             */ 
            Vector n = ((a - b) * (a - c));

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

        public static float Scalar(Vector a, Vector b)
        {
            return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }

        // -- operators

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        /// <summary>
        /// Cross product
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(
                ((a.Y * b.Z) - (a.Z * b.Y)),
                ((a.Z * b.X) - (a.X * b.Z)),
                ((a.X * b.Y) - (a.Y * b.X)));
        }

    }
}
