
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.System.Constants;
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
            
            Parts = new PartsRepository();

            Name = Domain.System.Constants.Const.Device.Name;
            Description = Domain.System.Constants.Const.Device.Description;
            Position = Domain.System.Constants.Const.Device.Position;
            Orientation = Domain.System.Constants.Const.Device.Orientation;
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
