using D3vS1m.Domain.Data.Arguments;
using System.Collections.Generic;

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

        public override ArgumentsBase GetDefault()
        {
            return new BatteryArgs();
        }

        // -- properties

        public List<BatteryPack> Batteries { get; set; }
    }
}
