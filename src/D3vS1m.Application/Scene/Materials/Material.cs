using System;

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

            Physics = new PhysicsRepository();
        }

        public Material(string name, MaterialPhysics physic) : this()
        {
            this.Name = name;
            this.Physics.Add(physic);
        }

        // -- properties

        /// <summary>
        /// Gets or sets the Name property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the unique identifier
        /// </summary>
        public string Uuid { get; private set; }

        /// <summary>
        /// Gets all frequency dependend material physic properties of type PhysicsRepository
        /// </summary>
        public PhysicsRepository Physics { get; set; }
    }
}
