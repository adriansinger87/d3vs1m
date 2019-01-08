using D3vS1m.Domain.Data.Scene;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.System.Constants
{
    public partial class Const
    {
        public class Device
        {
            public const string Name = "Device";
            public const string Description = "basic device type";
            public static readonly Vertex Position = new Vertex();
            public static readonly Angle Orientation = new Angle();
        }
    }
}
