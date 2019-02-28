using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SocketBase
{
    /// <summary>
    /// 聊天消息实体
    /// </summary>
    [Serializable]
    public class ChatMessageEntity
    {
        public string NickName { get; set; } //User Nick Name
        public ChatStateEnum MessageType { get; set; }  //Communication type
        public string MessageContent { get; set; }  //Message Content
        public Dictionary<EndPoint, string> MessageContentEx = new Dictionary<EndPoint, string>(); //Message content for broadcasting user list
        public DateTime timeStamp { get; set; }  //time stamp
        public EndPoint RemoteEndPoint { get; set; } //remote end point
    }
}
