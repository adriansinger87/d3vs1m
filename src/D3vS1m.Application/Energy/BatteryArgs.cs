using D3vS1m.Domain.Data.Arguments;
using System;
using System.Collections.Generic;

namespace D3vS1m.Application.Energy
{
    [Serializable]
    public class BatteryArgs : ArgumentsBase
    {
        // 

        public BatteryArgs() : base()
        {
            Key = Models.Energy.Battery.Key;
            Reset();
        }

        // -- methods

        public override void Reset()
        {
            Name = Models.Energy.Battery.Name;
            Batteries = new List<BatteryPack>();
        }

        // -- properties

        public List<BatteryPack> Batteries { get; set; }
    }
}
