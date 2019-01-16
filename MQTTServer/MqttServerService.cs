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
    }
}
