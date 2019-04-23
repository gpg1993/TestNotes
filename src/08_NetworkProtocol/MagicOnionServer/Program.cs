using Grpc.Core;
using Grpc.Core.Logging;
using MagicOnion.Redis;
using MagicOnion.Server;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System.IO;
using System.Reflection;
using MagicOnion;
using MagicOnion.HttpGateway.Swagger;
using MagicOnion.Hosting;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using static Grpc.Core.Server;

namespace MagicOnionServer
{
    /* magicOnion 对grpc服务包裹了一层，通过再magicOnion中设置参数
     * 两种启动方式 
     * **/
    class Program
    {
        static void Main(string[] args)
        {
            #region 启动方式一 通过kestrel代理 模拟asp.netcore webhost
            //var webHost = new WebHostBuilder()
            //   .UseKestrel()
            //   .UseStartup<Startup>()
            //   .UseUrls("http://localhost:5433")
            //   .Build();

            //webHost.Run();
            #endregion

            #region 启动方式二 IHostBuilder 自宿主程序
            const string GrpcHost = "localhost";//服务地址
            ServerPort port1 = new ServerPort(GrpcHost, 123456, ServerCredentials.Insecure);
            List<ServerPort> serverPorts = new List<ServerPort>();
            serverPorts.Add(port1);
            MagicOnionOptions magicOnionOptions = new MagicOnionOptions(true)
            {
                MagicOnionLogger = new MagicOnionLogToGrpcLoggerWithNamedDataDump(),
                DefaultGroupRepositoryFactory = new RedisGroupRepositoryFactory(),
                GlobalFilters = new MagicOnionFilterAttribute[]
                { },
                EnableCurrentContext = true,//允许本地异步启动当前服务上下文选项
                DisableEmbeddedService = true,//不允许嵌入服务例如心跳（heartbeat）
                                              //FormatterResolver= MessagePack.d              
                IsReturnExceptionStackTraceInErrorDetail = false,//ture:程序本身处理异常，并返回消息。false:扩散到grpc引擎。默认为false
                                                                 //ServiceLocator //添加扩充本地服务
            };

            var host = new HostBuilder()
                .UseMagicOnion(serverPorts)
               .Build();

            host.StartAsync();
            #endregion
        }
    }
    public class Startup
    {
        MagicOnionServiceDefinition service;
        public Startup()
        {
            const string GrpcHost = "localhost";//服务地址
            Environment.SetEnvironmentVariable("SETTINGS_MAX_HEADER_LIST_SIZE", "1000000");
            GrpcEnvironment.SetLogger(new ConsoleLogger());
            service = MagicOnionEngine.BuildServerServiceDefinition(new MagicOnionOptions(true)
            {
                MagicOnionLogger = new MagicOnionLogToGrpcLoggerWithNamedDataDump(),
                DefaultGroupRepositoryFactory = new RedisGroupRepositoryFactory(),
                GlobalFilters = new MagicOnionFilterAttribute[]
                { },
                EnableCurrentContext = true,//允许本地异步启动当前服务上下文选项
                DisableEmbeddedService = true,//不允许嵌入服务例如心跳（heartbeat）
                                              //FormatterResolver= MessagePack.d              
                IsReturnExceptionStackTraceInErrorDetail = false,//ture:程序本身处理异常，并返回消息。false:扩散到grpc引擎。默认为false
                                                                 //ServiceLocator //添加扩充本地服务
            });
            var server = new global::Grpc.Core.Server
            {
                Services = { service },
                Ports = { new ServerPort(GrpcHost, 123456, ServerCredentials.Insecure) }
            };
            server.Start();


        }

        public void ConfigureServices(IServiceCollection services)
        {
            // 添加 MagicOnion服务默认，提供启动时引用
            services.Add(new ServiceDescriptor(typeof(MagicOnionServiceDefinition), service));
        }

        public void Configure(IApplicationBuilder app, MagicOnionServiceDefinition magicOnion)
        {
            // 选项：添加swagger的xml地址
            var xmlName = "MagicOnion.Swagger.xml";
            var xmlPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), xmlName);//生成到编译路径中

            // 服务网关有两个中间件
            // 一个是swaggerView（MaigcOnionSwaggerMiddleware）
            //使用MagicOnion的Swagger扩展，就是让你的rpc接口也能在swagger页面上显示
            //下面这些东西你可能乍一看就懵逼，但你看到页面的时候就会发现，一个萝卜一个坑。
            //注意：swagger原生用法属性都是大写的，这里是小写。
            app.UseMagicOnionSwagger(magicOnion.MethodHandlers, new SwaggerOptions("MagicOnion.Server", "Swagger Integration Test", "/")
            {
                Info = new Info()
                {
                    title = "MGrpc",
                    version = "v1",
                    description = "This is the API-Interface for MGrpc",
                    termsOfService = "By NMS",
                    contact = new Contact
                    {

                        name = "bacon",
                        email = "623808222@qq.com"
                    }
                },
                XmlDocumentPath = xmlPath
            });

            // 一个是 Http1-JSON to gRPC-MagicOnion gateway(MagicOnionHttpGateway)
            //channel 地址要和Grpc（Magiconion）的保持一致
            //要想让rpc成为该web服务的接口，流量和协议被统一到你写的这个web项目中来，那么就要用个方法链接你和rpc
            //这个web项目承接你的请求，然后web去调用rpc获取结果，再返回给你。因此需要下面这句话
            //这个中间件从源码中看出主要是为了协议转换和messagepack与json互转
            app.UseMagicOnionHttpGateway(magicOnion.MethodHandlers, new Channel("localhost:123456", ChannelCredentials.Insecure));
        }
    }
}
