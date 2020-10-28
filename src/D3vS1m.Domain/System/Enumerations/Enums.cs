namespace D3vS1m.Domain.System.Enumerations
{
    /// <summary>
    /// It describes the executed simulation model in the state machine
    /// </summary>
    public enum SimulationTypes {
        Antenna,
        Channel,
        Devices,
        Network,
        Communication,
        Energy,
        Scene,
        Custom
    }
    
    public enum PartTypes
    {
        PowerSupply,
        Transceiver,
        Controller,
        Memory,
        Antenna,
        Sensor,
        Actuator,
        Custom
    };

    public enum ExportTypes
    {
        Json,
        Csv
    }

    public enum ImportTypes
    {
        Json,
        Csv,
        WavefrontObj
    }
}
