using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Events;
using D3vS1m.Domain.System.Enumerations;

namespace D3vS1m.Domain.Simulation
{
    public interface ISimulatable
    {
        // -- events

        /// <summary>
        /// Shall be fired at first, when the execution of the simulation model starts 
        /// </summary>
        event SimulatorEventHandler OnExecuting;

        /// <summary>
        /// Shall be fired at last, when the execution of the simulation model has finished 
        /// </summary>
        event SimulatorEventHandler Executed;

        // -- methods

        /// <summary>
        /// Attaches the specific arguments to the simulator concretion 
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns>the calling instance of the simulator</returns>
        ISimulatable With(ArgumentsBase arguments);

        /// <summary>
        /// Runs the implementation of the simulation model
        /// </summary>
        void Run();

        // -- properties

        ArgumentsBase Arguments { get; }

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
