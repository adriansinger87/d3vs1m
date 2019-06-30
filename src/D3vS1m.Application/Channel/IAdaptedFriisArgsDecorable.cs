using System.Collections.Generic;
using D3vS1m.Domain.Data.Scene;

namespace D3vS1m.Application.Channel
{
    public interface IAdaptedFriisArgsDecorable
    {
        // -- methods

        void Reset();
        void UpdatePositions();

        // -- properties

        float AttenuationExponent { get; set; }
        float AttenuationOffset { get; set; }
        string Guid { get; set; }
        string Name { get; set; }
        RadioCuboid RadioBox { get; set; }
        List<float[]> RxColors { get; set; }
        Vertex[] RxPositions { get; set; }
        float[] RxValues { get; set; }
        bool UseObstacles { get; set; }


    }
}