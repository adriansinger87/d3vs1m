using D3vS1m.Application.Network;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using D3vS1m.Application.Devices;

namespace D3vS1m.Application.Validation
{
    public class NetworkValidator : AbstractValidator<NetworkArgs>
    {
        public NetworkValidator()
        {
            SetupRules();
        }

        private void SetupRules()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(netArgs => netArgs.Network)
                .Must(n => ContainsDevices(n))
                .WithMessage("network instance must be present and should contain at least one device");

        }

        private bool ContainsDevices(PeerToPeerNetwork network)
        {
            if (network == null)        return false;
            if (network.Count == 0)     return false;

            return true;
        }
    }
}
