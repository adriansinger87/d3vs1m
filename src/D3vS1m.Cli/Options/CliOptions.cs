namespace D3vS1m.Cli.Options
{
    /// <summary>
    /// This class contains all options for command line arguments
    /// </summary>
    public class CliOptions
    {
        public CliOptions()
        {
            Workspace = "";
        }

        // properties

        [Cli(Short = "w", Long = "workspace")]
        public string Workspace { get; set; }

        [Cli(Short = "v", Long = "verbose")]
        public bool Verbose { get; set; }

        [Cli(Short = "t", Long = "runtime")]
        public string RuntimeFile { get; set; }

        [Cli(Short = "d", Long = "devices")]
        public string DevicesFile { get; set; }

        [Cli(Short = "a", Long = "antenna")]
        public string AntennaFile { get; set; }

        [Cli(Short = "r", Long = "radio")]
        public string ChannelFile { get; set; }

        [Cli(Short = "c", Long = "comm")]
        public string CommunicationlFile { get; set; }

        [Cli(Short = "e", Long = "energy")]
        public string EnergyFile { get; set; }

        [Cli(Short = "s", Long = "scene")]
        public string SceneFile { get; set; }

        [Cli(Short = "b", Long = "break")]
        public bool Break { get; set; }
    }
}
