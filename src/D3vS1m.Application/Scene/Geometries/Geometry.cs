using D3vS1m.Domain.Data.Scene;
using System;
using System.Collections.Generic;

namespace D3vS1m.Application.Scene.Geometries
{
    [Serializable]
    public class Geometry
    {
        public Geometry()
        {
            Name = Models.Scene.DefaultGeometry;
            Faces = new List<Face>();
            Vertices = new List<Vertex>();
            Normales = new List<Vertex>();
            Children = new List<Geometry>();
        }

        public Geometry(string name) : this()
        {
            Name = name;
        }

        // -- methods

        public Geometry FirstByName(string name, bool recursive)
        {
            Geometry found = (Name.Equals(name) ? this : null);
            if (found == null && recursive == true)
            {
                foreach (var g in Children)
                {
                    found = g.FirstByName(name, recursive);
                    if (found != null)
                    {
                        return found;
                    }
                }

            }
            return found;
        }

        public override string ToString()
        {
            return $"{Name} - Childs: {Children.Count}, Faces: {Faces.Count}, Vertices: {Vertices.Count}";
        }

        // -- properties

        public string Name { get; set; }

        public string Material { get; set; }
        public List<Face> Faces { get; set; }
        public List<Vertex> Vertices { get; set; }
        public List<Vertex> Normales { get; set; }
        public List<Geometry> Children { get; set; }
    }
}
