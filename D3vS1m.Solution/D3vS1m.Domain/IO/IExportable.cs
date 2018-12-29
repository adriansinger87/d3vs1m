using D3vS1m.Domain.System.Enumerations;

namespace D3vS1m.Domain.IO
{
    /// <summary>
    /// Interface for concrete export functionality
    /// </summary>
    public interface IExportable
    {
        /// <summary>
        /// Takes the cocnrete implementation of settings
        /// </summary>
        /// <param name="setting">settings concretion</param>
        /// <returns>the calling IExportable-instance for method chaining</returns>
        IExportable Setup(IOSettingsBase setting);

        /// <summary>
        /// Takes a generic data type that shall be exported
        /// </summary>
        /// <typeparam name="T">the explicit type of the data</typeparam>
        /// <param name="data">the instance of the data</param>
        /// <returns>the calling IExportable-instance for method chaining</returns>
        IExportable Build<T>(T data);

        /// <summary>
        /// Runs the export implementation
        /// </summary>
        /// <returns>The string is a generic result format and is specified by the export concretion</returns>
        string Export();

        // -- properties

        /// <summary>
        /// Gets the export type
        /// </summary>
        ExportTypes Type { get; }
    }
}
