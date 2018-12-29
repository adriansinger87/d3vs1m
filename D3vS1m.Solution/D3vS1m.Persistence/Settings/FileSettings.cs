using D3vS1m.Domain.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace D3vS1m.Persistence.Settings
{
    public class FileSettings : IOSettingsBase
    {
        // -- constructor

        public FileSettings()
        {
            Location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
        }

        // -- properties

        /// <summary>
        /// Gibt den Pfad ohne die Dateiendung aus oder legt diese fest
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gibt die Dateiendung aus
        /// </summary>
        public string Extension
        {
            get
            {
                string ext = string.Empty;
                if (Name.Contains("."))
                {
                    ext = Name.Split('.').Last();
                }

                return ext;
            }
        }

        /// <summary>
        /// Gibt den vollständigen Pfad mit Dateinamen aus.
        /// </summary>
        public string FullPath
        {
            get
            {
                return Path.Combine(Location, base.Name);
            }
        }
    }
}
