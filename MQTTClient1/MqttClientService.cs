using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQTTClient1
{
    public class MqttClientService
    {
        public IMqttClient CreateMqttClient()
        {
            return new MqttFactory().CreateMqttClient() as MqttClient;
        }
        public void ConnectServer(IMqttClient mqttClient, MqttClientOptions mqttClientOptions)
        {
            try
            {
                mqttClient.ConnectAsync(mqttClientOptions);
            }
            catch (MqttCommunicationException ee)
            {

                throw ee;
            }
        }
        public void DisconnectServer(IMqttClient mqttClient)
        {
            try
            {
                mqttClient.DisconnectAsync();
            }
            catch (MqttCommunicationException ee)
            {

                throw ee;
            }
        }
        public async void PublishMessage(IMqttClient mqttClient,string topic, string message)
        {
            if (string.IsNullOrEmpty(topic))
            {
                return;
            }
            var applicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(Encoding.UTF8.GetBytes(message))
            //.WithQualityOfServiceLevel(MqttQualityOfServiceLevel)
            .WithRetainFlag(true)//保持标志（Retain-Flag）该标志确定代理是否持久保存某个特定主题的消息。订阅该主题的新客户端将在订阅后立即收到该主题的最后保留消息。
            .Build();

            await mqttClient.PublishAsync(applicationMessage);
        }
    }
}
