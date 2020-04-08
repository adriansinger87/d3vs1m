using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using System.Collections.Generic;

namespace D3vS1m.Cli.Reader
{
    public interface IReadable
    {
        void Read(ArgumentsReader reader);
    }
}
