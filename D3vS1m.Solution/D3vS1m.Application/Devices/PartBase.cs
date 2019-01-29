using D3vS1m.Domain.System.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Devices
{
    public class PartBase
    {
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the enum PartTypes for the instance 
        /// </summary>
        public PartTypes Type { get; set; }
    }
}
