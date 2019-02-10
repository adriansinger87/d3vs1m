using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.Runtime;

namespace D3vS1m.Application.Scene
{
    public class SceneSimulator : SimulatorBase
    {
        // -- fields
        
        private InvariantSceneArgs _sceneArgs;

        // -- constructors

        /// <summary>
        /// Baware: no runtime will be usable
        /// </summary>
        public SceneSimulator() : this(null)
        {
        }

        public SceneSimulator(RuntimeBase runtime) : base(runtime)
        {

        }

        // -- methods

        public override ISimulatable With(ArgumentsBase arguments)
        {
            if (ConvertArgs(arguments, ref _sceneArgs))     return this;
            else                                        return ArgsNotAdded(arguments.Name);
        }

        public override void Run()
        {
            base.BeforeExecution();

            base.AfterExecution();
        }

        // -- properties

        public override string Name { get { return _sceneArgs.Name; } }

        public override SimulationModels Type { get { return SimulationModels.Scene; } }

        public override ArgumentsBase Arguments { get { return _sceneArgs; } }

    }
}
