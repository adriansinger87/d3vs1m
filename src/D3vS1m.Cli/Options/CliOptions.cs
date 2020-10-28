using D3vS1m.Application.Runtime.Options;

namespace D3vS1m.Cli.Options
{
    /// <summary>
    /// This class contains all options for command line arguments
    /// </summary>
    public class CliOptions : OptionsBase
    {
        public CliOptions()
        {
            Workspace = "";
        }

        // properties

        [Cli(Short = "w", Long = "workspace")]
        public override string Workspace { get; set; }

        [Cli(Short = "v", Long = "verbose")]
        public override bool Verbose { get; set; }

        [Cli(Short = "t", Long = "runtime")]
        public override string RuntimeFile { get; set; }

        [Cli(Short = "d", Long = "devices")]
        public override string DevicesFile { get; set; }

        [Cli(Short = "a", Long = "antenna")]
        public override string AntennaFile { get; set; }

        [Cli(Short = "r", Long = "radio")]
        public override string ChannelFile { get; set; }

        [Cli(Short = "c", Long = "comm")]
        public override string CommunicationlFile { get; set; }

        [Cli(Short = "e", Long = "energy")]
        public override string EnergyFile { get; set; }

        [Cli(Short = "s", Long = "scene")]
        public override string SceneFile { get; set; }

        [Cli(Short = "b", Long = "break")]
        public override bool Break { get; set; }
    }
}
