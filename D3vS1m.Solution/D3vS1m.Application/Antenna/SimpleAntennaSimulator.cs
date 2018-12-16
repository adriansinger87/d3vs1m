using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Simulation;
using System;
using System.Collections.Generic;
using System.Text;

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
        
    }
}
