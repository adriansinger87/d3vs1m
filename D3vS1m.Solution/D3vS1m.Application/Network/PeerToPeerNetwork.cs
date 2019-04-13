using D3vS1m.Application.Devices;
using D3vS1m.Application.Validation;
using D3vS1m.Domain.Data.Scene;
using FluentValidation.Results;
using Sin.Net.Domain.Logging;
using Sin.Net.Domain.Repository;

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
            AngleMatrix.Init(size, new Angle(float.NaN, float.NaN));
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

        /// <summary>
        /// index-based intexer to be able to get the instance at the index i
        /// </summary>
        /// <param name="index">The index of the item in the list</param>
        /// <returns>The instance of T with the corresponding index</returns>
        public BasicDevice this[int index] => Items[index];
    }
}
