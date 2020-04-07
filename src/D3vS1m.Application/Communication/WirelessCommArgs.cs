﻿using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Constants;
using System;

namespace D3vS1m.Application.Communication
{
    [Serializable]
    public class WirelessCommArgs : ArgumentsBase
    {
        /// <summary>
        /// Default constructor that creates a default instance.
        /// </summary>
        public WirelessCommArgs() : base()
        {
            Reset();
        }

        public override void Reset()
        {
            Name = Models.Communication.LrWpan.Name;
            TxFrequencyMHz = 2405;
            TxPowerDBm = 1;
        }

        // --- properties

        public float TxFrequencyMHz { get; set; }
        public float TxWavelength { get { return Const.Channel.Radio.FreqToMeter(this.TxFrequencyMHz); } }
        public float TxPowerDBm { get; set; }


    }
}
