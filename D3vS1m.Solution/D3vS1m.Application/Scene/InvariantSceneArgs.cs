using D3vS1m.Domain.Data.Arguments;
using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Scene
{
    public class InvariantSceneArgs : BaseArgs
    {
        public InvariantSceneArgs()
        {
            Name = Models.InvariantScene;
        }

        public object[] Obstacles { get; }
    }
}
