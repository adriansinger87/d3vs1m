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
            Vertices = new List<Vertex>();
            Normals = new List<Vertex>();
        }

        private Vertex getVertex(int index)
        {
            return Vertices.Count > index ? Vertices[index] : new Vertex();
        }

        private Vertex getNormale(int index)
        {
            // in case of a fault do not return null but a (0 | 0 | 0 ) object
            return Normals.Count > index ? Normals[index] : new Vertex();
        }

        // -- properties

        /// <summary>
        /// Gets the list of vertices that represents the face
        /// </summary>
        public List<Vertex> Vertices { get; private set; }

        /// <summary>
        /// Gets the list of normals that represents the face
        /// </summary>
        public List<Vertex> Normals { get; private set; }

        public Vertex A { get { return getVertex(0); } }
        public Vertex B { get { return getVertex(1); } }
        public Vertex C { get { return getVertex(2); } }
        public Vertex D { get { return getVertex(3); } }

        public Vertex NormaleA { get { return getNormale(0); } }
        public Vertex NormaleB { get { return getNormale(1); } }
        public Vertex NormaleC { get { return getNormale(2); } }
        public Vertex NormaleD { get { return getNormale(3); } }
    }
}
