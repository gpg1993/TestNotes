using System;
using System.Reflection.Emit;
using System.Threading.Tasks;
using WebSocketSharp;

namespace WebSocketSharpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            WebSocketRun();
        }

        public static async Task WebSocketRun()
        {
            var ws = new WebSocket("ws://dragonsnest.far/Laputa");
            ws.OnMessage += Ws_OnMessage;
            ws.OnError += Ws_OnError;
            ws.OnClose += Ws_OnClose;
            ws.Connect();
            ws.Send("Hi,nihao!");
            Console.ReadKey();
            ws.Close();
        }

        private static void Ws_OnClose(object sender, CloseEventArgs e)
        {
            Console.WriteLine($"client:{e.Code}");
        }

        private static void Ws_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"client:{e.Message}");
        }

        private static void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.IsBinary)//判读数据类型e.IsText,e.IsPing
            {

            }
            Console.WriteLine($"client:{e.Data}");
        }
    }
}
