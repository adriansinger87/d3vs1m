using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.System.Constants;
using System;

namespace D3vS1m.Application.Devices
{
    [Serializable]
    public class BasicDevice
    {
        public BasicDevice()
        {
            UUID = Guid.NewGuid().ToString();

            Parts = new PartsRepository();

            Name = Const.Device.Name;
            Description = Const.Device.Description;
            Position = new Vertex(Const.Device.PosX, Const.Device.PosY, Const.Device.PosZ);
            Orientation = new Angle(Const.Antenna.Azimuth, Const.Antenna.Elevation);
        }

        // -- public methods

        public override string ToString()
        {
            return $"{Name} {Position}";
        }

        // -- properties

        public string Name { get; set; }
        public Vertex Position { get; set; }

        public string UUID { get; set; }
        public string Description { get; set; }
        public Angle Orientation { get; set; }

        public PartsRepository Parts { get; }
    }
}
