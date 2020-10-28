using System;
using System.Collections.Generic;

namespace D3vS1m.Application.Scene.Materials
{
    /// <summary>
    /// This class represents a basic material and
    /// holds a list of physical parameters depending on the frequency.
    /// </summary>
    [Serializable]
    public partial class Material
    {

        // -- constructor

        public Material()
        {
            Uuid = Guid.NewGuid().ToString();
            Name = "Material";

            Physics = new List<MaterialPhysics>();
        }

        public Material(string name, MaterialPhysics physic) : this()
        {
            this.Name = name;
            this.Physics.Add(physic);
        }

        // -- methods

        public override string ToString() => $"{Name} with {Physics.Count} physical properties.";

        // -- properties

        /// <summary>
        /// Gets or sets the Name property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the unique identifier
        /// </summary>
        public string Uuid { get; set; }

        /// <summary>
        /// Gets all frequency dependend material physic properties of type PhysicsRepository
        /// </summary>
        public List<MaterialPhysics> Physics { get; set; }

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
                foreach (var fv in Physics)
                {
                    float d = fv.Frequency - freq;
                    if (d < delta)
                    {
                        delta = d;
                        index = Physics.IndexOf(fv);
                    }
                }
                // finish
                if (index != -1)
                {
                    found = Physics[index];
                }
                return found;
            }
        }
    }
}
