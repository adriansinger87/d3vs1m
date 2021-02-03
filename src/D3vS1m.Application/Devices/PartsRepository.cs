using D3vS1m.Domain.System.Enumerations;

using System;
using System.Collections.Generic;
using System.Linq;

namespace D3vS1m.Application.Devices
{
    [Serializable]
    public class PartsRepository
    {
        // -- fields

        private List<PartBase> _items;

		// -- properties

		public string Name { get; set; }

		public bool HasPowerSupply => _items.Any(p => p.Type == PartTypes.PowerSupply);

        // -- constructors

        public PartsRepository()
        {
            Name = "part repository";
            _items = new List<PartBase>();
        }

        // -- methods

        public PartBase GetPowerSupply()
		{
            return _items.FirstOrDefault(p => p.Type == PartTypes.PowerSupply);
		}
    }
}
