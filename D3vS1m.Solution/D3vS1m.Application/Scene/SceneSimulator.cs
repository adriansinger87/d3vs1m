using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Enumerations;
using D3vS1m.Domain.Simulation;

namespace D3vS1m.Application.Scene
{
    public class SceneSimulator : ISimulatable
    {
        private InvariantSceneArgs _args;
        
        public ISimulatable With(BaseArgs arguments)
        {
            _args = arguments as InvariantSceneArgs;
            return this;
        }

        public void Execute()
        {

        }

        public string Name { get { return _args.Name; } }

        public SimulationModels Model { get { return SimulationModels.Scene; } }

        public BaseArgs Arguments { get { return _args; } }
    }
}
