using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebSocketSharpServer
{
    class Program
    {
        public class Laputa : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs e)
            {
                var msg = e.Data == "BALUS"
                          ? "I've been balused already..."
                          : "I'm not available now.";

                Send(msg);
            }
        }
        static void Main(string[] args)
        {
            var wssv = new WebSocketServer("ws://dragonsnest.far");
            wssv.AddWebSocketService<Laputa>("/Laputa");
            wssv.Start();
            
            Console.ReadKey(true);
            wssv.Stop();
        }
    }
}
