using Microsoft.Extensions.Configuration;
using MQTTnet;
using MQTTnet.Client;
using Newtonsoft.Json;
using System.Text;

namespace Subscriber
{
    internal class Program
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

                // handler
                mqttClient.ApplicationMessageReceivedAsync += e => ApplicationMessageReceivedHandler(e);
                
                // connect
                await mqttClient.ConnectAsync(options, CancellationToken.None);

                // subcribing to a topic
                var subOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                    .WithTopicFilter(t => { t.WithTopic(configuration["MosquittoTopic"]); })
                    .Build();
                await mqttClient.SubscribeAsync(subOptions, CancellationToken.None);

                // hold the connection
                Console.ReadLine();
            }
        }

        private static Task ApplicationMessageReceivedHandler(MqttApplicationMessageReceivedEventArgs e)
        {
            var body = e.ApplicationMessage.Payload;
            var message = Encoding.UTF8.GetString(body);
            MosquittoMessage mm = JsonConvert.DeserializeObject<MosquittoMessage>(message);

            Console.WriteLine("[x] Received: {0}", mm.Name);
            return Task.CompletedTask;
        }
    }

    public class MosquittoMessage
    {
        public string Name { get; set; }
        public string Message { get; set; }
    }
}
