using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Communication;
using D3vS1m.Application.Energy;
using D3vS1m.Application.Network;
using D3vS1m.Application.Scene;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using Sin.Net.Domain.Persistence.Logging;
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

        public D3vS1mFactory() : base()
        {
            base.Simulators = new SimulatorRepository();
        }

        // -- methods

        public override ArgumentsBase NewArgument(string name)
        {
            switch (name)
            {
                case Models.Network.Name:
                    return new NetworkArgs();

                case Models.Scene.Name:
                    return new InvariantSceneArgs();

                case Models.Channel.AdaptedFriis.Name:
                    return new AdaptedFriisArgs();

                case Models.Antenna.Spheric.Name:
                    return new SphericAntennaArgs();

                case Models.Antenna.Simple.Name:
                    return new SimpleAntennaArgs();

                case Models.Antenna.Flat.Name:
                    return new FlatAntennaArgs();

                case Models.Communication.LrWpan.Name:
                    return new WirelessCommArgs();

                case Models.Energy.Battery.Name:
                    return new BatteryArgs();

                default:
                    return null;
            }
        }

        public override ISimulatable NewSimulator(string name, RuntimeBase runtime)
        {
            switch (name)
            {
                case Models.Network.Name:
                    return new PeerToPeerNetworkSimulator(runtime);

                case Models.Scene.Name:
                    return new SceneSimulator(runtime);

                case Models.Channel.AdaptedFriis.Name:
                    return new AdaptedFriisSimulator(runtime);

                case Models.Antenna.Spheric.Name:
                    return new SphericAntennaSimulator(runtime);

                case Models.Antenna.Simple.Name:
                    return new SimpleAntennaSimulator(runtime);

                case Models.Antenna.Flat.Name:
                    return new SimpleAntennaSimulator(runtime);

                case Models.Communication.LrWpan.Name:
                    return new LRWPANSimulator(runtime);

                case Models.Energy.Battery.Name:
                    return new BatteryPackSimulator(runtime);

                default:
                    return null;
            }
        }

        public override ArgumentsBase[] GetPredefinedArguments()
        {
            var args = new List<ArgumentsBase>
            {
                NewArgument(Models.Network.Name),
                NewArgument(Models.Scene.Name),
                NewArgument(Models.Channel.AdaptedFriis.Name),
                NewArgument(Models.Antenna.Spheric.Name),
                //NewArgument(Models.Antenna.Simple.Key),
                //(Models.Antenna.Flat.Key),
                NewArgument(Models.Communication.LrWpan.Name),
                NewArgument(Models.Energy.Battery.Name)

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

        public override RuntimeBase SetupRuntime(ArgumentsBase[] args, RuntimeBase runtime)
        {
            base.Simulators.Clear();
            foreach (var arg in args)
            {
                RegisterSimulator(
                    NewSimulator(arg.Name, runtime), arg);
            }

            runtime.BindSimulators(Simulators);
            return runtime;
        }

        // -- properties

    }
}