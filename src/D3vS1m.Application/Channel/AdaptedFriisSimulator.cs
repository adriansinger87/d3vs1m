using D3vS1m.Application.Communication;
using D3vS1m.Application.Scene;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.Events;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.System.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using TeleScope.Logging;
using TeleScope.Logging.Extensions;

namespace D3vS1m.Application.Channel
{
    [Serializable]
    public class AdaptedFriisSimulator : SimulatorBase
    {
        // -- fields

        private readonly ILogger<SimulatorBase> _log;
        private AdaptedFriisArgs _radioArgs;
        private WirelessCommArgs _commArgs;
        private InvariantSceneArgs _sceneArgs;

        // -- constructors

        /// <summary>
        /// Baware: no runtime will be usable
        /// </summary>
        public AdaptedFriisSimulator() : this(null)
        {

        }

        public AdaptedFriisSimulator(RuntimeBase runtime) : base(runtime)
        {
            _log = LoggingProvider.CreateLogger<SimulatorBase>();
        }

        // -- methods

        public override ISimulatable With(ArgumentsBase arguments)
        {
            // other args are loaded on runtime
            if (ConvertArgs(arguments, ref _radioArgs)) return this;
            else if (ConvertArgs(arguments, ref _commArgs)) return this;
            else if (ConvertArgs(arguments, ref _sceneArgs)) return this;
            else return ArgsNotAdded(arguments.Name);
        }

		protected override void BeforeExecution()
        {
            base.BeforeExecution();

            // do own stuff here...
        }


		protected override void AfterExecution()
        {
            base.AfterExecution();

        }

        public override void Run()
        {
            BeforeExecution();
            FriisTransmission();
            AfterExecution();
        }

        private void FriisTransmission()
        {
            var watch = new Stopwatch();
            watch.Start();
            var tx = new Vertex();
            float a = _radioArgs.AttenuationExponent;
            float c = GetFriisConstant(_commArgs.TxWavelength);
            _radioArgs.RxValues = new float[_radioArgs.RadioBox.TotalData];

            /*
             * - parallelize the loop
             * - make a wrapper object to get the Index and the rx position vector 'Rx'
             * - calculate the adapted friis transmission and save it in the corresponding RxValues[d.Index]
             */
            _radioArgs.RxPositions
                .AsParallel()
                .Select((rx, i) => new { Index = i, Rx = rx })
                .ForAll(d => _radioArgs.RxValues[d.Index] = GetAdaptedFriisAttenuation(c, Vertex.GetLength(tx, d.Rx), a));

            // log the brutto duration
            watch.Stop();
            _log.Trace($"{Name} calculated {_radioArgs.RadioBox.TotalData} values in {watch.ElapsedMilliseconds} ms");

        }

        /// <summary>
        /// Adapted friis transmission
        /// </summary>
        /// <param name="c">friis constant</param>
        /// <param name="r">radius or distance between transmitter position (tx) and receiver position (rx)</param>
        /// <param name="a">attenuation exponent</param>
        /// <returns></returns>
        private float GetAdaptedFriisAttenuation(float c, float r, float a)
        {
            // adapted friis transmission = c + 20 * log( r ^ a)
            var result = c + 20 * (float)(Math.Log10(Math.Pow(r, a)));
            return result;
        }

        /// <summary>
        /// precalculates the constant part of the adapted friis transmission
        /// to have this calculation only once in a simulation run 
        /// </summary>
        /// <param name="wavelength"></param>
        /// <returns></returns>
        public float GetFriisConstant(float wavelength)
        {
            return 20 * (float)Math.Log10((4 * Math.PI / wavelength));
        }

        // -- event moethods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected sealed override void OnStarted(object sender, SimulatorEventArgs e)
        {
            ConvertArgs(
                _runtime?.Simulators.AllArguments().GetByName(Models.Communication.LrWpan.Name),
                ref _commArgs);

            ConvertArgs(
                _runtime?.Simulators.AllArguments().GetByName(Models.Scene.Name),
                ref _sceneArgs);

            // override the rx positions field
            _radioArgs.UpdatePositions();
        }

        // -- properties

        public override string Key => Models.Channel.AdaptedFriis.Key;
        public override string Name => Models.Channel.AdaptedFriis.Name;
        public override SimulationTypes Type => SimulationTypes.Channel;
        public override ArgumentsBase Arguments => _radioArgs;
    }
}
