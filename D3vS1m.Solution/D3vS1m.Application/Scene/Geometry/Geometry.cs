using D3vS1m.Domain.Data.Scene;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Scene.Geometry
{
    public class Geometry
    {
        public override string ToString()
        {
            return "Geometry (" + Faces.Count + ")";
        }

        public string Material { get; set; }
        public List<Face> Faces { get; set; }
        public List<Vector> Vertices { get; set; }

        public List<Geometry> Children { get; set; }
    }
}
