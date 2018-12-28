﻿using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Enumerations;
using D3vS1m.Domain.Simulation;

namespace D3vS1m.Application.Antenna
{
    public class SimpleAntennaSimulator : ISimulatable
    {
        // -- fields

        SimpleAntennaArgs _args;

        // -- constructor

        public SimpleAntennaSimulator()
        {

        }

        // -- methods

        public ISimulatable With(BaseArgs arguments)
        {
            _args = arguments as SimpleAntennaArgs;

            return this;
        }

        public void Execute()
        {
           
        }

        // -- properties

        public string Name { get { return _args.Name; } }

        public SimulationModels Model { get { return SimulationModels.Antenna; } }

        public BaseArgs Arguments { get { return _args; } }
    }
}
