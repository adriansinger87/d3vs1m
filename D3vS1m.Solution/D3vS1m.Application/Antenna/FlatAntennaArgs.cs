using D3vS1m.Domain.Arguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Antenna
{
    public class FlatAntennaArgs : BaseArgs
    {
        /// <summary>
        /// Gibt den für alle Geräte gültigen Antennengewinn aus oder legt diesen fest 
        /// </summary>
        public float FlatGain { get; set; }
    }
}
