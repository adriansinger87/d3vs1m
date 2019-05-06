using D3vS1m.Domain.Simulation;
using FluentValidation;
using System.Linq;
using D3vS1m.Domain.System.Enumerations;

namespace D3vS1m.Application.Validation
{
    public class BasicValidator : AbstractValidator<SimulatorRepository>
    {
        public BasicValidator()
        {
            SetupRules();
        }

        /// <summary>
        /// Implements all rules for the validation and gets called inside the constructor of the basic class 
        /// </summary>
        protected virtual void SetupRules()
        {
            RuleFor(repo => repo.Count).GreaterThan(0);
        }
    }
}
