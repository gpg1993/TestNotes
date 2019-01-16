using MQTTnet;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQTTServer
{
    public class Server
    {
        public string ServerName { get; set; }
        public IMqttServer mqttServer { get; set; }
    }

    public class MqttMessageNotifyEventArgs : EventArgs
    {
        public bool IsConnect { get; set; }

        public string ClientId { get; set; }

        public MqttApplicationMessage MqttApplicationMessage { get; set; }

        public MqttMessageNotifyEventArgs(bool isConnect, string clientId, MqttApplicationMessage mqttApplicationMessage)
        {
            MqttApplicationMessage = mqttApplicationMessage;
            IsConnect = isConnect;
            ClientId = clientId;

        }

    }
}
