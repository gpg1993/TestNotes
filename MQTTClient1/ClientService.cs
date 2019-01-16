using System;
using System.Collections.Generic;
using System.Text;

namespace MQTTClient1
{
    public class ClientService
    {
        #region Fields&&Attributes
        private static ClientService _instance = null;
        private List<Client> list;
        private static readonly object syncLocker = new object();
        #endregion

        #region constructions
        public ClientService()
        {
            list = new List<Client>();
        }

        public static ClientService CreateIntance()
        {
            if (_instance == null)
            {
                lock (syncLocker)
                {
                    if (_instance == null)
                    {
                        _instance = new ClientService();
                    }
                }
            }
            return _instance;
        }
        #endregion

        #region Functions
        public Client GetServer(string ClientName)
        {
            Client Client = null;
            if (list != null && list.Count > 0)
            {
                Client = list.Find(c => c.ClientName == ClientName);

            }
            return Client;
        }
        private Random rand = new Random();
        public Client GetServer()
        {
            int index = rand.Next(list.Count);
            var Client = list[index];
            return Client;
        }
        public void AddServer(Client Client)
        {
            if (list != null && list.Count == 0)
            {
                list.Add(Client);
            }
            else
            {
                if (list.BinarySearch(Client) < 0)
                {
                    list.Add(Client);
                }
            }
        }
        public void RemoveServer(Client Client)
        {
            if (list == null || list.Count <= 0 || list.BinarySearch(Client) < 0)
                return;
            list.Remove(Client);
        }
        public void RemoveServer(string ClientName)
        {
            if (list == null || list.Count <= 0)
                return;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].ClientName.Equals(ClientName))
                {
                    list.Remove(list[i]);
                    break;
                }
            }
        }
        #endregion
    }
}
