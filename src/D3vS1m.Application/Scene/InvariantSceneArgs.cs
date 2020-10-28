using D3vS1m.Domain.Data.Arguments;
using System;

namespace D3vS1m.Application.Scene
{
    [Serializable]
    public class InvariantSceneArgs : ArgumentsBase
    {
        public InvariantSceneArgs() : base()
        {
            Key = Models.Scene.Key;
            Name = Models.Scene.Name;
            Reset();
        }

        public override void Reset()
        {
            
        }

        public object[] Obstacles { get; }
    }
}
