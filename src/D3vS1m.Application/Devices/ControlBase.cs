using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Devices
{
	public abstract class ControlBase
	{
		protected IDevice _device;

		protected ControlBase(IDevice device)
		{
			_device = device;
		}

		public abstract void Off();
		public abstract void On();
	}
}
