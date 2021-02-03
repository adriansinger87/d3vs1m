using D3vS1m.Application.Devices;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Data.Scene;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TeleScope.Logging;
using TeleScope.Logging.Extensions;

namespace D3vS1m.Application.Network
{
    [Serializable]
    public class PeerToPeerNetwork : IEnumerable
    {

        // -- fields

        private readonly ILogger<PeerToPeerNetwork> _log;

        [NonSerialized]
        private NetworkValidator _validator;

        // -- properties

        public string Name { get; set; }

        public List<IDevice> Items { get; private set; }

        public int Count => Items.Count;

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

        // -- indexter

        public IDevice this[int index] => Items[index];

        // -- constructors

        public PeerToPeerNetwork()
        {
            _log = LoggingProvider.CreateLogger<PeerToPeerNetwork>();
            _validator = new NetworkValidator();

            Name = Models.Network.Name;
            Items = new List<IDevice>();
            AssociationMatrix = new NetworkMatrix<bool>();
            DistanceMatrix = new NetworkMatrix<float>();
            RssMatrix = new NetworkMatrix<float>();
            AngleMatrix = new NetworkMatrix<Angle>();
        }

        // -- methods

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public void SetupMatrices()
        {
            // execute validation & log
            ValidationResult results = _validator.Validate(this);
            LogValidationErrors(results);

            int size = Items.Count;

            AssociationMatrix.Init(size);
            DistanceMatrix.Init(size);
            RssMatrix.Init(size);
            AngleMatrix.Init(size, new Angle(float.NaN, float.NaN));
        }

        public float Availability()
		{
            var online = Items.Count(d => d.IsActive);
            var all = Items.Count;
            return (float)online / (float)all;
		}

        private void LogValidationErrors(ValidationResult results)
        {
            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    string property = (!string.IsNullOrEmpty(failure.PropertyName) ? $" property: {failure.PropertyName}" : "");
                    _log.Error($"failed validation: {failure.ErrorMessage}{property}");
                }
            }
            else
            {
                _log.Debug($"{Name} validated correctly");
            }
        }


	}
}
