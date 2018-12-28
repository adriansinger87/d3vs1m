using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Enumerations;
using D3vS1m.Domain.Events;
using D3vS1m.Domain.Simulation;

namespace D3vS1m.Application.Channel
{
    public class AdaptedFriisSimulator : SimulatorEventBase, ISimulatable
    {
        private AdaptedFriisArgs _args;

        public ISimulatable With(BaseArgs arguments)
        {
            _args = arguments as AdaptedFriisArgs;
            return this;
        }

        public void Execute()
        {
            BeforeExecution();

            // do your implementation here...

            _args.AttenuationExponent = 1;
            _args.AttenuationOffset = 2;

            AfterExecution();
        }

        protected override void BeforeExecution()
        {
            BeforeExecution(new SimulatorEventArgs(_args));
        }

        protected override void AfterExecution()
        {
            AfterExecution(new SimulatorEventArgs(_args));
        }

        // -- properties

        public string Name { get { return _args.Name; } }

        public SimulationModels Model { get { return SimulationModels.Channel; } }

        public BaseArgs Arguments { get { return _args; } }
    }
}
