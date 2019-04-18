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

            RuleFor(repo => repo.Items.Cast<ISimulatable>())
                .Must(list => list.Any(i => i.Type == SimulationModels.Energy))
                .WithMessage("energy model not present");
        }
    }
}
