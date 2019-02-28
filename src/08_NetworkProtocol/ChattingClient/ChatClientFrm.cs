using SocketBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChattingClient
{
    public partial class ChatClientFrm : Form
    {
        public ChatClientFrm()
        {
            InitializeComponent();
        }

        #region properties
        private TcpClient client;
        private NetworkStream stream;
        private string loginName = string.Empty;
        private BinaryReader reader;
        private BinaryWriter writer;

        private int heartbeatCount = 5; //10s send once
        private int currentCount = 0;    //timer
        #endregion

        #region functions
        private void WriteToStream(ChatMessageEntity messageEntity)
        {
            ObjectInversion inversion = new ObjectInversion();
            byte[] byteArr = inversion.SerializeTo((object)messageEntity);
            writer.Write(byteArr);
            writer.Flush();
        }
        private void AnalysisPackage(ChatMessageEntity entity, ChatClient user)
        {
            switch (entity.MessageType)
            {
                case ChatStateEnum.OnLine:
                    if (entity.MessageContentEx.Count > 0)
                    {
                        lstUsers.Invoke(new Action(() => { lstUsers.Items.Clear(); }));
                        foreach (KeyValuePair<EndPoint, string> kvp in entity.MessageContentEx as Dictionary<EndPoint, string>)
                        {
                            ListViewItem lvi = new ListViewItem();
                            lvi.Text = kvp.Value;
                            lvi.Tag = kvp.Key as object;
                            lstUsers.Invoke(new Action(() => { lstUsers.Items.Add(lvi); }));
                        }
                    }
                    break;
                case ChatStateEnum.Message:
                    txtMessage.Append(entity.NickName + "【" + entity.RemoteEndPoint + "】 say:\n" + entity.MessageContent);
                    break;
                case ChatStateEnum.File:
                    break;
                case ChatStateEnum.OffLine:
                    foreach (var offlineUser in entity.MessageContentEx)
                    {
                        txtMessage.Append("用户已经下线：" + offlineUser.Key);

                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = offlineUser.Value;
                        lvi.Tag = offlineUser.Key as object;
                        lstUsers.Invoke(new Action(() => { lstUsers.Items.Remove(lvi); }));
                    }
                    break;
                case ChatStateEnum.Heartbeat:
                    break;
                case ChatStateEnum.CheckOffLine:
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region events
        private void ChatClientFrm_Load(object sender, EventArgs e)
        {
            LoginFrm loginFrm = new LoginFrm();
            loginFrm.LoginAction = (name) => { loginName = name; this.Text = "客户端" + name; };
            loginFrm.ShowDialog();
            
        }
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                client = new TcpClient(Dns.GetHostName(), ChatUtil.Port);
                txtMessage.Append("Successfully connected to Server side.");
            }
            catch (SocketException socketEx)
            {
                txtMessage.Append("Failed to connect server：" + socketEx.Data);
                return;
            }
            stream = client.GetStream();
            writer = new BinaryWriter(stream);
            ChatMessageEntity messageEntity = new ChatMessageEntity();
            messageEntity.NickName = loginName;
            messageEntity.MessageType = ChatStateEnum.OnLine;
            messageEntity.MessageContent = string.Empty;
            messageEntity.RemoteEndPoint = client.Client.LocalEndPoint;
            this.Text += " 【" + client.Client.LocalEndPoint + "】";
            messageEntity.timeStamp = DateTime.Now;

            WriteToStream(messageEntity);


            heartbeatTimer.Enabled = true;
            heartbeatTimer.Start();

            Task.Run(() => 
            {
                reader = new BinaryReader(stream);
                ObjectInversion inversion = new ObjectInversion();
                int bufferLen = client.ReceiveBufferSize;
                byte[] recvBytes = new byte[bufferLen];
                while (true)
                {
                    reader.Read(recvBytes, 0, bufferLen);
                    ChatMessageEntity entity = inversion.DeSerializeTo(recvBytes) as ChatMessageEntity;
                    AnalysisPackage(entity, null);
                }
            });
        }
        private void heartbeatTimer_Tick(object sender, EventArgs e)
        {
            currentCount++;
            if (currentCount == heartbeatCount)
            {
                txtMessage.Append("开始发送心跳包");
                ChatMessageEntity chat = new ChatMessageEntity();
                chat.MessageType = ChatStateEnum.Heartbeat;
                chat.timeStamp = DateTime.Now;
                chat.NickName = loginName;

                WriteToStream(chat);
                currentCount = 0;
            }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            ChatMessageEntity messageEntity = new ChatMessageEntity();
            messageEntity.NickName = loginName;
            messageEntity.MessageType = ChatStateEnum.Message;
            messageEntity.MessageContent = txtSend.Text;
            messageEntity.RemoteEndPoint = client.Client.LocalEndPoint;
            messageEntity.timeStamp = DateTime.Now;

            if (stream.CanWrite)
                WriteToStream(messageEntity);
        }
        #endregion
    }
}
