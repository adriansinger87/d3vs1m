using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Events;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.System.Enumerations;
using Sin.Net.Domain.Persistence.Logging;
using System;

namespace D3vS1m.Domain.Simulation
{
    [Serializable]
    public abstract class SimulatorBase : ISimulatable
    {
        // -- fields

        protected RuntimeBase _runtime;

        // -- events

        /// <summary>
        /// Shall be fired at first, when the execution of the simulation model starts 
        /// </summary>
        [field: NonSerialized]
        public event SimulatorEventHandler OnExecuting;

        /// <summary>
        /// Shall be fired at last, when the execution of the simulation model has finished 
        /// </summary>
        [field: NonSerialized]
        public event SimulatorEventHandler Executed;

        // -- constructor

        public SimulatorBase(RuntimeBase runtime)
        {
            Guid = global::System.Guid.NewGuid().ToString();

            if (runtime != null)
            {
                _runtime = runtime;
                _runtime.Started += OnStarted;
            }
        }

        // -- methods

        protected bool ConvertArgs<T>(ArgumentsBase input, ref T target)
        {
            if (input == null ||
                input is T == false)
            {
                return false;
            }
            else
            {
                target = (T)Convert.ChangeType(input, typeof(T));
                return true;
            }
        }

        protected ISimulatable ArgsNotAdded(string argsName)
        {
            Log.Warn($"The unnecessary arguments '{argsName}' were not added to the simulation model '{Name}'");
            return this;
        }

        /// <summary>
        /// Adds a concrete argument object to the simulator
        /// </summary>
        /// <param name="arguments">the arguments object,
        /// can be added several times depending on the needs of the simulator</param>
        /// <returns>the calling instance</returns>
        public abstract ISimulatable With(ArgumentsBase arguments);

        /// <summary>
        /// Main function to run the simulator 
        /// </summary>
        public abstract void Run();

        /// <summary>
        /// The method shall be called in the Run method before the run of the concrete simulator
        /// </summary>
        protected virtual void BeforeExecution()
        {
            Log.Trace($"- execute {this.Arguments.Name}");
            OnExecuting?.Invoke(this, new SimulatorEventArgs(this.Arguments));
        }

        /// <summary>
        /// The method shall be called in the Run method after the run of the concrete simulator
        /// </summary>
        protected virtual void AfterExecution()
        {
            Executed?.Invoke(this, new SimulatorEventArgs(this.Arguments));
        }

        protected virtual void OnStarted(object sender, SimulatorEventArgs e)
        {

        }

        /// <summary>
        /// Returns the name property
        /// </summary>
        /// <returns>result string</returns>
        public override string ToString()
        {
            return Name;
        }

        // --properties

        /// <summary>
        /// The settings that are specific for the runtime environment.
        /// </summary>
        public abstract ArgumentsBase Arguments { get; }

        public abstract string Name { get; }

        public string Guid { get; private set; }

        public abstract string Key { get; }

        public abstract SimulationTypes Type { get; }
    }
}
