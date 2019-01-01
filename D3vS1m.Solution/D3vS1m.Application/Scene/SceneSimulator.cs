using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.Simulation;

namespace D3vS1m.Application.Scene
{
    public class SceneSimulator : SimulatorBase
    {
        private InvariantSceneArgs _sceneArgs;
        
        public override ISimulatable With(ArgumentsBase arguments)
        {
            _sceneArgs = arguments as InvariantSceneArgs;
            return this;
        }

        public override void Run()
        {
            base.BeforeExecution();

            base.AfterExecution();
        }


        public override string Name { get { return _sceneArgs.Name; } }

        public override SimulationModels Model { get { return SimulationModels.Scene; } }

        public override ArgumentsBase Arguments { get { return _sceneArgs; } }

    }
}
