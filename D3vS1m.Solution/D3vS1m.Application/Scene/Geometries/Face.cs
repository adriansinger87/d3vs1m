using D3vS1m.Domain.Data.Scene;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Scene.Geometries
{
    public class Face
    {
        public Face()
        {
            this.A = 0;
            this.B = 0;
            this.C = 0;
            this.N = new Vertex();
        }

        public void Set(int a, int b, int c, Vertex n)
        {
            this.A = a;
            this.B = b;
            this.C = c;
        }

        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public Vertex N { get; set; }
    }
}
