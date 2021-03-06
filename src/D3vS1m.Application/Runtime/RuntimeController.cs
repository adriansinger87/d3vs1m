﻿using D3vS1m.Application.Validation;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Events;
using D3vS1m.Domain.Runtime;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TeleScope.Logging;
using TeleScope.Logging.Extensions;

namespace D3vS1m.Application.Runtime
{
	/// <summary>
	/// Implements the abstract class RuntimeBase and adds some behavior for validation and conrete runtime arguments
	/// </summary>
	public class RuntimeController : RuntimeBase
	{
		// -- fields

		private readonly ILogger<RuntimeController> _log;

		/// <summary>
		/// injected concretion of the validation of the simulation models
		/// </summary>
		private readonly BasicValidator _validator;

		/// <summary>
		/// private concretion of the arguments for this specific runtime
		/// </summary>
		private RuntimeArgs _args;

		private Stopwatch _watch;

		// -- constructor

		/// <summary>
		/// The constructor gets the concrete validator injected and instanciates the concrete arguments of RuntimeArgs type
		/// </summary>
		/// <param name="validator">The validator concretion could be of type BasicValidator or a derived class</param>
		public RuntimeController(BasicValidator validator) : base()
		{
			_log = LoggingProvider.CreateLogger<RuntimeController>();
			_validator = validator;
			_args = new RuntimeArgs();

			base.IterationPassed += OnIterationPassed;
			base.Started += OnStarted;
			base.Stopped += OnStopped;
		}

		// -- methods

		/// <summary>
		/// The arguments must be of type RuntimeArgs.
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		public override RuntimeBase With(ArgumentsBase args)
		{
			_args = args as RuntimeArgs;
			return this;
		}

		/// <summary>
		/// Validate the simulation models and their arguments
		/// </summary>
		/// <returns>Returns 'true' if there are no errors otherwise 'false'.</returns>
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
					_log.Error($"failed validation: {failure.ErrorMessage}{property}");
				}
			}
			else
			{
				_log.Info("Validation passed");
			}

			// store and return total result
			_isValid = results.IsValid;
			return _isValid;
		}

		/// <summary>
		/// Start the iteration of the run method of all registered simulation models
		/// as long as the condition method returns true
		/// The overide adds the start time to the concrete RuntimeArgs instance.
		/// </summary>
		/// <param name="condition">A method that determines the condition to continue or to end the simulation</param>
		/// <returns>The task object representing the async task</returns>
		public override Task RunAsync(Func<RuntimeBase, bool> condition)
		{
			_args.ResetTime();
			return base.RunAsync(condition);
		}

		// -- event methods

		private void OnStarted(object sender, SimulatorEventArgs e)
		{
			_watch = new Stopwatch();
			_watch.Start();
		}

		private void OnIterationPassed(object sender, SimulatorEventArgs e)
		{
			_args.Iterations++;
			_args.SimulatedTime = _args.SimulatedTime.Add(_args.CycleDuration);
			_log.Trace($"{_args.Iterations} iterations at simulated duration: {_args.SimulatedTime}");
		}

		private void OnStopped(object sender, SimulatorEventArgs e)
		{
			_watch.Stop();
			_args.ElapsedTime = _watch.Elapsed;
			_log.Trace($"Duration of simulation: {_args.ElapsedTime}.");
		}

		// -- properties

		/// <summary>
		/// Gets the concrete arguments of type RuntimeArgs  
		/// </summary>
		public override ArgumentsBase Arguments { get { return _args; } }
	}
}
