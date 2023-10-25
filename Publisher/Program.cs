using MQTTnet.Client;
using MQTTnet;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().
                AddJsonFile("appsettings.json", false, true).
                Build();



            var mqttFactory = new MqttFactory();
            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var options = new MqttClientOptionsBuilder()
                    .WithTcpServer(configuration["MosquittoHost"], int.Parse(configuration["MosquittoPort"]))
                    .WithClientId(Guid.NewGuid().ToString())
                    .WithCleanSession()
                    .WithCredentials(configuration["MosquittoUsername"], configuration["MosquittoPassword"])
                    .Build();
                await mqttClient.ConnectAsync(options, CancellationToken.None);

                var mm = new MosquittoMessage()
                {
                    Name = "Test",
                    Message = "Test Message"
                };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(mm));
                var msg = new MqttApplicationMessageBuilder()
                    .WithTopic(configuration["MosquittoTopic"])
                    .WithPayload(body)
                    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
                    .Build();

                await mqttClient.PublishAsync(msg, CancellationToken.None);
                await mqttClient.DisconnectAsync();
            }
        }
    }

    public class MosquittoMessage
    {
        public string Name { get; set; }
        public string Message { get; set; }
    }
}