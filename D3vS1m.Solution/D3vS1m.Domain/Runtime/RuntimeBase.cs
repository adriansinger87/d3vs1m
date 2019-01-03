﻿using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Events;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Logging;
using System;
using System.Threading.Tasks;

namespace D3vS1m.Domain.Runtime
{
    /// <summary>
    /// Abstract class with some base functionality to setup and run the desired simulation models
    /// </summary>
    public abstract class RuntimeBase
    {
        // -- fields
        
        protected SimulatorRepository _simRepo;

        protected bool _isValid;
        protected bool _isRunning;
        protected bool _stopping;

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

        /// <summary>
        /// The conrete runtime implementation implements the validation of all simulation models here.
        /// The Method is called before the iteration of all registered models in the RunAsync method.
        /// </summary>
        /// <returns></returns>
        public abstract bool Validate();

        /// <summary>
        /// Stops the simulation after finishing the current iteration of the simulation.
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
            _stopping = true;
            Log.Trace("Simulation runtime stopping");
        }
                
        #region RunAsync
        /// <summary>
        /// Start the iteration of the run method of all registered simulation models
        /// without any break condition
        /// </summary>
        /// <returns>The task object representing the async task</returns>
        public async Task RunAsync()
        {
            // go baby go!
            await RunAsync((runtime) => { return true; });
        }

        /// <summary>
        /// Start the iteration of the run method of all registered simulation models
        /// for a defined number of times
        /// </summary>
        /// <param name="count">Determines the number of iterations of all simulation models</param>
        /// <returns>The task object representing the async task</returns>
        public async Task RunAsync(int count)
        {
            int i = 0;
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

        /// <summary>
        /// Start the iteration of the run method of all registered simulation models
        /// as long as the condition method returns true
        /// </summary>
        /// <param name="condition">A method that determines the condition to continue or to end the simulation</param>
        /// <returns>The task object representing the async task</returns>
        public virtual async Task RunAsync(Func<RuntimeBase, bool> condition)
        {
            _stopping = false;  // reset the stopping flag before entering the async part of the method
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

                    if (!_stopping) // separate flag to ensure that the condition method does not overwrite the stop action
                    {
                        _isRunning = condition(this);
                    }

                    // fire event that one iteration of all siulation models has finished
                    IterationPassed?.Invoke(this, new SimulatorEventArgs(Arguments));
                }
                Log.Trace($"Leave simulation task");
            });
        }
        #endregion

        /// <summary>
        /// Returns the Name property of the Arguments instance
        /// </summary>
        /// <returns>result string</returns>
        public override string ToString()
        {
            return Arguments.Name;
        }

        // -- properties

        /// <summary>
        /// Gets the specific arguments for the concrete runtime implementation as base class 
        /// </summary>
        public abstract ArgumentsBase Arguments { get; }
    }
}