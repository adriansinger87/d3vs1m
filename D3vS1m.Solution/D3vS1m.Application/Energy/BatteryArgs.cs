using D3vS1m.Application.Devices;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Energy
{
    public class BatteryArgs : ArgumentsBase
    {
        // 

        public BatteryArgs()
        {
            Name = Models.BatteryPack;

            Batteries = new List<BatteryPack>();
        }

        // -- methods

        public override string ToString()
        {
            return base.ToString();
        }

        // -- properties

        public List<BatteryPack> Batteries { get; set; }
    }
}
