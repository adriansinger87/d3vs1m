using D3vS1m.Domain.Data.Scene;
using System;
using System.Collections.Generic;

namespace D3vS1m.Application.Scene.Geometries
{
    /// <summary>
    /// The Geometry class composes a lists of faces unattached vertices and normals.
    /// This class follows the composite-pattern so it has a list of children of the same time
    /// in order to create trees of geometries.
    /// Empty geometries for grouping purposes can exsist as well.
    /// </summary>
    [Serializable]
    public class Geometry
    {
        /// <summary>
        /// The default constructor instantiates all list properties and a default name.
        /// </summary>
        public Geometry()
        {
            Name = Models.Scene.Materials.DefaultGeometry;
            Faces = new List<Face>();
            Vertices = new List<Vertex>();
            Normales = new List<Vertex>();
            Children = new List<Geometry>();
        }

        /// <summary>
        /// The constructor calls the default constructor and defines the name.
        /// </summary>
        /// <param name="name">The specific name of the instance.</param>
        public Geometry(string name) : this()
        {
            Name = name;
        }

        // -- methods

        /// <summary>
        /// A search method that returns the first match by the name property.
        /// </summary>
        /// <param name="name">The name that will searched for.</param>
        /// <param name="recursive">If true this method will be called recursively on all children.</param>
        /// <returns>Returns the first instance with a matching name or null.</returns>
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

        /// <summary>
        /// Overridden method that returns important properties.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name} - Childs: {Children.Count}, Faces: {Faces.Count}, Vertices: {Vertices.Count}";
        }

        // -- properties

        /// <summary>
        /// Gets or sets the name of the instance.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the material GUID to look for in a channel simulation.
        /// </summary>
        public string MaterialGuid { get; set; }
        /// <summary>
        /// Gets or sets the list of faces that create the surface of the geometry.
        /// </summary>
        public List<Face> Faces { get; set; }
        /// <summary>
        /// Gets or sets a list unattached vertices.
        /// </summary>
        public List<Vertex> Vertices { get; set; }
        /// <summary>
        /// Gets or sets a list unattached normals.
        /// </summary>
        public List<Vertex> Normales { get; set; }
        /// <summary>
        /// Gets or sets a list of child geometries.
        /// </summary>
        public List<Geometry> Children { get; set; }
    }
}
