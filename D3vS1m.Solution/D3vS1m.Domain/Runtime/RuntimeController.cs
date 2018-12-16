using D3vS1m.Domain.Enumerations;
using D3vS1m.Domain.Runtime.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.Runtime
{
    public class RuntimeController
    {
        // -- fields

        private ISimulationRunnable _currentState;

        private ISimulationRunnable _AntennaState;
        private ISimulationRunnable _SceneState;
        private ISimulationRunnable _ChannelState;
        private ISimulationRunnable _DevicesState;
        private ISimulationRunnable _NetworkState;
        private ISimulationRunnable _CommunicationState;
        private ISimulationRunnable _EnergyState;

        // -- constructor

        public RuntimeController()
        {
            _AntennaState = new AntennaState(this);
        }

        // -- methods

        public void Start()
        {
            // TODO: implement the Run Method in a loop
        }

        public void Stop()
        {
            // TODO: implement something to stop simulation
        }

        private void Run()
        {
            /*
             * TODO:
             * - implement repositiory pattern of simulation models with name as key and concrete simulators with arguments and
             * - inject them into state machine
             * TODO: think about user defined FIFO for running the models instead of static calls in a hard coded order
             * TODO: write unit tests for rumtime controller and modularized simulation models
             */

            // what the environment dores...
            _currentState.RunScene();

            // what alle devices do...
            _currentState.RunDevices();

            // what the physical channel looks like...
            _currentState.RunChannel();

            //  what the physical transmitter / receiver does
            _currentState.RunAntenna();

            // what the network topology looks lke
            _currentState.RunNetwork();

            // what the communication stack does...
            _currentState.RunCommunication();

            // what the energy consumption does...
            _currentState.RunEnergy();
        }

        // -- properties

        public RuntimeStates CurrentState { get { return _currentState.State; } }

    }
}
