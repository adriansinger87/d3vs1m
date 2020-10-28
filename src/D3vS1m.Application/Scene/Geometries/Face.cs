using D3vS1m.Domain.Data.Scene;
using System;
using System.Collections.Generic;

namespace D3vS1m.Application.Scene.Geometries
{
    /// <summary>
    /// This class represents a single face with three or four point defining it.
    /// The lists of Vertices and Normals can have an arbitrary range and
    /// the first four elements are accessible with explicit properties.
    /// </summary>
    [Serializable]
    public class Face
    {
        /// <summary>
        /// The default constructor creates empty lists.
        /// </summary>
        public Face()
        {
            Vertices = new List<Vertex>();
            Normals = new List<Vertex>();
        }

        private Vertex GetVertex(int index)
        {
            return Vertices.Count > index ? Vertices[index] : Vertex.Origin;
        }

        private Vertex GetNormale(int index)
        {
            return Normals.Count > index ? Normals[index] : Vertex.Origin;
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

        /// <summary>
        /// Gets the first vertex or returns an origin vertex in case the list entry is empty.
        /// </summary>
        public Vertex A { get { return GetVertex(0); } }
        /// <summary>
        /// Gets the second vertex or returns an origin vertex in case the list entry is empty.
        /// </summary>
        public Vertex B { get { return GetVertex(1); } }
        /// <summary>
        /// Gets the third vertex or returns an origin vertex in case the list entry is empty.
        /// </summary>
        public Vertex C { get { return GetVertex(2); } }
        /// <summary>
        /// Gets the fourth vertex or returns an origin vertex in case the list entry is empty.
        /// </summary>
        public Vertex D { get { return GetVertex(3); } }
        /// <summary>
        /// Gets the first normale or returns an origin vertex in case the list entry is empty.
        /// </summary>
        public Vertex NormaleA { get { return GetNormale(0); } }
        /// <summary>
        /// Gets the second normale or returns an origin vertex in case the list entry is empty.
        /// </summary>
        public Vertex NormaleB { get { return GetNormale(1); } }
        /// <summary>
        /// Gets the third normale or returns an origin vertex in case the list entry is empty.
        /// </summary>
        public Vertex NormaleC { get { return GetNormale(2); } }
        /// <summary>
        /// Gets the fourth normale or returns an origin vertex in case the list entry is empty.
        /// </summary>
        public Vertex NormaleD { get { return GetNormale(3); } }
    }
}
