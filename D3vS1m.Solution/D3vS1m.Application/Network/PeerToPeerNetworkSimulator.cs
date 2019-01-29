using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.Simulation;
using System;
using System.Collections.Generic;
using System.Text;
using D3vS1m.Domain.Events;
using D3vS1m.Application.Validation;
using FluentValidation.Results;
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.System.Constants;
using D3vS1m.Domain.System.Logging;

namespace D3vS1m.Application.Network
{
    public class PeerToPeerNetworkSimulator : SimulatorBase
    {
        private NetworkArgs _netArgs;
        private PeerToPeerNetwork _net;



        // -- constructor

        public PeerToPeerNetworkSimulator()
        {

        }

        // -- methods

        public override ISimulatable With(ArgumentsBase arguments)
        {
            if (ConvertArgs(arguments, ref _netArgs))
            {
                // setup private reference of the network
                _net = _netArgs.Network;
                _netArgs.NetworkOutdated = true;
                return this;
            }
            else
            {
                return ArgsNotAdded(arguments.Name);
            }
        }

        public override void Run()
        {
            if (!_netArgs.NetworkOutdated)  return;

            base.BeforeExecution();

            CalculateDistances();
            CalculateOrientations();
            _netArgs.NetworkOutdated = false;   // network is now up-to-date

            base.AfterExecution();
        }

        // -- private method

        /// <summary>
        /// Calculates all distances between each device.
        /// </summary>
        private void CalculateDistances()
        {
            _net.DistanceMatrix.Each((r, c, v) => {
                var pos1 = _net[r].Position;
                var pos2 = _net[c].Position;
                return Vertex.GetLength(pos1, pos2);
            });
        }

        /// <summary>
        /// Calculates all angles (azimuth, elevation) between each device.
        /// </summary>
        private void CalculateOrientations()
        {
            _net.AngleMatrix.Each((r, c, v) => {

                var txDev = _net[r];  // Tx - transmitter
                var rxDev = _net[c];  // Rx - receiver

                Vertex e_EL = new Vertex(0, 1, 0);    // Einheitsvektor Elevation
                Vertex tx_EL = new Vertex(0, 1, 0);   // Sender Elevation

                Vertex e_AZ = new Vertex(0, 0, 1);    // Einheitsvektor Azimut
                Vertex tx_AZ = new Vertex(0, 0, 1);   // Sender Azimut

                Vertex line = rxDev.Position - txDev.Position;

                /*
                 * ELEVATION
                 * Einheitsvektor 'e_ELEVATION' (0°) ist (0, 1, 0) -> OpenGL (+y)-Richtung
                 * Winkelberechnung zwischen den Vektoren wie sie gerendert werden
                 */
                tx_EL = Vertex.RotateRadX(Const.Func.ToRadian(txDev.Orientation.Elevation), e_EL);
                tx_EL = Vertex.RotateRadY(Const.Func.ToRadian(txDev.Orientation.Azimuth), tx_EL);
                float elevation = Const.Func.ToDegree(Vertex.ACosRad(line, tx_EL));

                /*
                 * AZIMUT
                 * Normale 'n' auf Ebene zwischen rxPos, txPos und Elevationsstab
                 * ACHTUNG der Elevationsstab vom Sender muss ausgehend von der Sendeposition gemessen werden, daher die Addition
                 */
                tx_AZ = Vertex.RotateRadX(Const.Func.ToRadian(txDev.Orientation.Elevation), e_AZ);       // erst rotatiom um Elevation-Winkel
                tx_AZ = Vertex.RotateRadY(Const.Func.ToRadian(txDev.Orientation.Azimuth), tx_AZ);        // dann Azimut-Winkel der Antenne
                Vertex n = Vertex.Normalize(rxDev.Position, txDev.Position, (txDev.Position + tx_EL));
                float azimuth = Const.Func.ToDegree(Vertex.ASinRad(n, tx_AZ));

                // Azimut-Korrekturen
                if (float.IsNaN(azimuth))   // -> is not-a-number ?
                {
                    azimuth = 0;
                }
                else if (azimuth < 0)
                {
                    if (Vertex.Scalar(line, tx_AZ) < 0)       // zeigen die Vektoren in die gleiche Richtung oder nicht?
                    {
                        azimuth = (180 - azimuth);
                    }
                    else
                    {
                        azimuth = (360 + azimuth);
                    }
                }
                else if (Vertex.Scalar(line, tx_AZ) < 0)
                {
                    azimuth = (180 - azimuth);
                }

                return new Angle(azimuth, elevation);
            });
        }


        // -- properties       

        public override ArgumentsBase Arguments { get { return _netArgs; } }

        public override string Name { get { return _netArgs.Name; } }

        public override SimulationModels Model { get { return SimulationModels.Network; } }

       

    }
}
