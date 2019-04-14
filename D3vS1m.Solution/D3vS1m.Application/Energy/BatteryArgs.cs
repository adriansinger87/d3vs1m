using D3vS1m.Domain.Data.Arguments;
using System.Collections.Generic;

namespace D3vS1m.Application.Energy
{
    public class BatteryArgs : ArgumentsBase
    {
        // 

        public BatteryArgs()
        {
            Reset();
        }

        // -- methods

        public override void Reset()
        {
            Name = Models.BatteryPack;
            Batteries = new List<BatteryPack>();
        }

        // -- properties

        public List<BatteryPack> Batteries { get; set; }
    }
}
