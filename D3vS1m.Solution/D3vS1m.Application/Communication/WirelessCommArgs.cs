using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Communication
{
    public class WirelessCommArgs : ArgumentsBase
    {
        public WirelessCommArgs()
        {
            Name = Models.WirelessCommunication;

            TxFrequencyMHz = 2405;
            TxPowerDBm = 1;
        }

        // --- properties

        public float TxFrequencyMHz { get; set; }
        public float TxWavelength { get { return Radio.FreqToMeter(this.TxFrequencyMHz); } }
        public float TxPowerDBm { get; set; }
    }
}
