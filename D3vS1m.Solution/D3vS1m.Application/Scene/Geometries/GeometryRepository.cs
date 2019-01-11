using D3vS1m.Application.Scene.Materials;
using D3vS1m.Domain.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D3vS1m.Application.Scene.Geometries
{
    public class GeometryRepository : RepositoryBase<Geometry>
    {
        public GeometryRepository()
        {

        }

        public Geometry FirstOrDefault(string name, bool recursive)
        {
            Geometry found = this[name];

            if (recursive)
            {
                _items.ForEach((g) => {
                    found = g.FirstByName(name, recursive);
                });
            }

            return found;
        }

        // -- indexer 

        /// <summary>
        /// Name-based intexer to be able to find items by Name property without recursive search.
        /// For recursive search use FirstOrDefault method.
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
