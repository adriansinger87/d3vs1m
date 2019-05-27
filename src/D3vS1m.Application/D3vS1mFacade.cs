using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Communication;
using D3vS1m.Application.Energy;
using D3vS1m.Application.Network;
using D3vS1m.Application.Scene;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using Sin.Net.Domain.Logging;
using System;
using System.Collections.Generic;

namespace D3vS1m.Application
{
    [Serializable]
    public class D3vS1mFacade : ISimulationFacadable
    {
        // -- fields

        // -- constructors

        public D3vS1mFacade()
        {
            // HACK: generic is not working for session state serialization based on json strings
            /*
             * TODO: make all models explicit and the facade will cover the entire simulation
             * the runtime needs the ability to acess the models through a list or so not a repo
             */
            Simulators = new SimulatorRepository();
        }

        // -- methods

        /// <summary>
        /// Registers all relevant simulation models in the repository and
        /// sets up all simulator arguments.
        /// </summary>
        public void RegisterPredefined(RuntimeBase runtime)
        {
            Log.Trace("Register simulation models for D3vS1m Application");

            // arguments
            var netArgs = new NetworkArgs();
            var sceneArgs = new InvariantSceneArgs();
            var simpleAntennaArgs = new SimpleAntennaArgs();
            var sphericAntennaArgs = new SphericAntennaArgs();
            var radioArgs = new AdaptedFriisArgs();
            var comArgs = new WirelessCommArgs();
            var energyArgs = new BatteryArgs();

            // network
            Register(new PeerToPeerNetworkSimulator(runtime), netArgs);

            // scene
            Register(new SceneSimulator(runtime), sceneArgs);

            // radio channel
            Register(new AdaptedFriisSimulator(runtime), new ArgumentsBase[] {
                radioArgs,
                comArgs,
                sceneArgs
            });

            // anntenna
            Register(new SimpleAntennaSimulator(runtime), simpleAntennaArgs);
            Register(new SphericAntennaSimulator(runtime), sphericAntennaArgs);

            // devices

            // communication
            Register(new LRWPANSimulator(runtime), comArgs);

            // energy
            Register(new BatteryPackSimulator(runtime), new ArgumentsBase[] {
                energyArgs,
                runtime.Arguments
            });
        }

        /// <summary>
        /// Adds a concretion of ISimulatable to the repository of simulation models
        /// </summary>
        /// <param name="simulator">simulation model instance</param>
        /// <returns>Gives the model back</returns>
        public ISimulatable Register(ISimulatable simulator)
        {
            if (Simulators.Contains(simulator))
            {
                Log.Error($"The Simulation model '{simulator.Name}' must not registered twice.");
                return simulator;
            }
            Simulators.Add(simulator);
            return simulator;
        }

        /// <summary>
        /// Adds a concretion of ISimulatable to the repository of simulation models
        /// and adds the argument instance
        /// </summary>
        /// <param name="simulator">simulation model instance</param>
        /// <param name="args">The argument instance for the model</param>
        /// <returns>Gives the model back</returns>
        public ISimulatable Register(ISimulatable simulator, ArgumentsBase args)
        {
            simulator.With(args);
            return Register(simulator);
        }

        /// <summary>
        /// Adds a concretion of ISimulatable to the repository of simulation models
        /// </summary>
        /// <param name="simulator">simulation model instance</param>
        /// <param name="argsArray">The array of argument instances for the model</param>
        /// <returns>Gives the model back</returns>
        public ISimulatable Register(ISimulatable simulator, ArgumentsBase[] argsArray)
        {
            foreach (var a in argsArray)
            {
                simulator.With(a);
            }
            return Register(simulator);
        }

        // -- properties

        public SimulatorRepository Simulators { get; }

        public Dictionary<string, ArgumentsBase[]> Arguments { get; }
    }
}