using D3vS1m.Domain.Data.Arguments;

namespace D3vS1m.Application.Scene
{
    public class InvariantSceneArgs : ArgumentsBase
    {
        public InvariantSceneArgs()
        {
            Name = Models.InvariantScene;
        }

        public object[] Obstacles { get; }

    }
}
