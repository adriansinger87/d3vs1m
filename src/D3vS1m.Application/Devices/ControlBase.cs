using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Devices
{
	public abstract class ControlBase
	{
		protected IDevice _device;

		public ControlBase(IDevice device)
		{
			_device = device;
		}

		public abstract void Off();
		public abstract void On();
	}
}
