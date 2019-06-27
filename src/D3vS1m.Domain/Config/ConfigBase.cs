namespace D3vS1m.Domain.Config
{
    /// <summary>
    /// Abstract class to provide basic features for all kinds of config concretions.
    /// </summary>
    public abstract class ConfigBase
    {
        // -- constructors

        protected ConfigBase(string name)
        {
            Name = name;
        }

        // -- methods

        public override string ToString() => Name;

        // -- properties

        /// <summary>
        /// Gets or sets the name property.
        /// </summary>
        public virtual string Name { get; set; }
    }
}
