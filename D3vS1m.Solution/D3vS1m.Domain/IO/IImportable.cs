using D3vS1m.Domain.System.Enumerations;

namespace D3vS1m.Domain.IO
{
    /// <summary>
    /// Interface for concrete import functionality
    /// </summary>
    public interface IImportable
    {
        /// <summary>
        /// Takes the cocnrete implementation of settings
        /// </summary>
        /// <param name="setting">settings concretion</param>
        /// <returns>the calling IImportable-instance for method chaining</returns>
        IImportable Setup(IOSettingsBase setting);

        /// <summary>
        /// Runs the import implementation
        /// </summary>
        /// <returns>the calling IImportable-instance for method chaining</returns>
        IImportable Import();

        /// <summary>
        /// Casts the imported data instance in the generic result type T 
        /// </summary>
        /// <typeparam name="T">The target type T for the imported data</typeparam>
        /// <param name="casting">The concrete casting functionality</param>
        /// <returns>The imported data as target type</returns>
        T CastTo<T>(ICasteable casting) where T : new();

        // -- properties

        /// <summary>
        /// Gets the import type
        /// </summary>
        ImportTypes Type { get; }
    }
}
