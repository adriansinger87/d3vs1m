using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Data.Scene;
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

            // TODO: refactore magic numbers
            AttenuationExponent = 1.25F;
            AttenuationOffset = 0.5F;
        }

        public float AttenuationExponent { get; set; }
        public float AttenuationOffset { get; set; }
        public bool UseObstacles { get; set; }
        public RadioCuboid RadioBox { get; set; }
        public Vector[] RxPositions { get; set; }
        public float[] RxValues { get; set; }
        public List<float[]> RxColors { get; set; }
    }
}
