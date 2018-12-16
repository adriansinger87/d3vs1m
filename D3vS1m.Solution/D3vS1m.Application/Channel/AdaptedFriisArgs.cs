using D3vS1m.Domain.Data.Arguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Channel
{
    public class AdaptedFriisArgs : BaseArgs
    {
        public AdaptedFriisArgs()
        {
            Name = Models.AdaptedFriisTransmission;
        }

        public float AttenuationExponent { get; set; }
        public float AttenuationOffset { get; set; }
        public bool UseObstacles { get; set; }
        public object RadioBox { get; set; }
        public List<float[]> RxPositions { get; set; }
        public float[] RxValues { get; set; }
        public List<float[]> RxColors { get; set; }
    }
}
