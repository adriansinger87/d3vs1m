namespace D3vS1m.Domain.System.Enumerations
{
    /// <summary>
    /// It describes the executed simulation model in the state machine
    /// </summary>
    public enum SimulationModels {
        Antenna,
        Channel,
        Devices,
        Network,
        Communication,
        Energy,
        Scene,
        Custom
    }

    public enum ExportTypes
    {
        Json,
        Csv
    }

    public enum ImportTypes
    {
        Json,
        Csv
    }
}
