using System;

namespace D3vS1m.Cli.Options
{
    /// <summary>
    /// Attribute class...
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class OptionsAttribute : Attribute
    {
        public OptionsAttribute()
        {

        }

        public OptionsAttribute(string shortTerm, string longTerm) : this()
        {
            Short = shortTerm;
            Long = longTerm;
        }

        // -- properties

        /// <summary>
        /// Gets or sets the short term of the command line option.
        /// Do not use any special characters, pre- or suffixes.
        /// </summary>
        public string Short { get; set; }

        /// <summary>
        /// Gets or sets the long term of the command line option.
        /// Do not use any special characters, pre- or suffixes.
        /// </summary>
        public string Long { get; set; }
    }
}
