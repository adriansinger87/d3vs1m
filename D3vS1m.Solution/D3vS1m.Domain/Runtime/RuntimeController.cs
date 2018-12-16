using D3vS1m.Domain.Enumerations;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Domain.Runtime
{
    public class RuntimeController
    {
        // -- fields

        private SimulationRunnerRepository _runnerRepo;

        private ISimulationRunnable _currentRunner;  

        // -- constructor

        public RuntimeController()
        {
            _runnerRepo = new SimulationRunnerRepository();
        }

        // -- methods

        public void Setup(ISimulationRunnable[] simulationRunners)
        {
            _runnerRepo.Clear();
            _runnerRepo.AddRange(simulationRunners);

            foreach(ISimulationRunnable simRunner in _runnerRepo)
            {
                simRunner.Setup(this);
            }
        }

        public void Stop()
        {
            // TODO: implement something to stop simulation
        }

        public void Run()
        {
            // TODO: implement continous running progress and break conditions maybe with time based or event based approach 
            // TODO: implement Run and Stop as async methods

            /*
             * TODO:
             * - state pattern does not fit because of arbitrary simulator concretions in application layer and
             *   therefore an unknown state machine in domain layer
             * - create a solution with fluent validation in some layer to define allowed alignments of simulation models or
             * - use some kind of Type enum to classify and check the order of the simulation models
             */
            foreach (ISimulationRunnable runner in _runnerRepo)
            {
                _currentRunner = runner;

                _currentRunner.RunSimulation();
            }
        }

        // -- properties

        public string CurrentModel { get { return _currentRunner.Name; } }

    }
}
