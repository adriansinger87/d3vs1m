using System;
using System.Collections.Generic;
using System.Text;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Enumerations;
using D3vS1m.Domain.Events;

namespace D3vS1m.Domain.Simulation
{
    public abstract class SimulatorEventBase
    {
        /// <summary>
        /// Shall be fired at first, when the execution of the simulation model starts 
        /// </summary>
        public event SimulatorEventHandler OnExecuting;

        /// <summary>
        /// Shall be fired at last, when the execution of the simulation model has finished 
        /// </summary>
        public event SimulatorEventHandler Executed;

        /// <summary>
        /// The abstract method will be implemented by the concrete simulation model and shall call the overloaded method
        /// </summary>
        protected abstract void BeforeExecution();

        /// <summary>
        /// This implementation calls the OnExecuting event and shall be used by the concrete simulation model
        /// </summary>
        /// <param name="e">The SimulatorEventArgs</param>
        protected void BeforeExecution(SimulatorEventArgs e)
        {
            OnExecuting?.Invoke(this, e);
        }

        /// <summary>
        /// The abstract method will be implemented by the concrete simulation model and shall call the overloaded method.
        /// </summary>
        protected abstract void AfterExecution();

        /// <summary>
        /// This implementation calls the Executed event and shall be used by the concrete simulation model
        /// </summary>
        /// <param name="e">The SimulatorEventArgs</param>
        protected void AfterExecution(SimulatorEventArgs e)
        {
            Executed?.Invoke(this, e);
        }

    }
}
