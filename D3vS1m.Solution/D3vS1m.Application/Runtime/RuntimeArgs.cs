using D3vS1m.Domain.Data.Arguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Runtime
{
    public class RuntimeArgs : ArgumentsBase
    {
        public RuntimeArgs()
        {
            Name = Models.Runtime;        
        }

        public DateTime StartTime { get; set; }
    }
}
