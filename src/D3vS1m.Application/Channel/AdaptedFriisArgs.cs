using D3vS1m.Domain.Data.Arguments;
using D3vS1m.Domain.Data.Scene;
using D3vS1m.Domain.System.Constants;
using System;
using System.Collections.Generic;

namespace D3vS1m.Application.Channel
{
    [Serializable]
    public class AdaptedFriisArgs : ArgumentsBase, IAdaptedFriisArgsDecorable
    {
        public AdaptedFriisArgs() : base()
        {
            Key = Models.Channel.AdaptedFriis.Key;
            Name = Models.Channel.AdaptedFriis.Name;

            Reset();
        }

        // -- methods

        public override void Reset()
        {
            // default settings
            RadioBox = new RadioCuboid();
            AttenuationExponent = Const.Channel.Radio.AttenuationExponent;
            AttenuationOffset = Const.Channel.Radio.AttenuationOffset;
        }

        public virtual void UpdatePositions()
        {
            RxPositions = RadioBox.CreateRxPositions();
        }

        // -- properties

        public virtual float AttenuationExponent { get; set; }
        public virtual float AttenuationOffset { get; set; }
        public virtual bool UseObstacles { get; set; }
        public RadioCuboid RadioBox { get; set; }


        public virtual Vertex[] RxPositions { get; set; }
        
        public virtual float[] RxValues { get; set; }
        
        public virtual List<float[]> RxColors { get; set; }

    }
}
