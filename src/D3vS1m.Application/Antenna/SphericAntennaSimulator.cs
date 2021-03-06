﻿using D3vS1m.Application.Data;
using D3vS1m.Application.Network;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Constants;
using D3vS1m.Domain.System.Enumerations;
using Microsoft.Extensions.Logging;
using System;
using TeleScope.Logging;
using TeleScope.Logging.Extensions;

namespace D3vS1m.Application.Antenna
{
    [Serializable]
    public class SphericAntennaSimulator : SimulatorBase
    {

        // -- fields
        private readonly ILogger<SphericAntennaSimulator> _log;
        private SphericAntennaArgs _antennaArgs;
        private NetworkArgs _netArgs;

        // -- properties

        public override string Key => Models.Antenna.Spheric.Name;
        public override string Name => Models.Antenna.Spheric.Name;
        public override ArgumentsBase Arguments => _antennaArgs;
        public override SimulationTypes Type => SimulationTypes.Antenna;

        /// <summary>
        /// Baware: no runtime will be usable
        /// </summary>
        public SphericAntennaSimulator() : this(null)
        {
        }

        public SphericAntennaSimulator(RuntimeBase runtime) : base(runtime)
        {
            _log = LoggingProvider.CreateLogger<SphericAntennaSimulator>();
        }

        /// <summary>
        /// Adds a concrete argument object to the simulator
        /// </summary>
        /// <param name="arguments">Multiple argument objects can be added depending on the needs of the simulator</param>
        /// <returns>the calling instance</returns>
        public override ISimulatable With(ArgumentsBase arguments)
        {
            if (ConvertArgs(arguments, ref _antennaArgs))
            {
                return this;
            }
            else if (ConvertArgs(arguments, ref _netArgs))
            {
                return this;
            }
            else
            {
                return ArgsNotAdded(arguments.Name);
            }
        }

        public override void Run()
        {
            base.BeforeExecution();

            if (_antennaArgs.GainMatrix == null ||
                _netArgs == null ||
                _netArgs.Network == null)
            {
                _log.Warn($"{Arguments.Name} has not all needed arguments so the run method is canceled.");
                base.AfterExecution();
                return;
            }

            _netArgs.Network.AngleMatrix.Each((r, c, angle) =>
            {
                float gain = _netArgs.Network.RssMatrix[r, c];
                gain += CalculateAntennaGain(angle.Azimuth, angle.Elevation);
                _netArgs.Network.RssMatrix.Set(r, c, gain);
                return angle;
            });

            base.AfterExecution();
        }

        private float CalculateAntennaGain(float azimuthDegree, float elevationDegree)
        {
            Matrix<SphericGain> matrix = _antennaArgs.GainMatrix;
            int nAz = matrix.ColsCount;     // number of azimuth values (orizontal)
            int nEl = matrix.RowsCount;     // number of elevation values (vertical)

            float az_step = 360.0f / (float)nAz;
            float el_step = 180.0f / (float)(nEl - 1);

            float az_percent = Const.Math.Fract((azimuthDegree / az_step));
            float el_percent = Const.Math.Fract((elevationDegree / el_step));

            int row_0 = (int)Math.Floor((elevationDegree / el_step));
            int row_1 = row_0 < nEl - 1 ? row_0 + 1 : row_0 - 1;

            int col_0 = (int)Math.Floor((azimuthDegree / az_step));
            int col_1 = col_0 + 1;

            /*
             *  Float4 (x, y, z, w) collects the following surrounding matrix values of the precise direction 
             *  +---------------+-------------
             *	|	x [0,0]		|	y [0,1]	
             *	|	z [1,0]		|	w [1,1]
             *	
             *	each gain gets multipied with the percentage influence
             */
            var gain = new
            {
                x = matrix[row_0, col_0].Gain * (1 - el_percent) * (1 - az_percent),
                y = matrix[row_0, col_1].Gain * (1 - el_percent) * (az_percent),
                z = matrix[row_1, col_0].Gain * (el_percent) * (1 - az_percent),
                w = matrix[row_1, col_1].Gain * (el_percent) * (az_percent)
            };

            // return the sum of all gain fractions
            return (gain.x + gain.y + gain.z + gain.w);
        }


    }
}
