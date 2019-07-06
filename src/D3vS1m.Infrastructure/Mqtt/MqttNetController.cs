using D3vS1m.Domain.Config;
using D3vS1m.Domain.Infrastructure.Mqtt;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Options;
using MQTTnet.Protocol;
using Sin.Net.Domain.Persistence.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace D3vS1m.Infrastructure.Mqtt
{
    public class MqttNetController : IMqttControlable
    {
        // -- fields

        private IMqttClient _client;
        private MqttConfig _config;
        private IMqttClientOptions _options;

        public event MqttMessageReceivedEventHandler MessageReceived;
        public event MqttConnectedEventHandler Connected;
        public event MqttConnectedEventHandler Disconnected;

        // -- constructor

        public MqttNetController()
        {

        }

        // -- methods

        public bool CreateClient(ConfigBase config)
        {
            _config = config as MqttConfig;
            _options = new MqttClientOptionsBuilder()
               .WithTcpServer(_config.Broker, _config.Port)
               .WithClientId(_config.ClientID)
               .WithCleanSession(true)
               .Build();

            var factory = new MqttFactory();
            _client = factory.CreateMqttClient();

            //_client.Connected += mqttConnected;
            _client.UseConnectedHandler(async e =>
            {
                // fire own event for application layer
                if (e.AuthenticateResult.ResultCode != MqttClientConnectResultCode.Success)
                {
                    throw new Exception($"Connection error '{e.AuthenticateResult.ResultCode.ToString()}' in {this.GetType().Name}");
                }

                Connected?.Invoke(this, new MqttConnectedEventArgs(_config.Broker, _config.Port, _config.ClientID));
                // bind all topics to the connected broker
                foreach (string topic in _config.Topics)
                {
                    await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic(topic).Build());
                }
            });

            _client.UseDisconnectedHandler(async e =>
            {
                Log.Warn($"Disconnected from broker '{_config.Broker}'", this);
                // fire own event for application layer
                Disconnected?.Invoke(this, new MqttConnectedEventArgs(_config.Broker, _config.Port, _config.ClientID));
                await Task.Delay(TimeSpan.FromSeconds(5));
                try
                {
                    await ConnectAsync();
                }
                catch (Exception ex)
                {
                    Log.Error("could not reconnect", this);
                    Log.Fatal(ex);
                }
            });

            _client.UseApplicationMessageReceivedHandler(e =>
            {
                Log.Trace(e.ApplicationMessage.Topic, this);
                string topic = e.ApplicationMessage.Topic;
                string msg = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                MessageReceived?.Invoke(this, new MqttReceivedEventArgs(topic, msg));
            });

            return true;
        }

        public async Task ConnectAsync()
        {
            await _client.ConnectAsync(_options);
        }

        public async Task DisconnectAsync()
        {
            await _client.DisconnectAsync();
        }

        public Task PublishAsync(string topic, string message)
        {
            return PublishAsync(topic, message, _config.QoS);
        }

        public async Task PublishAsync(string topic, string message, int qos)
        {
            MqttApplicationMessage msg = new MqttApplicationMessageBuilder()
               .WithTopic(topic)
               .WithPayload(message)
               .WithRetainFlag(false)
               .Build();

            if (qos >= 0 &&
                qos <= 2)
            {
                msg.QualityOfServiceLevel = (MqttQualityOfServiceLevel)qos;
            }
            else
            {
                msg.QualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce;
            }

            await _client.PublishAsync(msg);
        }

        // -- properties

        public ConfigBase Config { get { return _config; } }

    }
}
