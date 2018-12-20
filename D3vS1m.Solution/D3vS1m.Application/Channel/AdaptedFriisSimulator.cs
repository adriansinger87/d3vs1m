using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Enumerations;
using D3vS1m.Domain.Simulation;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Channel
{
    public class AdaptedFriisSimulator : ISimulatable
    {
        private AdaptedFriisArgs _args;



        public ISimulatable With(BaseArgs arguments)
        {
            _args = arguments as AdaptedFriisArgs;
            return this;
        }

        public void Execute()
        {

        }

        // -- properties

        public string Name { get { return _args.Name; } }

        public SimulationModels Model { get { return SimulationModels.Channel; } }
    }
}
