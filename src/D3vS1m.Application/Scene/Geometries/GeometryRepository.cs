using System;
using System.Collections.Generic;
using System.Linq;

namespace D3vS1m.Application.Scene.Geometries
{
    /// <summary>
    /// This class aggregates all Geometry objects and provides additional functions or properties. 
    /// </summary>
    [Serializable]
    public class GeometryRepository
    {

        // -- fields

        private List<Geometry> _items;

        // -- constructors

        /// <summary>
        /// default empty constructor
        /// </summary>
        public GeometryRepository()
        {
            _items = new List<Geometry>();
        }


        // -- methods

        public void Add(Geometry geometry)
        {
            _items.Add(geometry);
        }

        /// <summary>
        /// This search method returns the first element that matches the name property.
        /// 
        /// </summary>
        /// <param name="name">The name that will searched for.</param>
        /// <param name="recursive">If true this method will be called recursively on all children.</param>
        /// <returns>Returns the first instance with a matching name or null.</returns>
        public Geometry FirstOrDefault(string name, bool recursive)
        {
            var found = default(Geometry);
            foreach(var g in _items)
            {
                found = g.FirstByName(name, recursive);
                if (found != null)
                {
                    return found;
                }
            }

            return found;
        }

        /// <summary>
        /// Adds a new element and returns it.
        /// </summary>
        /// <param name="item">The new Geometry instance</param>
        /// <param name="name">The name of the instance.</param>
        /// <returns></returns>
        public Geometry Add(Geometry item, string name)
        {
            item.Name = name;
            _items.Add(item);
            return _items.Last();
        }

        // -- indexer 

        /// <summary>
        /// Name-based intexer to be able to find items by Name property without recursive search.
        /// For recursive search use FirstByName method of the items.
        /// </summary>
        /// <param name="name">name-based index</param>
        /// <returns>the instance of T or default type that is null.</returns>
        public Geometry this[string name]
        {
            get
            {
                return _items.FirstOrDefault(g => g.Name == name);
            }
        }
    }
}
