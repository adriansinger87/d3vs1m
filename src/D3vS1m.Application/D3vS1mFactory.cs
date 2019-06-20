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
using System.Linq;

namespace D3vS1m.Application
{
    /// <summary>
    /// This is the implementation of the factory-pattern to create simulator-objects and attach arguments to it.
    /// </summary>
    public class D3vS1mFactory : FactoryBase
    {
        // -- fields

        private RuntimeBase _runtime;

        // -- constructors

        public D3vS1mFactory(RuntimeBase runtime) : base(runtime)
        {
            // HACK: generic is not working for session state serialization based on json strings
            /*
             * TODO: safe only the args in the session of a web-app
             */
            base.Simulators = new SimulatorRepository();
        }

        // -- methods

        /// <summary>
        /// Registers all relevant simulation models in the repository and
        /// sets up all simulator arguments.
        /// </summary>
        public override void RegisterPredefined()
        {
            Log.Trace("Register simulation models for D3vS1m Application");


            // network
            Register(new PeerToPeerNetworkSimulator(_runtime));

            // scene
            Register(new SceneSimulator(_runtime));

            // radio channel
            Register(new AdaptedFriisSimulator(_runtime));

            // anntenna
            Register(new SimpleAntennaSimulator(_runtime));
            Register(new SphericAntennaSimulator(_runtime));

            // devices

            // communication
            Register(new LRWPANSimulator(_runtime));

            // energy
            Register(new BatteryPackSimulator(_runtime));
        }

        /// <summary>
        /// Adds a concretion of ISimulatable to the repository of simulation models
        /// </summary>
        /// <param name="simulator">simulation model instance</param>
        /// <returns>Gives the model back</returns>
        public override ISimulatable Register(ISimulatable simulator)
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
        public override ISimulatable Register(ISimulatable simulator, ArgumentsBase args)
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
        public override ISimulatable Register(ISimulatable simulator, ArgumentsBase[] argsArray)
        {
            foreach (var a in argsArray)
            {
                simulator.With(a);
            }
            return Register(simulator);
        }

        // -- properties

    }
}