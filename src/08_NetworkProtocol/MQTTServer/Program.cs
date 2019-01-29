using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MQTTServer
{
    class Program
    {
        static void Main(string[] args)
        {
            MqttServerService mqttServerService = new MqttServerService();
            IMqttServer mqttServer = mqttServerService.CreateMqttServer();
            mqttServer.ApplicationMessageReceived += MqttServer_ApplicationMessageReceived;
            mqttServer.ClientConnected += MqttServer_ClientConnected;
            mqttServer.ClientDisconnected += MqttServer_ClientDisconnected;
            mqttServer.ClientSubscribedTopic += MqttServer_ClientSubscribedTopic;
            mqttServer.ClientUnsubscribedTopic += MqttServer_ClientSubscribedTopic;

            ServerService serverService = new ServerService();
            Server server = new Server();
            server.ServerName = Guid.NewGuid().ToString();
            server.mqttServer = mqttServer;
            serverService.AddServer(server);
            
            mqttServerService.StartMqttServer(mqttServer, CreateOptions());
            Console.WriteLine("发送多少次？ ");
            string key = Console.ReadLine();
            for (int i = 0; i < Convert.ToInt32(key); i++)
            {
                var applicationMessage = new MqttApplicationMessageBuilder()
                   .WithTopic("A/B/C")
                   .WithPayload(Encoding.UTF8.GetBytes(i.ToString()))
                   .WithExactlyOnceQoS();
                mqttServerService.BroadCast(mqttServer, applicationMessage);
            }
            
            Console.ReadLine();
        }

        private static void MqttServer_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {
            Console.WriteLine($"客户端[{e.ClientId}]>> 主题：{e.ApplicationMessage.Topic} 消息：{Encoding.UTF8.GetString(e.ApplicationMessage.Payload)} Qos：{e.ApplicationMessage.QualityOfServiceLevel} 保留：{e.ApplicationMessage.Retain}");
        }
        private static void MqttServer_ClientConnected(object sender, MqttClientConnectedEventArgs e)
        {
            Console.WriteLine($"客户端[{e.ClientId}]>> 连接");
        }
        private static void MqttServer_ClientDisconnected(object sender, MqttClientDisconnectedEventArgs e)
        {
            Console.WriteLine($"客户端[{e.ClientId}]>> 断开");
        }
        private static void MqttServer_ClientSubscribedTopic(object sender, MqttClientSubscribedTopicEventArgs e)
        {
            Console.WriteLine($"客户端[{e.ClientId}]>> 订阅 {e.TopicFilter}");
        }
        private static void MqttServer_ClientSubscribedTopic(object sender, MqttClientUnsubscribedTopicEventArgs e)
        {
            Console.WriteLine($"客户端[{e.ClientId}]>> 取消订阅 {e.TopicFilter}");
        }


        private static MqttServerOptions CreateOptions()
        {
            try
            {

                var options = new MqttServerOptions()
                {
                    //连接验证
                    ConnectionValidator = p =>
                    {
                        if (p.ClientId == "XYZ")
                        {
                            if (p.Username != "USER" || p.Password != "PASS")
                            {
                                p.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                                return;
                            }
                        }
                        //if (p.ClientId.Length < 10)
                        //{
                        //    p.ReturnCode = MqttConnectReturnCode.ConnectionRefusedIdentifierRejected;
                        //    return;
                        //}
                        p.ReturnCode = MqttConnectReturnCode.ConnectionAccepted;
                    },
                    //消息拦截器
                    ApplicationMessageInterceptor = context =>
                    {
                        if (MqttTopicFilterComparer.IsMatch(context.ApplicationMessage.Topic, "A/B/C"))
                        {
                            byte[] news = Encoding.UTF8.GetBytes("-服务器拦截处理后的消息");
                            byte[] temp = new byte[context.ApplicationMessage.Payload.Length + news.Length];
                            Array.Copy(context.ApplicationMessage.Payload, temp, context.ApplicationMessage.Payload.Length);
                            Array.Copy(news, 0, temp, context.ApplicationMessage.Payload.Length, news.Length);
                            context.ApplicationMessage.Payload = temp;
                        }
                    },
                    //订阅拦截器
                    SubscriptionInterceptor = context => {
                        if (context.TopicFilter.Topic.StartsWith("A/B/C") && context.ClientId == "XYZ")
                        {
                            context.AcceptSubscription = false;
                            context.CloseConnection = true;
                        }
                    },
                    Storage = new RetainedMessageHandler(),


                };
                //options.TlsEndpointOptions.Port = 8880;
                return options;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public class RetainedMessageHandler : IMqttServerStorage
        {
            private const string Filename = "C:\\MQTT\\RetainedMessages.json";

            public Task SaveRetainedMessagesAsync(IList<MqttApplicationMessage> messages)
            {
                var directory = Path.GetDirectoryName(Filename);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                File.WriteAllText(Filename, JsonConvert.SerializeObject(messages));
                return Task.FromResult(0);
            }

            public Task<IList<MqttApplicationMessage>> LoadRetainedMessagesAsync()
            {
                IList<MqttApplicationMessage> retainedMessages;
                if (File.Exists(Filename))
                {
                    var json = File.ReadAllText(Filename);
                    retainedMessages = JsonConvert.DeserializeObject<List<MqttApplicationMessage>>(json);
                }
                else
                {
                    retainedMessages = new List<MqttApplicationMessage>();
                }

                return Task.FromResult(retainedMessages);
            }



        }
    }
}
