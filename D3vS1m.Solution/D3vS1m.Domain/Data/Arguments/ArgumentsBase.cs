namespace D3vS1m.Domain.Data.Arguments
{
    /// <summary>
    /// This class is an abstract base class for all concrete argument implementations used by the simulation models
    /// </summary>
    public abstract class ArgumentsBase
    {
        public override string ToString()
        {
            return $"args: {Name}";
        }

        /// <summary>
        /// Gets or sets the name of the specific arguments class
        /// </summary>
        public string Name { get; set; }
    }
}
