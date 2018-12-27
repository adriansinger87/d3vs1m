using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Enumerations;
using D3vS1m.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.Simulation
{
    public interface ISimulatable
    {
        // -- methods

        /// <summary>
        /// Attaches the specific arguments to the simulator concretion 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns>the calling instance of the simulator</returns>
        ISimulatable With(BaseArgs arguments);

        /// <summary>
        /// Runs the implementation of the simulation model
        /// </summary>
        void Execute();

        // -- properties

        /// <summary>
        /// Gets the name property of the simulation model
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the type of the simulation model
        /// </summary>
        SimulationModels Model { get; }
    }
}
