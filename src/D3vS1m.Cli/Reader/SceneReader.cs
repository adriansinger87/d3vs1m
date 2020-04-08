using D3vS1m.Application;
using D3vS1m.Domain.System.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Cli.Reader
{
    public class SceneReader : IReadable
    {
        public void Read(ArgumentsReader reader)
        {
            var args = reader.Factory.NewArgument(Models.Scene.Name);
            reader.Arguments.Add(SimulationTypes.Scene, args);
        }
    }
}
