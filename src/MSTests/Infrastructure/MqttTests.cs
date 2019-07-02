using D3vS1m.Domain.Infrastructure.Mqtt;
using D3vS1m.Infrastructure.Mqtt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sin.Net.Domain.Logging;
using System.Threading.Tasks;

namespace MSTests.Infrastructure
{
    [TestClass]
    public class MqttTests : TestBase
    {
        IMqttControlable _mqtt;
        private bool _isConnected;
        private MqttConfig _config;

        [TestInitialize]
        public override void Arrange()
        {
            base.Arrange();

            _mqtt = new MqttNetController();
            _config = new MqttConfig
            {
                Broker = "broker.hivemq.com",
                Port = 1883,
                ClientID = "D3vS1m-MSTests-Client"
            };
            _mqtt.Connected += OnConnected;
            _mqtt.CreateClient(_config);
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
            _mqtt.DisconnectAsync();
            _mqtt = null;
        }

        [TestMethod]
        public async Task ConnectAndDisconnectMqtt()
        {
            // connect: act & assert
            await ConnectAsync();
            Assert.IsTrue(_isConnected, $"mqtt {_mqtt} is not connected");

            // disconnect: act & assert
            await DisconnectAsync();
            Assert.IsFalse(_isConnected);
        }

        // -- private methods

        private async Task ConnectAsync()
        {
            Log.Info($"Connecting with {_config.ToString()}", this);

            // act
            await _mqtt.ConnectAsync();
        }

        private async Task DisconnectAsync()
        {
            Log.Info($"Disconnecting from {_config.ToString()}", this);

            // act
            await _mqtt.DisconnectAsync().ContinueWith((task) =>
            {
                _isConnected = false;
            });
        }

        // -- events

        private void OnConnected(object sender, MqttConnectedEventArgs e)
        {
            Log.Info($"Connected to {e.Broker} with id {e.ClientID}", this);
            _isConnected = true;

            var mqtt = sender as MqttNetController;
            var config = mqtt.Config as MqttConfig;

            //_devices.AddRange(config.Topics.Select(s => new DeviceBase { Id = s }));
        }
    }
}
