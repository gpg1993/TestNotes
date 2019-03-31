using MagicOnionServer;
using MagicOnion;
using MagicOnion.Client;
using System;
using Grpc.Core;
using System.Threading.Tasks;

namespace MagicOnionClient1
{
    class Program
    {
        static void Main(string[] args)
        {
            Client1().GetAwaiter().GetResult();
            //Task.Run(Client1).Start();
            Console.ReadKey();
        }

        public static async Task Client1()
        {
            //标准gRPC通道
            var channel = new Channel("localhost", 123456, ChannelCredentials.Insecure);

            //获取MagicOnion动态客户端代理
            var client = MagicOnionClient.Create<ICalcSerivce>(channel);

            //调用方法 
            var result = await client.SumAsync(100, 200);
            Console.WriteLine("Client Received:" + result);
        }
    }
}
