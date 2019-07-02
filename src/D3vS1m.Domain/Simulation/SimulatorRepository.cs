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
        /// id-based intexer to be able to get the instance with the same id.
        /// </summary>
        /// <param name="id">id property in the simulator instances</param>
        /// <returns>the first instance of T with the matching Id property</returns>
        public ISimulatable this[string id] => Items.FirstOrDefault(s => s.Id == id);

        public ISimulatable this[SimulationModels type] => Items.FirstOrDefault(s => s.Type == type);

    }
}
