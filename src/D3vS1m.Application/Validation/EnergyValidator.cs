using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D3vS1m.Application.Validation
{
    public class EnergyValidator : BasicValidator
    {
        public EnergyValidator()
        {
        }

        protected override void SetupRules()
        {
            base.SetupRules();

            RuleFor(repo => repo.Items)
                .Must(list => list.Any(i => i.Type == SimulationTypes.Energy))
                .WithMessage("energy model not present");
        }
    }
}
