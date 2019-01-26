using D3vS1m.Application.Devices;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.System.Constants;
using D3vS1m.Domain.System.Logging;
using FluentValidation.Results;
using System;
using System.Collections.Generic;

using System.Text;

namespace D3vS1m.Application.Network
{
    public class NetworkArgs : ArgumentsBase
    {
        // -- fields

        NetworkValidator _validator;

        // -- constructor

        public NetworkArgs()
        {
            Name = Models.PeerToPeerNetwork;
            Network = new PeerToPeerNetwork();

            _validator = new NetworkValidator();
        }

        // -- methods

        public void AddDevices(BasicDevice[] devices)
        {
            Network.AddRange(devices);
        }

        public void SetupMatrices()
        {
            // execute validation & log
            ValidationResult results = _validator.Validate(this);
            LogValidationErrors(results);

            int size = Network.Count;

            Network.AssociationMatrix.Init(size);
            Network.DistanceMatrix.Init(size);
            Network.RssMatrix.Init(size);
            Network.AngleMatrix.Init(size);
        }

        /// <summary>
        /// Calculates all distances between each device.
        /// </summary>
        public void CalculateDistances()
        {
            Network.DistanceMatrix.Each((r, c, v) => {
                var pos1 = Network[r].Position;
                var pos2 = Network[c].Position;
                return Vertex.GetLength(pos1, pos2);
            });
        }

        /// <summary>
        /// Calculates all angles (azimuth, elevation) between each device.
        /// </summary>
        public void CalculateAngles()
        {
            Network.AngleMatrix.Each((r, c, v) => {

                var txDev = Network[r];  // Tx - transmitter
                var rxDev = Network[c];  // Rx - receiver

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

        private void LogValidationErrors(ValidationResult results)
        {
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    string property = (!string.IsNullOrEmpty(failure.PropertyName) ? $" property: {failure.PropertyName}" : "");
                    Log.Error($"failed validation: {failure.ErrorMessage}{property}");
                }
            }
            else
            {
                Log.Debug($"{Network.Name} validated correctly");
            }
        }

        // -- properties

        public PeerToPeerNetwork Network { get; set; }
    }
}
