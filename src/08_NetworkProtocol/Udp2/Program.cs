using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Udp2
{
    class Program
    {
        static void Main(string[] args)
        {
            ListenUdp1();
            ListenUdp2();

            Console.ReadKey();
        }

        public static void ListenUdp1()
        {
            IPAddress address1 = IPAddress.Parse("127.138.2.2");
            IPEndPoint iep1 = new IPEndPoint(address1, 8080);
            UdpClient uc = new UdpClient(iep1);
            Action<UdpClient> UdpAction = (udp) => {
                while (true)
                {
                    IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
                    IPAddress address = IPAddress.Parse("192.168.2.1");
                    IPEndPoint iep = new IPEndPoint(address, 8888);
                    //获得Form1发送过来的数据包
                    string text = System.Text.Encoding.UTF8.GetString(uc.Receive(ref iep));

                    Console.WriteLine(iep.Address + "---" + iep.Port);
                    Console.WriteLine(text);
                }
            };
            Action action = () => { };
            Task task1 = new Task(() => UdpAction(uc));
            task1.Start();
        }
        public static void ListenUdp2()
        {
            UdpClient uc = new UdpClient(8080);
            Action<UdpClient> UdpAction = (udp) => {
                while (true)
                {
                    IPAddress[] addresses = Dns.GetHostAddresses(Dns.GetHostName());
                    IPAddress address = IPAddress.Parse("192.168.2.1");
                    IPEndPoint iep = new IPEndPoint(address, 8888);
                    //获得Form1发送过来的数据包
                    string text = System.Text.Encoding.UTF8.GetString(uc.Receive(ref iep));

                    Console.WriteLine(iep.Address + "---" + iep.Port);
                    Console.WriteLine(text);
                }
            };
            Action action = () => { };
            Task task1 = new Task(() => UdpAction(uc));
            task1.Start();
        }
    }
}
