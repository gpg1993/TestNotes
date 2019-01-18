using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MQTTClient1
{
    class Program
    {
        static void Main(string[] args)
        {
            GetTaskFor();
            Console.ReadLine();
        }

        public static async Task GetTaskFor()
        {
            for (int i = 0; i < 1000; i++)
            {
                await startClient();
            }
        }

        public static async Task startClient()
        {
            MqttClientService mqttClientService = new MqttClientService();
            IMqttClient mqttClient = mqttClientService.CreateMqttClient();
            mqttClient.ApplicationMessageReceived += MqttClient_ApplicationMessageReceived;
            mqttClient.Connected += MqttClient_Connected;
            mqttClient.Disconnected += MqttClient_Disconnected;

            MqttClientOptions mqttClientOptions = CreateOptions(ProtocolType.TCP, "127.0.0.1");
            await mqttClientService.ConnectServer(mqttClient, mqttClientOptions);
            var topicFilter = new TopicFilterBuilder().WithTopic("A/B/C").WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce);
            await mqttClientService.SubscribeMessage(mqttClient, topicFilter);
        }


        private static MqttClientOptions CreateOptions(ProtocolType protocolType, string IpAddress)
        {
            try
            {
                //启用TLS
                var tlsOptions = new MqttClientTlsOptions
                {
                    UseTls = true,
                    IgnoreCertificateChainErrors = true,
                    IgnoreCertificateRevocationErrors = true,
                    AllowUntrustedCertificates = true
                };
                var options = new MqttClientOptions
                {
                    ClientId = Guid.NewGuid().ToString(),
                };
                switch (protocolType)
                {
                    case ProtocolType.TCP:
                        options.ChannelOptions = new MqttClientTcpOptions
                        {
                            Server = IpAddress,
                            //Port = 8880
                            //TlsOptions = tlsOptions
                        };
                        break;
                    case ProtocolType.WS:
                        options.ChannelOptions = new MqttClientWebSocketOptions
                        {
                            Uri = IpAddress,
                            TlsOptions = tlsOptions,
                            
                        };
                        break;
                    default:
                        break;
                }
                if (options.ChannelOptions == null)
                {
                    throw new InvalidOperationException();
                }
                //设定证书
                options.Credentials = new MqttClientCredentials
                {
                    Username = "USER",
                    Password = "PASS"
                };

                options.CleanSession = true;//会话清除
                options.KeepAlivePeriod = TimeSpan.FromSeconds(10);

                //==遗言
                //WillMessage = new MqttApplicationMessage()
                //{
                //    Topic = txt_topic.Text.Trim(),
                //    Payload = (Encoding.UTF8.GetBytes("我的遗言")),
                //    QualityOfServiceLevel = MqttQualityOfServiceLevel.AtMostOnce,
                //    Retain = false

                //},
                //ProtocolVersion = MQTTnet.Serializer.MqttProtocolVersion.V311,
                return options;
            }
            catch (Exception)
            {

                throw;
            }

        }

        private static void MqttClient_Disconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
            Console.WriteLine($"客户端{e.ClientWasConnected}");
        }

        private static void MqttClient_Connected(object sender, MqttClientConnectedEventArgs e)
        {
            Console.WriteLine($"客户端{e.IsSessionPresent}");
        }

        private static void MqttClient_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            Console.WriteLine($"客户端{e.ClientId},主题为{e.ApplicationMessage.Topic}，消息{Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
        }
    }
}
