using Sin.Net.Domain.Repository;
using System;
using System.Linq;

namespace D3vS1m.Application.Scene.Geometries
{
    [Serializable]
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
                Items.ForEach((g) =>
                {
                    found = g.FirstByName(name, recursive);
                });
            }

            return found;
        }

        public Geometry Add(Geometry item, string name)
        {
            item.Name = name;
            Items.Add(item);
            return Items.Last();
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
                return Items.FirstOrDefault(g => g.Name == name);
            }
        }
    }
}
