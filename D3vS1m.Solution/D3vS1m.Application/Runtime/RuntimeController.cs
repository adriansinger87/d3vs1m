using D3vS1m.Application.Validation;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Logging;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace D3vS1m.Application.Runtime
{
    public class RuntimeController : RuntimeBase
    {
        private SimulationValidator validator;

        public RuntimeController()
        {
            validator = new SimulationValidator();
        }

        public override bool Validate()
        {
            // execute validation
            ValidationResult results = validator.Validate(_simRepo);

            // log
            results.RuleSetsExecuted.ToList().ForEach(s => Log.Trace(s));

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    string property = (!string.IsNullOrEmpty(failure.PropertyName) ? $" property: {failure.PropertyName}" : "");
                    Log.Error($"failed validation: {failure.ErrorMessage}{property}");
                }
            }

            // store and return total result
            _isValid = results.IsValid;
            return _isValid;
        }
    }
}
