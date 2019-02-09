using D3vS1m.Application.Data;
using D3vS1m.Application.Network;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Constants;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.System.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Antenna
{
    public class SphericAntennaSimulator : SimulatorBase
    {
        SphericAntennaArgs _antennaArgs;
        NetworkArgs _netArgs; 

        public SphericAntennaSimulator()
        {
            Name = Models.SphericAntenna;
        }

        /// <summary>
        /// Adds a concrete argument object to the simulator
        /// </summary>
        /// <param name="arguments">Multiple argument objects can be added depending on the needs of the simulator</param>
        /// <returns>the calling instance</returns>
        public override ISimulatable With(ArgumentsBase arguments)
        {
            if (ConvertArgs(arguments, ref _antennaArgs))   return this;
            else if (ConvertArgs(arguments, ref _netArgs))  return this;
            else                                            return ArgsNotAdded(arguments.Name);
        }
        
        public override void Run()
        {
            base.BeforeExecution();

            // TODO iterate all network orientation informations and calculate resulting antenna gain
            // TODO remove test magic numbers
            float gain = CalculateGain(45,45);

            base.AfterExecution();
        }

        private float CalculateGain(float azimuthDegree, float elevationDegree)
        {
            Matrix<SphericGain> matrix = _antennaArgs.GainMatrix;
            int nAz = matrix.ColsCount;     // number of azimuth values (orizontal)
            int nEl = matrix.RowsCount;     // number of elevation values (vertical)

            float az_step = 360.0f / (float)nAz;
            float el_step = 180.0f / (float)(nEl - 1);

            float az_percent = Const.Func.Fract((azimuthDegree / az_step));
            float el_percent = Const.Func.Fract((elevationDegree / el_step));

            int row_0 = (int)Math.Floor((elevationDegree / el_step));
            int row_1 = row_0 + 1;

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
            var gain = new {
                x = matrix[row_0, col_0].Gain * (1 - el_percent) * (1 - az_percent),
                y = matrix[row_0, col_1].Gain * (1 - el_percent) * (az_percent),
                z = matrix[row_1, col_0].Gain * (el_percent) * (1 - az_percent),
                w = matrix[row_1, col_1].Gain * (el_percent) * (az_percent)
            };
            
            // return the sum of all gain fractions
            return (gain.x + gain.y + gain.z + gain.w);
        }

        public override ArgumentsBase Arguments { get { return _antennaArgs; } }
        public override string Name { get; }
        public override SimulationModels Model { get { return SimulationModels.Antenna; } }

    }
}
