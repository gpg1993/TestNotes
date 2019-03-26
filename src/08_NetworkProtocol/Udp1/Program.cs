using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Udp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(1000);
            IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress address = IPAddress.Parse("127.138.2.2");
            IPEndPoint iPEndPoint = new IPEndPoint(address, 8080);
            UdpClient udpClient = new UdpClient();
            string temp = "您好";
            int count = 20;
            for (int i = 0; i < count; i++)
            {
                byte[] b = System.Text.Encoding.UTF8.GetBytes(temp + "---" + i);
                udpClient.Send(b, b.Length, Dns.GetHostName(),8080);

                byte[] c = System.Text.Encoding.UTF8.GetBytes(temp + "---hahaha");
                udpClient.Send(c, c.Length, iPEndPoint);
            }
            Console.ReadKey();
        }
    }
}
