
using D3vS1m.Domain.Data.Scene;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Devices
{
    public class BasicDevice
    {
        public BasicDevice()
        {
            UUID = Guid.NewGuid().ToString();
            Position = new Vector();
            Orientation = new Angle();
            Parts = new PartsRepository();

            // TODO: remove magic strings
            Name = "Device";
            Description = "basic device type";
        }

        // -- public methods

        public override string ToString()
        {
            return $"{Name} {Position}";
        }

        // -- properties

        public string UUID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Vector Position { get; set; }
        public Angle Orientation { get; set; }

        public PartsRepository Parts { get; }
    }
}
