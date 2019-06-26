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
using System.Collections.Generic;

namespace D3vS1m.Application
{
    /// <summary>
    /// This is the implementation of the factory-pattern to create simulator-objects and attach arguments to it.
    /// </summary>
    public class D3vS1mFactory : FactoryBase
    {
        // -- fields

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

        public override ArgumentsBase NewArgument(string name)
        {
            switch (name)
            {
                case Models.Network.Key:
                    return new NetworkArgs();

                case Models.Scene.Key:
                    return new InvariantSceneArgs();

                case Models.Channel.AdaptedFriis.Key:
                    return new AdaptedFriisArgs();

                case Models.Antenna.Spheric.Key:
                    return new SphericAntennaArgs();

                case Models.Antenna.Simple.Key:
                    return new SimpleAntennaArgs();

                case Models.Antenna.Flat.Key:
                    return new FlatAntennaArgs();

                case Models.Communication.LrWpan.Key:
                    return new WirelessCommArgs();

                case Models.Energy.Battery.Key:
                    return new BatteryArgs();

                default:
                    return null;
            }
        }

        public override ISimulatable NewSimulator(string name)
        {
            switch (name)
            {
                case Models.Network.Key:
                    return new PeerToPeerNetworkSimulator(_runtime);

                case Models.Scene.Key:
                    return new SceneSimulator(_runtime);

                case Models.Channel.AdaptedFriis.Key:
                    return new AdaptedFriisSimulator(_runtime);

                case Models.Antenna.Spheric.Key:
                    return new SphericAntennaSimulator(_runtime);

                case Models.Antenna.Simple.Key:
                    return new SimpleAntennaSimulator(_runtime);

                case Models.Antenna.Flat.Key:
                    return new SimpleAntennaSimulator(_runtime);

                case Models.Communication.LrWpan.Key:
                    return new LRWPANSimulator(_runtime);

                case Models.Energy.Battery.Key:
                    return new BatteryPackSimulator(_runtime);

                default:
                    return null;
            }
        }

        public override ArgumentsBase[] GetPredefinedArguments()
        {
            var args = new List<ArgumentsBase>
            {
                NewArgument(Models.Network.Key),
                NewArgument(Models.Scene.Key),
                NewArgument(Models.Channel.AdaptedFriis.Key),
                NewArgument(Models.Antenna.Spheric.Key),
                //NewArgument(Models.Antenna.Simple.Key),
                //(Models.Antenna.Flat.Key),
                NewArgument(Models.Communication.LrWpan.Key),
                NewArgument(Models.Energy.Battery.Key)

            };
            return args.ToArray();
        }
        
        /// <summary>
        /// Adds a concretion of ISimulatable to the repository of simulation models
        /// and adds the argument instance
        /// </summary>
        /// <param name="simulator">simulation model instance</param>
        /// <param name="args">The argument instance for the model</param>
        /// <returns>Gives the model back</returns>
        public override ISimulatable RegisterSimulator(ISimulatable simulator, ArgumentsBase args)
        {
            simulator.With(args);
            if (Simulators.Contains(simulator))
            {
                Log.Error($"The Simulation model '{simulator.Name}' must not registered twice.");
                return simulator;
            }
            Simulators.Add(simulator);
            return simulator;
        }

        public override RuntimeBase SetupSimulation(ArgumentsBase[] args)
        {
            base.Simulators.Clear();
            foreach (var arg in args)
            {
                RegisterSimulator(NewSimulator(arg.Name), arg);
            }

            _runtime.Setup(Simulators);
            return _runtime;
        }

        // -- properties

        public override ArgumentsBase[] Arguments => base.Simulators.AllArguments;

    }
}