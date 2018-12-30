using System;
using System.Linq;
using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.System.Enumerations;
using D3vS1m.Domain.Events;
using D3vS1m.Domain.Simulation;
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Application.Communication;

namespace D3vS1m.Application.Channel
{
    public class AdaptedFriisSimulator : SimulatorEventBase, ISimulatable
    {
        // -- fields

        private AdaptedFriisArgs _radioArgs;

        private WirelessCommArgs _commArgs;

        public AdaptedFriisSimulator()
        {

        }

        // -- methods

        public ISimulatable With(ArgumentsBase arguments)
        {
            _radioArgs = arguments as AdaptedFriisArgs;
            return this;
        }

        public void Execute()
        {
            BeforeExecution();

            // do your implementation here...
            FriisTransmission();

            AfterExecution();
        }

        private void FriisTransmission()
        {
            var tx = new Vector();
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
                .ForAll(d => _radioArgs.RxValues[d.Index] = GetAdaptedFriisAttenuation(c, Vector.GetLength(tx, d.Rx), a));
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

        public void SetupCommunication(ArgumentsBase args)
        {
            _commArgs = args as WirelessCommArgs;
        }

        // -- events

        protected override void BeforeExecution()
        {
            BeforeExecution(new SimulatorEventArgs(_radioArgs));
        }

        protected override void AfterExecution()
        {
            AfterExecution(new SimulatorEventArgs(_radioArgs));
        }

        // -- properties

        public string Name { get { return _radioArgs.Name; } }

        public SimulationModels Model { get { return SimulationModels.Channel; } }

        public ArgumentsBase Arguments { get { return _radioArgs; } }
    }
}
