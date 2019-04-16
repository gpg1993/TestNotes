using Grpc.Core;
using MagicOnion.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace MagicOnionTest01
{
    class Program
    {
        static void Main(string[] args)
        {
            //使用.netcore 中的host扩展发布服务
            const string GrpcHost = "localhost";//服务地址
            List<ServerPort> serverPorts = new List<ServerPort>() { new ServerPort(GrpcHost, 123456, ServerCredentials.Insecure) };
            var host = new HostBuilder()
                .UseMagicOnion(serverPorts)
               .Build();
            host.Run();
        }
    }
}
