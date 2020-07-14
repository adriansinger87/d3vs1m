using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using Sin.Net.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace D3vS1m.Domain.Simulation
{
    [Serializable]
    public class SimulatorRepository : RepositoryBase<ISimulatable>
    {
        public SimulatorRepository() : base()
        {
            base.Name = "simulation models";
        }

        // -- methods

        public override void Clear()
        {
            if (Items == null)
            {
                Items = new List<ISimulatable>();
            }
            else
            {
                Items.Clear();
            }
        }

        public List<ISimulatable> SortActiveSimulators()
        {
            return Items
                .Where(s => s.Arguments.Active)
                .OrderBy(s => s.Arguments.Index).ToList();
        }

        public void SetArguments(string id, ArgumentsBase args)
        {
            this[id].With(args);
        }

        public ISimulatable GetByName(string name)
        {
            return Items.FirstOrDefault(s => s.Name == name);
        }

        // -- properties

        public ArgumentsBase[] AllArguments => Items.Select(s => s.Arguments).ToArray();

        // -- indexer

        /// <summary>
        /// Key-based intexer to be able to get the instance with the same Key.
        /// </summary>
        /// <param name="key">The key property in the simulator instances</param>
        /// <returns>the first instance of T with the matching Id property</returns>
        public ISimulatable this[string key] => Items.FirstOrDefault(s => s.Key == key);

        public ISimulatable this[SimulationTypes type] => Items.FirstOrDefault(s => s.Type == type);

    }
}
