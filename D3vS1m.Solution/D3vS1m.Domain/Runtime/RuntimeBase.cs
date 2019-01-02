using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Events;
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

        protected bool _isValid;
        protected bool _isRunning;

        // -- events

        /// <summary>
        /// The event gets fired when the execution of all simulation models has finished one iteration 
        /// </summary>
        public event SimulatorEventHandler IterationPassed;

        // -- methods

        public RuntimeBase Setup(SimulatorRepository simulatorRepo)
        {
            _isValid = false;
            _simRepo = simulatorRepo;
            return this;
        }

        public abstract bool Validate();

        public virtual void Stop()
        {
            Log.Trace("Simulation runtime stopped");
            _isRunning = false;
        }

        #region RunAsync
        public virtual async Task RunAsync()
        {
            // go baby go!
            await RunAsync((runtime) => { return true; });
        }

        public virtual async Task RunAsync(int count)
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

        public virtual async Task RunAsync(Func<RuntimeBase, bool> condition)
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

                    // fire event that one siulation run is finished
                    IterationPassed?.Invoke(this, new SimulatorEventArgs(Arguments));
                }
                Log.Trace($"Leave simulation task");
            });
        }
        #endregion

        // -- properties

        /// <summary>
        /// Gets the specific arguments for the concrete runtime implementation as base class 
        /// </summary>
        public abstract ArgumentsBase Arguments { get; }
    }
}
