using System;

namespace D3vS1m.Domain.Data.Arguments
{
    /// <summary>
    /// This class is an abstract base class for all concrete argument implementations used by the simulation models
    /// </summary>
    [Serializable]
    public abstract class ArgumentsBase
    {
        public ArgumentsBase()
        {
            Guid = global::System.Guid.NewGuid().ToString();
            Active = false;
        }

        // -- methods

        /// <summary>
        /// Gets a hard coded representation of the arguments
        /// </summary>
        /// <returns>The default instance of the arguments implementation</returns>
        public abstract void Reset();

        /// <summary>
        /// Returns the Name property of the abstract base class
        /// </summary>
        /// <returns>result string</returns>
        public override string ToString() => $"args: {Name}";

        // -- properties

        public string Key { get; protected set; }

        /// <summary>
        /// Gets or sets the name of the specific arguments class
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets the global Identifier of the arguments instance
        /// </summary>
        public virtual string Guid { get; set; }

        /// <summary>
        /// Gets or sets if the corresponding simulator shall be used in the runtime or not.
        /// </summary>
        public virtual bool Active { get; set; }

    }
}
