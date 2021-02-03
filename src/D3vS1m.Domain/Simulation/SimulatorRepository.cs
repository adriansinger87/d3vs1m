using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace D3vS1m.Domain.Simulation
{
    [Serializable]
    public class SimulatorRepository
    {
        // -- fields

        private List<ISimulatable> _simulators;

        // -- indexer

        /// <summary>
        /// Key-based intexer to be able to get the instance with the same Key.
        /// </summary>
        /// <param name="key">The key property in the simulator instances</param>
        /// <returns>the first instance of T with the matching Id property</returns>
        public ISimulatable this[string key] => _simulators.FirstOrDefault(s => s.Key == key);

        public ISimulatable this[SimulationTypes type] => _simulators.FirstOrDefault(s => s.Type == type);

		// -- properties

		public string Name { get; set; }

		// -- constructor(s)

		public SimulatorRepository() : base()
        {
            Name = "simulation models";
        }

        // -- methods

        public void Clear()
        {
            if (_simulators == null)
            {
                _simulators = new List<ISimulatable>();
            }
            else
            {
                _simulators.Clear();
            }
        }

        public List<ISimulatable> SortActiveSimulators()
        {
            return _simulators
                .Where(s => s.Arguments.Active)
                .OrderBy(s => s.Arguments.Index).ToList();
        }

        public void SetArguments(string id, ArgumentsBase args)
        {
            this[id].With(args);
        }

        public ISimulatable GetByName(string name)
        {
            return _simulators.FirstOrDefault(s => s.Name == name);
        }

        public ArgumentsBase[] AllArguments()
		{
            return _simulators.Select(s => s.Arguments).ToArray();

        }
    }
}
