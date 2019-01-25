using D3vS1m.Application.Devices;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Data.Scene;
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

        public void CalculateDistances()
        {
            Network.DistanceMatrix.Each((r, c, v) => {
                var pos1 = Network[r].Position;
                var pos2 = Network[c].Position;
                return Vertex.GetLength(pos1, pos2);
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
