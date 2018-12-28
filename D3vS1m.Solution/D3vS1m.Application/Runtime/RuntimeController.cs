using D3vS1m.Application.Validation;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.System.Logging;
using FluentValidation.Results;
using System.Linq;

namespace D3vS1m.Application.Runtime
{
    public class RuntimeController : RuntimeBase
    {
        /// <summary>
        /// injected concretion of the validation of the simulation models
        /// </summary>
        private BasicValidator _validator;

        public RuntimeController(BasicValidator validator)
        {
            _validator = validator;
        }

        public override bool Validate()
        {
            // execute validation
            ValidationResult results = _validator.Validate(_simRepo);

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
