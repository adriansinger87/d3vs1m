﻿using D3vS1m.Application.Devices;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Data.Repositories;
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.System.Logging;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Network
{
    public class PeerToPeerNetwork : RepositoryBase<BasicDevice>
    {

        private NetworkValidator _validator;

        public PeerToPeerNetwork()
        {
            _validator = new NetworkValidator();

            Name = Models.PeerToPeerNetwork;

            AssociationMatrix = new NetworkMatrix<bool>();
            DistanceMatrix = new NetworkMatrix<float>();
            RssMatrix = new NetworkMatrix<float>();
            AngleMatrix = new NetworkMatrix<Angle>();
        }

        public void SetupMatrices()
        {
            // execute validation & log
            ValidationResult results = _validator.Validate(this);
            LogValidationErrors(results);

            int size = this.Count;

            AssociationMatrix.Init(size);
            DistanceMatrix.Init(size);
            RssMatrix.Init(size);
            AngleMatrix.Init(size);
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
                Log.Debug($"{Name} validated correctly");
            }
        }

        // -- properties

        /// <summary>
        /// Gets or sets the communication associations between the devices.
        /// </summary>
        public NetworkMatrix<bool> AssociationMatrix { get; set; }

        /// <summary>
        /// Gets or sets the distances between the devices.
        /// </summary>
        public NetworkMatrix<float> DistanceMatrix { get; set; }

        /// <summary>
        /// Gets or sets the received signal strength (RSS) between the devices.
        /// </summary>
        public NetworkMatrix<float> RssMatrix { get; set; }

        /// <summary>
        /// Gets or sets the angles between the devices.
        /// </summary>
        public NetworkMatrix<Angle> AngleMatrix { get; set; }
    }
}
