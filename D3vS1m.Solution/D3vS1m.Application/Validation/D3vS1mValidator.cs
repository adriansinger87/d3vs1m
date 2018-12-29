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

            RuleFor(repo => repo.Cast<ISimulatable>())
                .Must(list => list.Any(i => i.Model == SimulationModels.Scene))
                .WithMessage("scene model not present");

            RuleFor(repo => repo.Cast<ISimulatable>().FirstOrDefault(i => i.Model == SimulationModels.Channel))
                .Must(i => i.Arguments != null).WithMessage("scene model has no arguments")
                .Must(i => i.Arguments is AdaptedFriisArgs).WithMessage("scene model has wrong argument type")
                .Must(i => ValidateChannelArguments(i)).WithMessage("scene model arguments not valid");
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
