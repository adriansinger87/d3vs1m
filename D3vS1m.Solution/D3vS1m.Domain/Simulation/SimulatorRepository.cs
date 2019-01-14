using D3vS1m.Domain.Data.Repositories;
using System.Linq;

namespace D3vS1m.Domain.Simulation
{
    public class SimulatorRepository : RepositoryBase<ISimulatable>
    {
        public SimulatorRepository()
        {
            base.Name = "simulation models";
        }

        // -- indexer

        /// <summary>
        /// id-based intexer to be able to get the instance with the same id.
        /// </summary>
        /// <param name="id">id field in the simulator instances</param>
        /// <returns>the first instance of T with the matching Id property</returns>
        public ISimulatable this[string id]
        {
            get
            {
                var found = _items.FirstOrDefault(s => s.Id == id);
                return found;
            }
        }
    }
}
