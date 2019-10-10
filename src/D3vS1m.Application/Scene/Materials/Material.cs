using System;
using System.Collections.Generic;

namespace D3vS1m.Application.Scene.Materials
{
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

        public static List<Material> CreateDemoMaterials()
        {
            var freq = 2405.0F;
            var list = new List<Material>
            {
                new Material("Beton", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 3,
                    RelativePermeability = 1.0F,
                    RelativePermittivity = 9.0F }
                    .CalcReflectionValues()),
                new Material("Gips", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 1.5F,
                    RelativePermeability = 1,
                    RelativePermittivity = 1 }
                    .CalcReflectionValues()),
                new Material("Holzfassade", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 10.0F,
                    RelativePermeability = 1.0F,
                    RelativePermittivity = 2.3345F }
                    .CalcReflectionValues()),
                new Material("Holz mittel", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 3.44F,
                    RelativePermeability = 1,
                    RelativePermittivity = 1.5905F }
                    .CalcReflectionValues()),
                new Material("Holz dünn", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 1.5F,
                    RelativePermeability = 1,
                    RelativePermittivity = 2.33F }
                    .CalcReflectionValues()),
                new Material("Stahl", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 7.0F,
                    RelativePermeability = 100000.0F,
                    RelativePermittivity = 2.3345F }
                    .CalcReflectionValues()),
                new Material("Metall Kleinteile", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 7,
                    RelativePermeability = 1,
                    RelativePermittivity = 100000.0F }
                    .CalcReflectionValues()),
                new Material("Aussenglas", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 0.77F,
                    RelativePermeability = 1,
                    RelativePermittivity = 6 }
                    .CalcReflectionValues()),
                new Material("Innenglas", new MaterialPhysics {
                    Frequency = freq,
                    PenetrationLoss = 0.77F,
                    RelativePermeability = 1,
                    RelativePermittivity = 19 }
                    .CalcReflectionValues())
            };

            return list;
        }

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
