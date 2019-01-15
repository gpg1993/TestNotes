using MQTTnet;
using MQTTnet.Exceptions;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQTTServer
{
    public class MqttServerService
    {
        public IMqttServer CreateMqttServer()
        {
            var mqttServer = new MqttFactory().CreateMqttServer() as MqttServer;
            mqttServer.ApplicationMessageReceived += MqttServer_ApplicationMessageReceived;
            mqttServer.ClientConnected += MqttServer_ClientConnected;
            mqttServer.ClientDisconnected += MqttServer_ClientDisconnected;
            mqttServer.ClientSubscribedTopic += MqttServer_ClientSubscribedTopic;
            mqttServer.ClientUnsubscribedTopic += MqttServer_ClientSubscribedTopic;
            return mqttServer;
        }
        public void StartMqttServer(IMqttServer mqttServer, IMqttServerOptions mqttServerOptions)
        {
            try
            {
                mqttServer.StartAsync(mqttServerOptions);
            }
            catch (MqttCommunicationException ex)
            {
                throw ex;
            }
        }
        public void StopMqttServer(IMqttServer mqttServer)
        {
            try
            {
                mqttServer.StopAsync();
            }
            catch (MqttCommunicationException ee)
            {
                throw ee;
            }

        }
        public void BroadCast(IMqttServer mqttServer,MqttApplicationMessageBuilder builder)
        {
            try
            {
                mqttServer.PublishAsync(builder.Build());
            }
            catch (MqttCommunicationException ee)
            {
                throw ee;
            }
        }

        private void MqttServer_ApplicationMessageReceived(object sender, MqttApplicationMessageReceivedEventArgs e)
        {

        }
        private void MqttServer_ClientConnected(object sender, MqttClientConnectedEventArgs e)
        {

        }
        private void MqttServer_ClientDisconnected(object sender, MqttClientDisconnectedEventArgs e)
        {

        }
        private void MqttServer_ClientSubscribedTopic(object sender, MqttClientSubscribedTopicEventArgs e)
        { }
        private void MqttServer_ClientSubscribedTopic(object sender, MqttClientUnsubscribedTopicEventArgs e)
        { }

    }
}
