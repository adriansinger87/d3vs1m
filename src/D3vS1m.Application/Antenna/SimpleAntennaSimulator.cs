using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using System;

namespace D3vS1m.Application.Antenna
{
    [Serializable]
    public class SimpleAntennaSimulator : SimulatorBase
    {
        // -- fields

        private SimpleAntennaArgs _args;

        // -- constructor

        public SimpleAntennaSimulator(RuntimeBase runtime) : base(runtime)
        {

        }

        // -- methods

        public override ISimulatable With(ArgumentsBase arguments)
        {
            if (ConvertArgs(arguments, ref _args)) return this;
            else return ArgsNotAdded(arguments.Name);

        }

        public override void Run()
        {
            base.BeforeExecution();

            base.AfterExecution();
        }

        // -- properties

        public override string Key => Models.Antenna.Simple.Name;
        public override string Name => Models.Antenna.Simple.Name;
        public override SimulationTypes Type => SimulationTypes.Antenna;
        public override ArgumentsBase Arguments => _args;

    }
}
