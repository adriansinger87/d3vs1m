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

        // --- public methods

        public void Set(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public float[] ToArray()
        {
            return new float[] { this.X, this.Y, this.Z };
        }

        public override string ToString()
        {
            return $"( {X} | {Y} | {Z} )";
        }

        // --- operators

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

        // --- properties

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public double Length
        {
            get
            {
                // sqrt( (x)² + (y)² + (z)² )  
                return Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
                    //Math.Pow(X, 2) +
                    //Math.Pow(Y, 2) +
                    //Math.Pow(Z, 2));
            }
        }
    }
}
