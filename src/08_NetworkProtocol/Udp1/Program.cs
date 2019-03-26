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
            //Thread.Sleep(1000);
            Send1();
            Console.ReadKey();
        }

        public static void Send1()
        {
            string temp = "您好";
            int count = 20;

            var Send1 = new Task(() => {
                UdpClient udpClient = new UdpClient();
                for (int i = 0; i < count; i++)
                {
                    byte[] b = System.Text.Encoding.UTF8.GetBytes(temp + "---" + i);
                    udpClient.Send(b, b.Length, Dns.GetHostName(), 8080);
                }
            });
            Send1.Start();
            var Send2 = new Task(() => {
                UdpClient udpClient = new UdpClient();
                for (int i = 0; i < count; i++)
                {
                    IPHostEntry iPHostEntry = Dns.GetHostEntry(Dns.GetHostName());
                    IPAddress address = IPAddress.Parse("127.138.2.2");
                    IPEndPoint iPEndPoint = new IPEndPoint(address, 8080);

                    byte[] c = System.Text.Encoding.UTF8.GetBytes(temp + "---hahaha");
                    udpClient.Send(c, c.Length, iPEndPoint);
                }
            });
            Send2.Start();
            

            UdpClient udpClient1 = new UdpClient(8088);
            var listen1 = new Task(() => {
                while (true)
                {
                    IPEndPoint iPEndPoint1 = new IPEndPoint(IPAddress.Any, 8088);
                    string text = System.Text.Encoding.UTF8.GetString(udpClient1.Receive(ref iPEndPoint1));
                    Console.WriteLine(text + "----" + iPEndPoint1.Address + ":" + iPEndPoint1.Port);
                }
            });
            listen1.Start();

            
        }

        public static void Send2()
        {
            // 得到本机IP，设置TCP端口号
            IPEndPoint ip = new IPEndPoint(IPAddress.Any, 8001);
            Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            newsock.Bind(ip);
            Console.WriteLine("This is a Server, host name is {0}", Dns.GetHostName());

            //等待客户机连接
            Console.WriteLine("Waiting for a client");

        }
    }
}
