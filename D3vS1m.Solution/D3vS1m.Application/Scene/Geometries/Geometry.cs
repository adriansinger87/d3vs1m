using D3vS1m.Domain.Data.Scene;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Scene.Geometries
{
    public class Geometry
    {
        public Geometry()
        {
            // TODO: remove magic string
            Name = "geometry";

            Faces = new List<Face>();
            Vertices = new List<Vertex>();
            Normales = new List<Vertex>();
            Children = new List<Geometry>();
        }

        public Geometry(string name) : this()
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"{Name} - Childs: {Children.Count}, Faces: {Faces.Count}, Vertices: {Vertices.Count}";
        }

        public string Name { get; set; }

        public string Material { get; set; }
        public List<Face> Faces { get; set; }
        public List<Vertex> Vertices { get; set; }
        public List<Vertex> Normales { get; set; }
        public List<Geometry> Children { get; set; }
    }
}
