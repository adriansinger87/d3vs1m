namespace D3vS1m.Domain.Data.Arguments
{
    /// <summary>
    /// This class is an abstract base class for all concrete argument implementations used by the simulation models
    /// </summary>
    public abstract class ArgumentsBase
    {
        // -- methods

        /// <summary>
        /// Gets a hard coded representation of the arguments
        /// </summary>
        /// <returns>The default instance of the arguments implementation</returns>
        public abstract ArgumentsBase GetDefault();

        /// <summary>
        /// Returns the Name property of the abstract base class
        /// </summary>
        /// <returns>result string</returns>
        public override string ToString()
        {
            return $"args: {Name}";
        }

        // -- properties

        /// <summary>
        /// Gets or sets the name of the specific arguments class
        /// </summary>
        public string Name { get; set; }
    }
}
