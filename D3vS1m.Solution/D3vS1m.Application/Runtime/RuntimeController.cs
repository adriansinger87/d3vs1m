using D3vS1m.Application.Validation;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.System.Logging;
using FluentValidation.Results;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace D3vS1m.Application.Runtime
{
    public class RuntimeController : RuntimeBase
    {
        // -- fields

        /// <summary>
        /// injected concretion of the validation of the simulation models
        /// </summary>
        private BasicValidator _validator;

        /// <summary>
        /// private concretion of the arguments for this specific runtime
        /// </summary>
        private RuntimeArgs _args;

        // -- constructor

        public RuntimeController(BasicValidator validator)
        {
            _validator = validator;
            _args = new RuntimeArgs();
        }

        // -- methods

        public override bool Validate()
        {
            // execute validation
            ValidationResult results = _validator.Validate(_simRepo);

            // log
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

        public override Task RunAsync(Func<RuntimeBase, bool> condition)
        {
            _args.StartTime = DateTime.Now;
            return base.RunAsync(condition);
        }

        public override string ToString()
        {
            return _args.Name;
        }

        // -- properties

        public override ArgumentsBase Arguments { get { return _args; } }
    }
}
