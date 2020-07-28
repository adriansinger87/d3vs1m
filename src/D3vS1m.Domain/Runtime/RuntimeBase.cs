using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Events;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using Sin.Net.Domain.Persistence;
using Sin.Net.Domain.Persistence.Adapter;
using Sin.Net.Domain.Persistence.Logging;
using Sin.Net.Domain.Persistence.Settings;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace D3vS1m.Domain.Runtime
{
    /// <summary>
    /// Abstract class with some base functionality to setup and run the desired simulation models
    /// </summary>
    [Serializable]
    public abstract class RuntimeBase
    {
        // -- fields

        protected SimulatorRepository _simRepo;

        protected bool _isValid;
        protected bool _isRunning;
        protected bool _stopping;

        // -- events

        /// <summary>
        /// The event gets fired when the simulation starts the first iteration. 
        /// </summary>
        [field: NonSerialized]
        public event SimulatorEventHandler Started;

        /// <summary>
        /// The event gets fired when the simulation stoppes the last iteration. 
        /// </summary>
        [field: NonSerialized]
        public event SimulatorEventHandler Stopped;

        /// <summary>
        /// The event gets fired when the execution of all simulation models has finished one iteration 
        /// </summary>
        [field: NonSerialized]
        public event SimulatorEventHandler IterationPassed;

        // -- methods

        public abstract RuntimeBase With(ArgumentsBase args);

        /// <summary>
        /// Takes the Simulator Repository instance with all ready-to-go simulators and sets the internal valid-state to invalid.
        /// This ensures that the concretion of this base class implements a validation method and runs it before the simulation. 
        /// </summary>
        /// <param name="simulatorRepo">The repository instance</param>
        /// <returns>Returns the calling instance for method chaining</returns>
        public RuntimeBase BindSimulators(SimulatorRepository simulatorRepo)
        {
            _isValid = false;
            _simRepo = simulatorRepo;
            return this;
        }

        public RuntimeBase SetupSimulators(Action<SimulatorRepository> action)
		{
            action(_simRepo);
            return this;
        }

        public RuntimeBase ExportResults(IExportable exporter, SettingsBase setting, IAdaptable adapter)
		{
            exporter
                .Setup(setting)
                .Build(_simRepo.AllArguments, adapter)
                .Export();

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
                Log.Info($"# Start of simulation");
                _isRunning = condition(this);

                // fire event on iteration starts
                Started?.Invoke(this, new SimulatorEventArgs(Arguments));

                var list = _simRepo.SortActiveSimulators();
                while (_isRunning)
                {
                    foreach (ISimulatable sim in list)
                    {
                        sim.Run();
                    }

                    // fire event that one iteration of all simulation models has finished
                    IterationPassed?.Invoke(this, new SimulatorEventArgs(Arguments));

                    // separate flag to ensure that the condition method does not overwrite the stop action
                    if (!_stopping) 
                    {
                        _isRunning = condition(this);
                    }
                }
            })
            .ContinueWith((t) => {
                if (t.Status == TaskStatus.Faulted)
                {
                    Log.Error($"{t.Exception.InnerExceptions.Count} exception occured during simulation.");
                    foreach (var e in t.Exception.InnerExceptions)
                    {
                        Log.Fatal(e);
                    }
                }

                Log.Info($"# End of simulation");
                Stopped?.Invoke(this, new SimulatorEventArgs(Arguments));
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

        /// <summary>
        /// Gets the information, if the validation was successful or not.
        /// </summary>
        public bool IsValid { get { return _isValid; } }

        /// <summary>
        /// Gets the information, if the simulation is running or not. 
        /// </summary>
        public bool IsRunning { get { return _isRunning; } }

        // -- indexer

        /// <summary>
        /// Gets the repository for the attached simulators.
        /// </summary>
        public SimulatorRepository Simulators => _simRepo;

    }
}
