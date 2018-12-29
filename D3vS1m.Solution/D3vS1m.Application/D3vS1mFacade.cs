using D3vS1m.Application.Antenna;
using D3vS1m.Application.Channel;
using D3vS1m.Application.Network;
using D3vS1m.Application.Scene;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Simulation;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application
{
    public class D3vS1mFacade : ISimulationFacadable
    {
        // -- fields

        private SimulatorRepository _repo;

        // -- constructors

        public D3vS1mFacade()
        {
            SimulatorRepo = new SimulatorRepository();
        }

        // -- methods

        public void Register(ISimulatable simulator, ArgumentsBase args)
        {
            simulator.With(args);
            SimulatorRepo.Add(simulator);
        }

        public void RegisterSimulation()
        {
            // scene
            var scene = new SceneSimulator();
            Register(scene, new InvariantSceneArgs());

            // channel
            var friis = new AdaptedFriisSimulator();
            Register(friis, new AdaptedFriisArgs());

            // anntenna
            var antenna = new SimpleAntennaSimulator();
            Register(antenna, new SimpleAntennaArgs());

            // network
            var net = new PeerToPeerNetworkSimulator();
            Register(antenna, new NetworkArgs());

            // devices

            // communication

            // energy

        }

        // -- properties

        public SimulatorRepository SimulatorRepo { get; }
    }
}
