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

        public List<ISimulatable> Items { get; private set; }

        // -- indexer

        /// <summary>
        /// Key-based intexer to be able to get the instance with the same Key.
        /// </summary>
        /// <param name="key">The key property in the simulator instances</param>
        /// <returns>the first instance of T with the matching Id property</returns>
        public ISimulatable this[string key] => Items.FirstOrDefault(s => s.Key == key);

        public ISimulatable this[SimulationTypes type] => Items.FirstOrDefault(s => s.Type == type);

        public int Count => Items.Count;

		// -- properties

		public string Name { get; set; }

		// -- constructor(s)

		public SimulatorRepository() : base()
        {
            Name = "simulation models";
            Items = new List<ISimulatable>();
        }

        // -- methods

        public void Add(ISimulatable simulator)
        {
            Items.Add(simulator);
        }

        public bool Contains(ISimulatable simulator)
        {
            return Items.Contains(simulator);
        }

        public void Clear()
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
        
        public ArgumentsBase[] AllArguments()
		{
            return Items.Select(s => s.Arguments).ToArray();

        }

	
	}
}
