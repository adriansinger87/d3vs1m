using D3vS1m.Domain.System.Enumerations;
using Sin.Net.Domain.Repository;
using System;
using System.Linq;

namespace D3vS1m.Application.Devices
{
    [Serializable]
    public class PartsRepository : RepositoryBase<PartBase>
    {
        public PartsRepository()
        {
            Name = "part repository";
        }

        // -- methods

        public PartBase GetPowerSupply()
		{
            return this.Items.FirstOrDefault(p => p.Type == PartTypes.PowerSupply);
		}

        // -- properties

        public bool HasPowerSupply => this.Items.Any(p => p.Type == PartTypes.PowerSupply);
    }
}
