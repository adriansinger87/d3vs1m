namespace D3vS1m.Domain.Infrastructure.Mqtt
{
    public delegate void MqttConnectedEventHandler(object sender, MqttConnectedEventArgs e);

    public class MqttConnectedEventArgs
    {
        // -- Constructor

        public MqttConnectedEventArgs(string broker, int port, string clientId)
        {
            Broker = broker;
            Port = port;
            ClientID = clientId;
        }

        /// <summary>
        /// Gets the string of the other properties.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Broker}:{Port} - {ClientID}";
        }

        // -- properties

        public string Broker { get; private set; }

        public int Port { get; private set; }

        public string ClientID { get; private set; }
    }
}
