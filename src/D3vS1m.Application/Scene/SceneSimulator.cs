using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.Runtime;
using System;

namespace D3vS1m.Application.Scene
{
    [Serializable]
    public class SceneSimulator : SimulatorBase
    {
        // -- fields
        
        private InvariantSceneArgs _sceneArgs;

        // -- constructors

        public SceneSimulator(RuntimeBase runtime) : base(runtime)
        {

        }

        // -- methods

        public override ISimulatable With(ArgumentsBase arguments)
        {
            if (ConvertArgs(arguments, ref _sceneArgs))     return this;
            else                                            return ArgsNotAdded(arguments.Name);
        }

        public override void Run()
        {
            base.BeforeExecution();

            base.AfterExecution();
        }

        // -- properties

        public override string Id => Models.Scene.Name;
        public override string Name => Models.Scene.Name;
        public override SimulationTypes Type => SimulationTypes.Scene;
        public override ArgumentsBase Arguments => _sceneArgs;

    }
}
