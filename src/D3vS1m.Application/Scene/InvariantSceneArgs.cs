using D3vS1m.Domain.Data.Arguments;
using System;

namespace D3vS1m.Application.Scene
{
    [Serializable]
    public class InvariantSceneArgs : ArgumentsBase
    {
        public InvariantSceneArgs()
        {
            Reset();
        }

        public override void Reset()
        {
            Name = Models.Scene.Key;
        }

        public object[] Obstacles { get; }

        /// <summary>
        /// Gets or sets the flag to render the data of the radio map or not
        /// TODO: This is a presentation-layer functionality --> move it at sometime to the presentation layer
        /// </summary>
        public bool RenderRadioMap { get; set; }

    }
}
