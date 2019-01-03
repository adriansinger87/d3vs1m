﻿using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Events;
using D3vS1m.Domain.System.Enumerations;

namespace D3vS1m.Domain.Simulation
{
    public abstract class SimulatorBase : ISimulatable
    {
        // -- events

        /// <summary>
        /// Shall be fired at first, when the execution of the simulation model starts 
        /// </summary>
        public event SimulatorEventHandler OnExecuting;

        /// <summary>
        /// Shall be fired at last, when the execution of the simulation model has finished 
        /// </summary>
        public event SimulatorEventHandler Executed;

        // -- methods

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
        protected void BeforeExecution()
        {
            OnExecuting?.Invoke(this, new SimulatorEventArgs(this.Arguments));
        }

        /// <summary>
        /// The method shall be called in the Run method after the run of the concrete simulator
        /// </summary>
        protected void AfterExecution()
        {
            Executed?.Invoke(this, new SimulatorEventArgs(this.Arguments));
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

        public abstract ArgumentsBase Arguments { get; }

        public abstract string Name { get; }

        public abstract SimulationModels Model { get; }
    }
}