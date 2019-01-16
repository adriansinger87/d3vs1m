using D3vS1m.Application.Data;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.System.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Antenna
{
    public class SphericAntennaSimulator : SimulatorBase
    {
        SphericAntennaArgs _antennaArgs;

        public SphericAntennaSimulator()
        {
            Name = Models.SphericAntenna;
            GainMatrix = new Matrix<SphericGain>();

        }


        public override ISimulatable With(ArgumentsBase arguments)
        {
            if (ConvertArgs(arguments, ref _antennaArgs))  return this;
            else                                           return ArgsNotAdded(arguments.Name);
        }


        public override void Run()
        {
            // TODO: implement sheric gain calculation here...
        }

        public Matrix<SphericGain> GainMatrix { get; set; }

        public override ArgumentsBase Arguments { get { return _antennaArgs; } }
        public override string Name { get; }
        public override SimulationModels Model => throw new NotImplementedException();
    }
}
