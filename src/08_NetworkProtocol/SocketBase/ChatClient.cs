using System;
using System.IO;
using System.Net.Sockets;

namespace SocketBase
{
    /// <summary>
    /// 聊天客户端
    /// </summary>
    public class ChatClient
    {
        public TcpClient client;
        public BinaryReader reader;
        public BinaryWriter writer;
        public NetworkStream stream;
        public string name;
        public long bytesLength;
        public ChatClient(TcpClient client)
        {
            this.client = client;
            stream = client.GetStream();
            reader = new BinaryReader(stream);
            writer = new BinaryWriter(stream);
        }
        public void closeClient()
        {
            client.Close();
            reader.Close();
            writer.Close();
        }
    }
}
