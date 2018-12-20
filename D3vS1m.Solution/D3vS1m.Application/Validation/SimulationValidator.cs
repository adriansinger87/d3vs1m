using System;
using System.Collections.Generic;
using System.Text;
using D3vS1m.Domain;
using D3vS1m.Domain.Simulation;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;
using D3vS1m.Domain.Enumerations;

namespace D3vS1m.Application.Validation
{
    public class SimulationValidator : AbstractValidator<SimulatorRepository>
    {
        public SimulationValidator()
        {
            RuleFor(repo => repo.Count).GreaterThan(0);

            RuleFor(repo => repo.Cast<ISimulatable>())
                .Must(list => list.Any(i => i.Model == SimulationModels.Channel))
                .WithMessage("network model not present");

            
        }
    }
}
