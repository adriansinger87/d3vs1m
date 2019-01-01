using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Logging;
using System;
using System.Threading.Tasks;

namespace D3vS1m.Domain.Runtime
{
    public abstract class RuntimeBase
    {
        // -- fields

        protected SimulatorRepository _simRepo;

        private ISimulatable _currentSim;

        protected bool _isValid;

        protected bool _isRunning;

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
            Log.Trace("Simulation runtime stopped");
            _isRunning = false;
        }

        public async Task RunAsync()
        {
            // go baby go!
            await RunAsync((runtime) => { return true; });
        }

        public async Task RunAsync(int count)
        {
            int i = 0;

            // run for count times
            await RunAsync((runtime) =>
            {
                if (i >= count)
                {
                    return false;
                }

                i++;
                return true;
            });
        }

        public async Task RunAsync(Func<RuntimeBase, bool> condition)
        {
            if (!_isValid)
            {
                // stop running
                Log.Trace("Simulation is invalid or was not validated");
                Stop();
                return;
            }

            await Task.Run(() =>
            {
                _isRunning = condition(this);
                while (_isRunning)
                {
                    foreach (ISimulatable sim in _simRepo)
                    {
                        sim.Run();
                    }
                    _isRunning = condition(this);
                }
                Log.Trace($"Leave simulation task");
            });
        }
        
        // -- properties
    }
}
