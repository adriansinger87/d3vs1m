using D3vS1m.Domain.Data.Arguments;
using System;

namespace D3vS1m.Application.Scene
{
    [Serializable]
    public class InvariantSceneArgs : ArgumentsBase
    {
        public InvariantSceneArgs() : base()
        {
            Reset();
        }

        public override void Reset()
        {
            Name = Models.Scene.Name;
        }

        public object[] Obstacles { get; }
    }
}
