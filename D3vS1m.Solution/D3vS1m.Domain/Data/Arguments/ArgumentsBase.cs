namespace D3vS1m.Domain.Data.Arguments
{
    /// <summary>
    /// This class is an abstract base class for all concrete argument implementations used by the simulation models
    /// </summary>
    public abstract class ArgumentsBase
    {
        /// <summary>
        /// Returns the Name property of the abstract base class
        /// </summary>
        /// <returns>result string</returns>
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
