using D3vS1m.Domain.Data.Scene;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Scene.Geometry
{
    public class Face
    {
        public Face()
        {
            this.A = 0;
            this.B = 0;
            this.C = 0;
            this.N = new Vector();
        }

        public void Set(int a, int b, int c, Vector n)
        {
            this.A = a;
            this.B = b;
            this.C = c;
        }

        public int A { get; set; }
        public int B { get; set; }
        public int C { get; set; }
        public Vector N { get; set; }
    }
}
