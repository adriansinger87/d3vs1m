using D3vS1m.Domain.Arguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Antenna
{
    class SimpleAntennaArgs : BaseArgs
    {
        public SimpleAntennaArgs()
        {
            base.Name = Models.SimpleAntenna;
        }

        /// <summary>
        /// Gets or sets the individual isotropic antenna gain for one device. 
        /// </summary>
        public float SimpleGain { get; set; }
    }
}
