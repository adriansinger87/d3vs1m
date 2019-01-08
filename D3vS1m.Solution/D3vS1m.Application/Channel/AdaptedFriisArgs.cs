using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.System.Constants;
using System.Collections.Generic;

namespace D3vS1m.Application.Channel
{
    public class AdaptedFriisArgs : ArgumentsBase
    {
        public AdaptedFriisArgs()
        {
            Name = Models.AdaptedFriisTransmission;

            // default settings
            RadioBox = new RadioCuboid();

            AttenuationExponent = Const.Channel.Radio.AttenuationExponent;
            AttenuationOffset = Domain.System.Constants.Const.Channel.Radio.AttenuationOffset;
        }

        public float AttenuationExponent { get; set; }
        public float AttenuationOffset { get; set; }
        public bool UseObstacles { get; set; }
        public RadioCuboid RadioBox { get; set; }
        public Vertex[] RxPositions { get; set; }
        public float[] RxValues { get; set; }
        public List<float[]> RxColors { get; set; }
    }
}
