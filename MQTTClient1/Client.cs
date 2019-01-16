using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQTTClient1
{
    public class Client
    {
        public string ClientName { get; set; }
        public IMqttClient mqttClient { get; set; }
    }
    public enum ProtocolType
    {
        TCP,
        WS
    }
}
