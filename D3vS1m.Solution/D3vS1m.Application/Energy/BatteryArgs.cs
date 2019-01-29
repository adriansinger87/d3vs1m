using D3vS1m.Domain.Data.Arguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Energy
{
    public class BatteryArgs : ArgumentsBase
    {
        public BatteryArgs()
        {
            Name = Models.BatteryPack;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
