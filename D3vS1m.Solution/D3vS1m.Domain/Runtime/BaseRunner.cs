using System;
using System.Collections.Generic;
using System.Text;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Enumerations;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Logging;

namespace D3vS1m.Domain.Runtime
{
    public class BaseRunner : ISimulationRunnable
    {
        // -- fields

        protected ISimulatable _simulator;
        internal RuntimeController _control;

        // -- constructor

        public BaseRunner(ISimulatable simulator)
        {
            _simulator = simulator;
        }

        // -- methods

        public virtual void Setup(RuntimeController control)
        {
            _control = control;
        }

        public virtual void RunSimulation()
        {
            Log.Debug($"execute {Name}");
            _simulator.Execute();
        }

        public override string ToString()
        {
            return $"base runner: {Name}";
        }

        // -- properties

        public string Name { get { return _simulator.Name; } }
    }
}
