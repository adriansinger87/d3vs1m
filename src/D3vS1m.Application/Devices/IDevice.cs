using System;
using System.Collections.Generic;
using System.Text;
using D3vS1m.Domain.Data.Scene;

namespace D3vS1m.Application.Devices
{
	public interface IDevice
	{
        string Uuid { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        bool IsActive { get; set; }
        Vertex Position { get; set; }
        Angle Orientation { get; set; }
        PartsRepository Parts { get; }
        ControlBase Controls { get; }
	}
}
