using D3vS1m.Domain.Config;
using System.Threading.Tasks;

namespace D3vS1m.Domain.Infrastructure.Mqtt
{
    public interface IMqttControlable
    {
        bool CreateClient(ConfigBase config);

        Task PublishAsync(string topic, string message);
        Task PublishAsync(string topic, string message, int qos);

        Task ConnectAsync();

        Task DisconnectAsync();

        // -- Properties

        ConfigBase Config { get; }

        // events

        event MqttMessageReceivedEventHandler MessageReceived;
        event MqttConnectedEventHandler Connected;
        event MqttConnectedEventHandler Disconnected;
    }
}
