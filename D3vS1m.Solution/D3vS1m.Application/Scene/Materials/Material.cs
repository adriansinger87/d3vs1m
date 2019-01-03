using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Scene.Materials
{
    public partial class Material
    {
        public Material()
        {
            UUID = Guid.NewGuid().ToString();
            Name = "Material";

            Physics = new PhysicsRepository();
        }

        public Material(string name, MaterialPhysics physic) : this()
        {
            this.Name = name;
            this.Physics.Add(physic);
        }

        /// <summary>
        /// Gets or sets the Name property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the unique identifier
        /// </summary>
        public string UUID { get; private set; }

        public PhysicsRepository Physics { get; set; }
    }
}
