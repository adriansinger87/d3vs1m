using D3vS1m.Domain.Enumerations;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.Runtime
{
    public abstract class RuntimeBase
    {
        // -- fields

        protected SimulatorRepository _simRepo;

        private ISimulatable _currentSim;

        protected bool _isValid;

        // -- methods

        public RuntimeBase Setup(SimulatorRepository simulatorRepo)
        {
            _isValid = false;
            _simRepo = simulatorRepo;
            return this;
        }

        public abstract bool Validate();

        public void Stop()
        {
            // TODO: implement something to stop simulation
        }

        public void Run()
        {
            if (!_isValid)
            {
                // stop continous running
                return;
            }

            // TODO: implement continous running progress and break conditions maybe with time based or event based approach 
            // TODO: implement Run and Stop as async methods

            foreach (ISimulatable sim in _simRepo)
            {
                _currentSim = sim;

                _currentSim.Execute();
            }
        }

        // -- properties
    }
}
