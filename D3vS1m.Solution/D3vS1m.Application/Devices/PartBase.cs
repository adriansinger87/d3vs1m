using D3vS1m.Domain.System.Enumerations;
using System;

namespace D3vS1m.Application.Devices
{
    [Serializable]
    public class PartBase
    {
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the enum PartTypes for the instance 
        /// </summary>
        public PartTypes Type { get; set; }
    }
}
