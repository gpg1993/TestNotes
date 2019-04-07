using MagicOnionServer;
using MagicOnion;
using MagicOnion.Client;
using System;
using Grpc.Core;
using System.Threading.Tasks;
using System.IO;

namespace MagicOnionClient1
{
    class Program
    {
        static void Main(string[] args)
        {
            Client1().GetAwaiter().GetResult();

            Console.ReadKey();
        }

        public static async Task Client1()
        {
            //标准gRPC通道
            var channel = new Channel("localhost", 123456, ChannelCredentials.Insecure);
            //var channel = new Channel("localhost", 123456, new SslCredentials(File.ReadAllText("roots.pem")));

            //获取MagicOnion动态客户端代理
            var client = MagicOnionClient.Create<ICalcService>(channel);
            //调用方法 
            var result = await client.SumAsync(100, 200);
            Console.WriteLine("Client Received:" + result);

            var client1 = MagicOnionClient.Create<IStudentService>(channel);
            var result1 = await client1.AddStudent(new Student { Id = 5, Age = "18", name = "France", weight = 180.00 });
            Console.WriteLine("Client Received:" + result1);
            var result2 = client1.GetAllStudent();
            Console.WriteLine("Client Received:" + result2);
        }
    }
}
