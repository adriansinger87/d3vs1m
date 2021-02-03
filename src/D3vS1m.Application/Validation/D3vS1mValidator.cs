using D3vS1m.Application.Channel;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.Simulation;
using FluentValidation;
using System.Linq;

namespace D3vS1m.Application.Validation
{
    public class D3vS1mValidator : BasicValidator
    {
        public D3vS1mValidator() : base()
        {
        }

        protected override void SetupRules()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            base.SetupRules();

            // channel
            RuleFor(repo => repo.Items)
                .Must(list => list.Any(i => i.Type == SimulationTypes.Channel))
                .WithMessage("channel model not present");

            RuleFor(repo => repo.Items.FirstOrDefault(i => i.Type == SimulationTypes.Channel))
                .Must(i => i.Arguments != null).WithMessage("channel model has no arguments")
                .Must(i => i.Arguments is AdaptedFriisArgs).WithMessage("scene model has wrong argument type")
                .Must(i => ValidateChannelArguments(i)).WithMessage("scene model arguments not valid");

            // network
            RuleFor(repo => repo.Items)
                .Must(list => list.Any(i => i.Type == SimulationTypes.Network))
                .WithMessage("network model not present");

            // scene
            RuleFor(repo => repo.Items)
                .Must(list => list.Any(i => i.Type == SimulationTypes.Scene))
                .WithMessage("scene model not present");

            // commuication
            RuleFor(repo => repo.Items)
                .Must(list => list.Any(i => i.Type == SimulationTypes.Communication))
                .WithMessage("communication model not present");
        }

        private bool ValidateChannelArguments(ISimulatable sim)
        {
            AdaptedFriisArgs args = sim.Arguments as AdaptedFriisArgs;

            if (args.AttenuationExponent <= 1)                  return false;
            if (args.AttenuationOffset < 0)                     return false;

            // all okay
            return true;
        }
    }
}
