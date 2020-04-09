namespace D3vS1m.Cli.Options
{
    /// <summary>
    /// This class contains all options for command line arguments
    /// </summary>
    public class CliOptions
    {
        public CliOptions()
        {
            // HACK: The workspace should be empty so that no specific workspace folder is assumed.
            Workspace = "";
        }

        // properties

        [Options(Short = "w", Long = "workspace")]
        public string Workspace { get; set; }

        [Options(Short = "v", Long = "verbose")]
        public bool Verbose { get; set; }

        [Options(Short = "t", Long = "runtime")]
        public string RuntimeFile { get; set; }

        [Options(Short = "d", Long = "devices")]
        public string DevicesFile { get; set; }

        [Options(Short = "a", Long = "antenna")]
        public string AntennaFile { get; set; }

        [Options(Short = "r", Long = "radio")]
        public string ChannelFile { get; set; }

        [Options(Short = "c", Long = "comm")]
        public string CommunicationlFile { get; set; }

        [Options(Short = "e", Long = "energy")]
        public string EnergyFile { get; set; }

        [Options(Short = "s", Long = "scene")]
        public string SceneFile { get; set; }
    }
}
