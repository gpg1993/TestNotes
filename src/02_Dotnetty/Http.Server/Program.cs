using DotNetty.Codecs.Http;
using DotNetty.Transport.Bootstrapping;
using DotNetty.Transport.Channels;
using DotNetty.Transport.Channels.Sockets;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Http.Server
{
    class Program
    {
        static async Task Main(string[] args)
        {
            IEventLoopGroup bossGroup = new MultithreadEventLoopGroup(1);
            IEventLoopGroup workGroup = new MultithreadEventLoopGroup();
            try
            {
                var bootstrap = new ServerBootstrap();
                bootstrap.Group(bossGroup, workGroup);
                bootstrap.Channel<TcpServerSocketChannel>()
                    .Option(ChannelOption.SoBacklog, 8191)
                    .ChildHandler(new ActionChannelInitializer<IChannel>(channel =>
                    {
                        IChannelPipeline pipeline = channel.Pipeline;
                        pipeline.AddLast(new HttpServerCodec());
                        pipeline.AddLast(new HttpObjectAggregator(65536));

                    }));
                IChannel bootstrapChannel = await bootstrap.BindAsync(IPAddress.IPv6Any, 8881);

                await bootstrapChannel.CloseAsync();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                bossGroup.ShutdownGracefullyAsync().Wait();
            }
        }
    }
}
