﻿using D3vS1m.Application.Communication;
using D3vS1m.Application.Scene;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.Events;
using D3vS1m.Domain.Runtime;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.System.Enumerations;
using Sin.Net.Domain.Logging;
using System;
using System.Diagnostics;
using System.Linq;

namespace D3vS1m.Application.Channel
{
    public class AdaptedFriisSimulator : SimulatorBase
    {
        // -- fields

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

        }

        // -- methods

        public override ISimulatable With(ArgumentsBase arguments)
        {
            if (ConvertArgs(arguments, ref _radioArgs)) return this;
            else if (ConvertArgs(arguments, ref _commArgs)) return this;
            else if (ConvertArgs(arguments, ref _sceneArgs)) return this;
            else return ArgsNotAdded(arguments.Name);
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
            Log.Trace($"{Name} calculated {_radioArgs.RadioBox.TotalData} values in {watch.ElapsedMilliseconds} ms");

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
            return c + 20 * (float)(Math.Log10(Math.Pow(r, a)));
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

        [Obsolete]
        public void SetupCommunication(ArgumentsBase args)
        {
            _commArgs = args as WirelessCommArgs;
        }

        // -- properties

        public override string Name { get { return _radioArgs.Name; } }

        public override SimulationModels Type { get { return SimulationModels.Channel; } }

        public override ArgumentsBase Arguments { get { return _radioArgs; } }


    }
}
