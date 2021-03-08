using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Application.Runtime.Options
{
	public abstract class OptionsBase
	{
        protected OptionsBase()
        {
            Workspace = "";
        }

        // properties

        public abstract string Workspace { get; set; }

        public abstract string RuntimeFile { get; set; }

        public abstract string DevicesFile { get; set; }

        public abstract string AntennaFile { get; set; }

        public abstract string ChannelFile { get; set; }

        public abstract string CommunicationlFile { get; set; }

        public abstract string EnergyFile { get; set; }

        public abstract string SceneFile { get; set; }
    }
}
