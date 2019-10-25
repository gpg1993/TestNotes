using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using MagicOnion.Hosting;
using Microsoft.Extensions.Logging;
using Grpc.Core;
using MagicOnion.Server;
using MagicOnion.Server.Hubs;
using MessagePack;
using Microsoft.Extensions.DependencyInjection;
using System;
using Grpc.Core.Logging;
using System.IO;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using Bacon.Service1.Util;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Reflection;
using MagicOnion;
using MagicOnion.HttpGateway.Swagger;
using AutoMapper;
using Bacon.Service1.Data;
using AutoMapper.Configuration;
using System.ComponentModel;

namespace Bacon.Service
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //添加 json 文件路径
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            //创建配置根对象
            var Configuration = builder.Build();

            //Environment.SetEnvironmentVariable("GRPC_VERBOSITY", "DEBUG");
            //Environment.SetEnvironmentVariable("GRPC_TRACE", "all");

            Environment.SetEnvironmentVariable("SETTINGS_MAX_HEADER_LIST_SIZE", "1000000");

            GrpcEnvironment.SetLogger(new ConsoleLogger());

            var options = new MagicOnionOptions(true)
            {
                MagicOnionLogger = new MagicOnionLogToGrpcLoggerWithNamedDataDump(),
                GlobalFilters = new MagicOnionFilterAttribute[]
                {
                },
                EnableCurrentContext          = true,
                DefaultGroupRepositoryFactory = new ConcurrentDictionaryGroupRepositoryFactory(),
                DisableEmbeddedService        = true,
            };
            var conn = ConnectionMultiplexer.Connect(Configuration["RedisConn"]);
            options.ServiceLocator.Register(conn);
            options.ServiceLocator.Register(MessagePackSerializer.DefaultResolver);
            options.ServiceLocator.Register<IMagicOnionLogger>(new NullMagicOnionLogger());

            //IServiceCollection services = new ServiceCollection();
            //services.AddTransient<ISomeService>(sp => new FooService(5));
            //services.AddAutoMapper(typeof(Source));
            //var                provider = services.BuildServiceProvider();
            //using (var scope = provider.CreateScope())
            //{
            //    var mapper = scope.ServiceProvider.GetService<IMapper>();

            //    foreach (var typeMap in mapper.ConfigurationProvider.GetAllTypeMaps())
            //    {
            //        Console.WriteLine($"{typeMap.SourceType.Name} -> {typeMap.DestinationType.Name}");
            //    }

            //    foreach (var service in services)
            //    {
            //        Console.WriteLine(service.ServiceType + " - " + service.ImplementationType);
            //    }

            //    var dest = mapper.Map<Dest2>(new Source2());
            //    Console.WriteLine(dest.ResolvedValue);
            //}

            var magicOnionHost = MagicOnionHost.CreateDefaultBuilder(useSimpleConsoleLogger: true)
                .ConfigureServices((ctx, services) =>
                {
                    services.Configure<MongoAppSetting>(x =>
                    {
                        x.MongoConn       = Configuration["MongoConn"].ToString();
                        x.MongoDbDatabase = Configuration["MongoDbDatabase"].ToString();
                    });
                    services.AddAutoMapper((collect, cfg) => { cfg.AddProfile<MappingProfile>(); },
                        AppDomain.CurrentDomain.GetAssemblies());
                    services.Add(new ServiceDescriptor(typeof(IMapper),
                        sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService),
                        ServiceLifetime.Transient));
                    services.AddSingleton<MongoDbContext>();
                    services.AddScoped<IUserRepository, UserRepository>();
                })
                .UseMagicOnion(options,
                    new ServerPort(Configuration["GrpcService:IP"], Convert.ToInt32(Configuration["GrpcService:Port"]),
                        ServerCredentials.Insecure))
                .UseConsoleLifetime()
                .Build();

            var webHost = new WebHostBuilder()
                .ConfigureServices(collection =>
                {
                    // Add MagicOnionServiceDefinition for reference from Startup.
                    collection.AddSingleton<MagicOnionServiceDefinition>(magicOnionHost.Services
                        .GetService<MagicOnionServiceDefinition>());
                })
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://localhost:5432")
                .Build();

            await Task.WhenAll(webHost.RunAsync(), magicOnionHost.RunAsync());
        }
    }

    public class Startup
    {
        // Inject MagicOnionServiceDefinition from DIl
        public void Configure(IApplicationBuilder app, MagicOnionServiceDefinition magicOnion)
        {
            // Optional:Summary to Swagger
            var xmlName = "Sandbox.NetCoreServer.xml";
            var xmlPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), xmlName);

            // HttpGateway has two middlewares.
            // One is SwaggerView(MagicOnionSwaggerMiddleware)
            // One is Http1-JSON to gRPC-MagicOnion gateway(MagicOnionHttpGateway)
            app.UseMagicOnionSwagger(magicOnion.MethodHandlers,
                new SwaggerOptions("MagicOnion.Server", "Swagger Integration Test", "/")
                {
                    //XmlDocumentPath = xmlPath
                });
            app.UseMagicOnionHttpGateway(magicOnion.MethodHandlers,
                new Channel("localhost:12345", ChannelCredentials.Insecure));
        }
    }

    public class Source
    {
    }

    public class Dest
    {
    }

    public class Source2
    {
    }

    public class Dest2
    {
        public int ResolvedValue { get; set; }
    }

    public class Profile1 : Profile
    {
        public Profile1()
        {
            CreateMap<Source, Dest>();
        }
    }

    public class Profile2 : Profile
    {
        public Profile2()
        {
            CreateMap<Source2, Dest2>()
                .ForMember(d => d.ResolvedValue, opt => opt.MapFrom<DependencyResolver>());
        }
    }

    public class DependencyResolver : IValueResolver<object, object, int>
    {
        private readonly ISomeService _service;

        public DependencyResolver(ISomeService service)
        {
            _service = service;
        }

        public int Resolve(object source, object destination, int destMember, ResolutionContext context)
        {
            return _service.Modify(destMember);
        }
    }

    public interface ISomeService
    {
        int Modify(int value);
    }

    public class FooService : ISomeService
    {
        private readonly int _value;

        public FooService(int value)
        {
            _value = value;
        }

        public int Modify(int value) => value + _value;

    }
}
