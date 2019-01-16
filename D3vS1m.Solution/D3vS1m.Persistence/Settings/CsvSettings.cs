using System;
using System.Collections.Generic;
using System.Text;

namespace D3vS1m.Persistence.Settings
{
    /// <summary>
    /// Die Klasse repräsentiert alle Einstellungsmöglichkeiten zum Lesen und Schreiben von CSV Dateien
    /// </summary>
    public class CsvSettings : FileSettings
    {
        // -- fields

        private bool _hasHeader;

        // -- constructor

        public CsvSettings()
        {
            Separator = ';';
            HasHeader = true;
        }

        // -- properties

        /// <summary>
        /// Gibt das Trennzeichen zwischen den Spalten aus oder legt dieses fest.
        /// </summary>
        public char Separator { get; set; }

        /// <summary>
        /// Gibt an oder legt fest, ob die erste Zeile als Tabellenkopf ausgewertet werden soll bzw. ob es nutzerspezifische Daten vor den eigentlichen Csv-Daten gibt (true),
        /// Wenn die erste Zeile bereits Csv-Daten enthält und die Eigenschaft CustomHeader null ist, wird 'false' ausgegeben.
        /// </summary>
        public bool HasHeader
        {
            get
            {
                return (_hasHeader == true || !string.IsNullOrEmpty(CustomHeader));
            }
            set
            {
                _hasHeader = value;
            }
        }

        /// <summary>
        /// Gibt an oder legt fest welcher Text vor den eigentlichen Csv-Daten in der Ausgabedatei stehen sollen
        /// </summary>
        public string CustomHeader { get; set; }
    }
}
