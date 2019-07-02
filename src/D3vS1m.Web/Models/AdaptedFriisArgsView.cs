using D3vS1m.Application.Channel;
using D3vS1m.Domain.Data.Scene;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace D3vS1m.Web.Models
{
    public class AdaptedFriisArgsView : IAdaptedFriisArgsDecorable
    {
        // -- constructor

        private AdaptedFriisArgs _args;

        public AdaptedFriisArgsView(AdaptedFriisArgs args)
        {
            _args = args;
        }

        // -- methods

        public void Reset()
        {
            _args.Reset();
        }

        public void UpdatePositions()
        {
            _args.UpdatePositions();
        }

        // -- properties

        public string Guid { get => _args.Guid; set => _args.Guid = value; }
        public string Name { get => _args.Name; set => _args.Name = value; }

        public float AttenuationExponent { get => _args.AttenuationExponent; set => _args.AttenuationExponent = value; }
        public float AttenuationOffset { get => _args.AttenuationOffset; set => _args.AttenuationOffset = value; }
        public bool UseObstacles { get => _args.UseObstacles; set => _args.UseObstacles = value; }

        public RadioCuboid RadioBox { get => _args.RadioBox; set => _args.RadioBox = value; }

        [JsonIgnore]
        public Vertex[] RxPositions { get => _args.RxPositions; set => _args.RxPositions = value; }
        [JsonIgnore]
        public float[] RxValues { get => _args.RxValues; set => _args.RxValues = value; }
        [JsonIgnore]
        public List<float[]> RxColors { get => _args.RxColors; set => _args.RxColors = value; }
     
    }
}
