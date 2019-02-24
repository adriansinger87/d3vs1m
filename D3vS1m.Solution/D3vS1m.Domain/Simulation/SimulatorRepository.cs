using D3vS1m.Domain.Data.Repositories;
using D3vS1m.Domain.System.Enumerations;
using System.Collections.Generic;
using System.Linq;

namespace D3vS1m.Domain.Simulation
{
    public class SimulatorRepository : RepositoryBase<ISimulatable>
    {
        public SimulatorRepository()
        {
            base.Name = "simulation models";


        }

        // -- properties

        public List<ISimulatable> Items { get { return _items; } }

        // -- indexer

        /// <summary>
        /// id-based intexer to be able to get the instance with the same id.
        /// </summary>
        /// <param name="name">name field in the simulator instances</param>
        /// <returns>the first instance of T with the matching Id property</returns>
        public ISimulatable this[string name]
        {
            get
            {
                var found = _items.FirstOrDefault(s => s.Name == name);
                return found;
            }
        }

        public ISimulatable this[SimulationModels type]
        {
            get
            {
                var found = _items.FirstOrDefault(s => s.Type == type);
                return found;
            }
        }
    }
}
