using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SocketBase
{
    public static class ChatUtil
    {
        public static int Port = 44449;//port 
        /// <summary>
        /// get local ip address
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetLocalIPAddress()
        {
            string hostName = Dns.GetHostName();//get hostname
            IPAddress[] addressList = Dns.GetHostAddresses(hostName);//get hostAddresses by hostname
            if (addressList.Length > 0)
                return addressList[0];//define default hostAdress Index 0
            return null;
        }
        /// <summary>
        /// get local ipendpoint
        /// </summary>
        /// <returns></returns>
        public static IPEndPoint GetLocalIPEndPoint()
        {
            IPAddress iPAddress = ChatUtil.GetLocalIPAddress();
            int port = ChatUtil.Port;
            return new IPEndPoint(iPAddress, port);
        }

    }
}
