using System;
using System.Collections.Generic;
using System.Text;

namespace Bacon.Service1.Util
{
    public class MongoAppSetting
    {
        public string MongoConn { get; set; }
        public string MongoDbDatabase         { get; set; }
    }

    public class ConsulAppSetting
    {
        public string IP   { get; set; }
        public int    Port { get; set; }
    }

    public class GrpcAppSetting
    {
        public string Name { get; set; }
        public string IP   { get; set; }
        public int    Port { get; set; }
    }
}
