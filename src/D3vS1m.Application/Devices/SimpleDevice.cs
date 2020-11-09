using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.System.Constants;
using System;

namespace D3vS1m.Application.Devices
{
    [Serializable]
    public class SimpleDevice : IDevice
    {
        public SimpleDevice()
        {
            Uuid = Guid.NewGuid().ToString();
            Name = Const.Device.Name;
            Description = Const.Device.Description;
            Position = new Vertex(Const.Device.PosX, Const.Device.PosY, Const.Device.PosZ);
            Orientation = new Angle(Const.Antenna.Azimuth, Const.Antenna.Elevation);
            IsActive = true;

            Parts = new PartsRepository();
            Controls = new SimpleControls(this);
        }

        // -- public methods

        public override string ToString()
        {
            return $"{Name} {Position}";
        }

        // -- properties

        public string Uuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Vertex Position { get; set; }
        public Angle Orientation { get; set; }
        public PartsRepository Parts { get; }
        public ControlBase Controls { get; }	
    }
}
