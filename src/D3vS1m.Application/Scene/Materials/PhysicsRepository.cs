using Sin.Net.Domain.Repository;
using System;

namespace D3vS1m.Application.Scene.Materials
{
    [Serializable]
    public class PhysicsRepository : RepositoryBase<MaterialPhysics>
    {
        public PhysicsRepository()
        {

        }

        // -- indexer

        /// <summary>
        /// Frequency-based intexer to be able to get the element closest to the given frequency out of the repository.
        /// The element doesn't need to have the exact frequency. 
        /// </summary>
        /// <param name="freq">the searced frequency</param>
        /// <returns>the instance of MaterialPhysics with the closes frequency property or a new constructed instance</returns>
        public MaterialPhysics this[float freq]
        {
            get
            {
                var found = new MaterialPhysics();
                int index = -1;
                float delta = float.MaxValue;
                foreach (var fv in Items)
                {
                    float d = fv.Frequency - freq;
                    if (d < delta)
                    {
                        delta = d;
                        index = Items.IndexOf(fv);
                    }
                }
                // finish
                if (index != -1)
                {
                    found = Items[index];
                }
                return found;
            }
        }
    }
}
