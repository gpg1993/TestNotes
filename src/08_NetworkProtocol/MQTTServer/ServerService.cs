using System;
using System.Collections.Generic;
using System.Text;
using MQTTnet;
using MQTTnet.Extensions.Rpc;
using MQTTnet.Protocol;
using MQTTnet.Server;

namespace MQTTServer
{
    /// <summary>
    /// mqtt服务端单例
    /// </summary>
    public class ServerService
    {
        #region Fields&&Attributes
        private static ServerService _instance = null;
        private List<Server> list;
        private static readonly object syncLocker = new object();
        #endregion

        #region constructions
        public ServerService()
        {
            list = new List<Server>();
        }

        public static ServerService CreateIntance()
        {
            if (_instance==null)
            {
                lock (syncLocker)
                {
                    if (_instance == null)
                    {
                        _instance = new ServerService();
                    }
                }
            }
            return _instance;
        }
        #endregion

        #region Functions
        public Server GetServer(string ServerName)
        {
            Server server = null;
            if (list != null && list.Count > 0)
            {
                server = list.Find(c => c.ServerName == ServerName);
                
            }
            return server;
        }
        private Random rand = new Random();
        public Server GetServer()
        {
            int index = rand.Next(list.Count);
            var server = list[index];
            return server;
        }
        public void AddServer(Server server)
        {
            if (list != null && list.Count == 0)
            {
                list.Add(server);
            }
            else
            {
                if (list.BinarySearch(server)<0)
                {
                    list.Add(server);
                }
            }
        }
        public void RemoveServer(Server server)
        {
            if (list == null || list.Count <= 0 || list.BinarySearch(server) < 0)
                return;
            list.Remove(server);
        }
        public void RemoveServer(string serverName)
        {
            if (list == null || list.Count <= 0)
                return;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ServerName.Equals(serverName))
                {
                    list.Remove(list[i]);
                    break;
                }
            }
        }
        #endregion
    }
}
