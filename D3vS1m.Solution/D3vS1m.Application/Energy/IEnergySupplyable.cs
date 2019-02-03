using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Energy
{
    public interface IEnergySupplyable
    {
        IEnergySupplyable Discharge(float current, TimeSpan time);

        IEnergySupplyable Charge(float current, TimeSpan time);
    }
}
