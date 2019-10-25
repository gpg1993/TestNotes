using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Ping ping = new Ping();
            ping.age = "12";
            ping.name = "sun";
            var mediator = BuildMediator();
            var msg = await mediator.Send<ResponseMsg>(ping);
            Console.WriteLine(msg.nameAge);
            HelloWorld helloWorld = new HelloWorld() { name = "green" };
            await mediator.Publish<HelloWorld>(helloWorld);
            //await mediator.Publish(helloWorld);
            Console.ReadKey();
        }
        private static IMediator BuildMediator()
        {
            var services = new ServiceCollection();
            services.AddMediatR(typeof(Ping));
            var provider = services.BuildServiceProvider();
            return provider.GetRequiredService<IMediator>();
        }
    }

    public class ResponseMsg
    {
        public string nameAge { get; set; }
    }
    public class Ping : IRequest<ResponseMsg> { 
        public string name { get; set; }
        public string age { get; set; }
    }
    public class PingHandler : RequestHandler<Ping, ResponseMsg>
    {
        protected override ResponseMsg Handle(Ping request)
        {
            ResponseMsg msg = new ResponseMsg();
            msg.nameAge = request.age + request.name;
            return msg;
        }
    }

    public class Ping1 : IRequest
    {
        public string name { get; set; }
        public string age { get; set; }
    }
    public class PingHandler1 : AsyncRequestHandler<Ping1>
    {
        protected async override Task Handle(Ping1 request, CancellationToken cancellationToken)
        {
            await Task.Delay(100).ConfigureAwait(false);
        }
    }

    public class HelloWorld : INotification
    {
        public string name { get; set; }
    }
    public class CNReply : INotificationHandler<HelloWorld>
    {
        public Task Handle(HelloWorld notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"CN Reply: Hello from CN");
            return Task.CompletedTask;
        }
    }
    public class USReply : INotificationHandler<HelloWorld>
    {
        public Task Handle(HelloWorld notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"US Reply: Hello from US");
            return Task.CompletedTask;
        }
    }
    public class MessageListener : INotificationHandler<INotification>
    {
        public Task Handle(INotification notification, CancellationToken cancellationToken)
        {
            notification.GetType();
            Console.WriteLine($"接收到新的消息：{notification.GetType()}");

            return Task.CompletedTask;
        }
    }
}
