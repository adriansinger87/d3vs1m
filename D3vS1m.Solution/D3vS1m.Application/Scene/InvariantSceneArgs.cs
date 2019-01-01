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

        /// <summary>
        /// Gets or sets the flag to render the data of the radio map or not
        /// TODO: This is a presentation-layer functionality --> move it at sometime to the presentation layer
        /// </summary>
        public bool RenderRadioMap { get; set; }

    }
}
