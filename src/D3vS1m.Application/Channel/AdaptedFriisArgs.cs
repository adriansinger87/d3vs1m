using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.System.Constants;
using System;
using System.Collections.Generic;

namespace D3vS1m.Application.Channel
{
    [Serializable]
    public class AdaptedFriisArgs : ArgumentsBase
    {
        public AdaptedFriisArgs()
        {
            Reset();
        }

        // -- methods

        public override void Reset()
        {
            Name = Models.Channel.AdaptedFriis.Key;

            // default settings
            RadioBox = new RadioCuboid();

            AttenuationExponent = Const.Channel.Radio.AttenuationExponent;
            AttenuationOffset = Const.Channel.Radio.AttenuationOffset;
        }

        // -- properties

        public float AttenuationExponent { get; set; }
        public float AttenuationOffset { get; set; }
        public bool UseObstacles { get; set; }
        public RadioCuboid RadioBox { get; set; }
        public Vertex[] RxPositions { get; set; }
        public float[] RxValues { get; set; }
        public List<float[]> RxColors { get; set; }
    }
}
