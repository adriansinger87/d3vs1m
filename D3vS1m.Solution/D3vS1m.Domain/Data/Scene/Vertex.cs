using System;

namespace D3vS1m.Domain.Data.Scene
{
    /// <summary>
    /// The Vector represents a 3D-vector in the cartesian coordinate system with a float type for the x, y and z dimension.
    /// </summary>
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
