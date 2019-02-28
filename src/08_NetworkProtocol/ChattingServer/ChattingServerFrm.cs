using SocketBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChattingServer
{
    public partial class ChattingServerFrm : Form
    {
        public ChattingServerFrm()
        {
            InitializeComponent();
        }

        #region properties
        private TcpListener serverListener;  //客户监听器
        private Dictionary<EndPoint, string> ChatClientCollection;
        private AutoResetEvent resetEvent;
        private AutoResetEvent resetEventForListener;
        private List<ChatClient> ChatClientLists;

        private int tickCount = 10; //1分钟检测一次
        private int tickCountInStep = 0;      //跑秒记录

        private int expiryCount = 3; //超过3次不回应，视为掉线
        private int expiryCountInStep = 0; //次数记录
        private Dictionary<EndPoint, List<int>> heartbeatContainer;
        private List<int> heartbeatList;

        private Dictionary<EndPoint, string> offLineChatClients;  //记录掉线的用户

        private AutoResetEvent syncHeartbeatSend;

        //心跳包发送给客户端后，如果客户端有应答，那么下面这个集合中的value将一直保持为0的状态，否则就累加，一旦累加大于等于3次，则视为掉线。
        private Dictionary<ChatClient, int> ChatClientOnLineCounter;   //
        #endregion

        #region Functions
        private void StartListen()
        {
            try
            {
                //start to listen
                serverListener.Start();
            }
            catch (SocketException soe) { throw new Exception(soe.Message); }
            catch (InvalidOperationException ioe) { throw new Exception(ioe.Message); }
        }
        private void ChatClientList(ChatClient ChatClient)
        {
            if (!ChatClientLists.Contains(ChatClient))
                ChatClientLists.Add(ChatClient);
        }
        private void ChatClientJoin(EndPoint endPoint, string nickname)
        {
            if (!ChatClientCollection.ContainsKey(endPoint))
            {
                ChatClientCollection.Add(endPoint, nickname);
                //add to ChatClientList
                ListViewItem lvi = new ListViewItem();
                lvi.Text = nickname;
                lvi.Tag = endPoint;
                lstOnLineUser.Invoke(new Action(() => { lstOnLineUser.Items.Add(lvi); }));
            }
        }
        private void BroadCastChatClients()
        {
            ChatMessageEntity entity = new ChatMessageEntity();
            entity.MessageType = ChatStateEnum.OnLine;
            entity.MessageContentEx = ChatClientCollection;

            ObjectInversion inversion = new ObjectInversion();
            byte[] byteArr = inversion.SerializeTo((object)entity);

            foreach (ChatClient ChatClient in ChatClientLists)
            {
                ChatClient.writer.Write(byteArr);
                ChatClient.writer.Flush();
            }
        }



        #region 接收客户端发来的信息
        private void ReceiveClientsThread(object ChatClient)
        {
            Thread t = new Thread(new ParameterizedThreadStart(ReceiveClients));
            t.IsBackground = true;
            t.Start(ChatClient);
        }

        private void ReceiveClients(object ChatClient)
        {
            ChatClient u = ChatClient as ChatClient;
            ObjectInversion inversion = new ObjectInversion();
            byte[] recvBytes = new byte[u.client.ReceiveBufferSize];
            while (true)  //注意，这里需要加个循环，否则之后client发送的数据无法接收
            {
                try
                {
                    u.stream.Read(recvBytes, 0, recvBytes.Length); //sync mode, 为什么一切换到async mode,就会发生乱序
                    ChatMessageEntity entity = inversion.DeSerializeTo(recvBytes) as ChatMessageEntity;
                    AnalysisPackage(entity, u);

                    //清空数组，以免脏读
                    recvBytes = new byte[u.client.ReceiveBufferSize];
                }
                catch { }
            }
        }

        private void AnalysisPackage(ChatMessageEntity entity, ChatClient ChatClient)
        {
            switch (entity.MessageType)
            {
                case ChatStateEnum.OnLine:
                    txtLog.Append(entity.NickName + "【" + entity.RemoteEndPoint + "】 has connected.");
                    ChatClient.name = entity.NickName;                                     //将名字做保存
                    ChatClientList(ChatClient);                                                                //将用户添加到上线列表
                    ChatClientJoin(entity.RemoteEndPoint, entity.NickName);  //将用户添加到UI中
                    ChatClientOnLineCounter.Add(ChatClient, 0);                                   //开始启动计时器，检测心跳包
                    BroadCastChatClients();                                                           //广播用户上线信息
                    break;
                case ChatStateEnum.Message:
                    txtLog.Append(entity.NickName + "【" + entity.RemoteEndPoint + "】 say:\n" + entity.MessageContent);
                    break;
                case ChatStateEnum.OffLine:

                    break;
                case ChatStateEnum.Heartbeat:
                    //syncHeartbeatSend.Set();
                    txtLog.Append("收到客户端" + entity.NickName + "的心跳回应包.");
                    if (ChatClientOnLineCounter.ContainsKey(ChatClient))
                    {
                        ChatClientOnLineCounter[ChatClient] = 0;
                    }
                    else
                    {
                        ChatClientOnLineCounter.Add(ChatClient, 0);
                    }
                    break;
                default: break;
            }
        }
        #endregion

        #region 接收客户端的连接
        private void AcceptClientsThread()
        {
            Thread thread = new Thread(new ThreadStart(AcceptClients));
            thread.IsBackground = true;
            thread.Start();
        }

        private void AcceptClients()
        {
            while (true)
            {
                TcpClient client = null;
                serverListener.BeginAcceptTcpClient(new AsyncCallback((iar) =>
                {
                    TcpListener listenerAsync = (TcpListener)iar.AsyncState;
                    client = listenerAsync.EndAcceptTcpClient(iar);
                    resetEventForListener.Set();
                }), serverListener);

                resetEventForListener.WaitOne();
                ChatClient ChatClient = new ChatClient(client);
                //ChatClient.writer.Write(messageTest);
                // ChatClient.writer.Flush();

                //start a new thread to receive client message.
                ReceiveClientsThread((object)ChatClient);
            }
        }
        #endregion
        #endregion

        #region event
        /**
        * 流程就是： Server开始监听--》客户端上线--》server发消息说，Send me your name pls!-->客户端把名字发过来。
        * */
        //Init
        private void ChattingServerFrm_Load(object sender, EventArgs e)
        {
            ChatClientCollection = new Dictionary<EndPoint, string>();
            serverListener = new TcpListener(ChatUtil.GetLocalIPEndPoint());
            resetEvent = new AutoResetEvent(false);
            resetEventForListener = new AutoResetEvent(false);
            ChatClientLists = new List<ChatClient>();

            heartbeatContainer = new Dictionary<EndPoint, List<int>>();
            heartbeatList = new List<int>();
            offLineChatClients = new Dictionary<EndPoint, string>();

            heartbeatTimer.Enabled = true;
            syncHeartbeatSend = new AutoResetEvent(false);

            ChatClientOnLineCounter = new Dictionary<ChatClient, int>();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            StartListen();
            txtLog.Append("Server starts to listen.");
            //open new child thread to deal with accepted clients.
            AcceptClientsThread();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {

        }
        bool flag = false;
        private void heartbeatTimer_Tick(object sender, EventArgs e)
        {
            tickCountInStep++;
            if (tickCountInStep == tickCount)
            {
                if (ChatClientCollection.Count > 0)
                {
                    //计数器自动递增
                    expiryCountInStep++;
                    foreach (ChatClient ChatClient in ChatClientLists)
                    {
                        ChatClientOnLineCounter[ChatClient]++;
                    }
                    //连续监测三次之后，开始监测集合中的掉线情况
                    if (expiryCountInStep == expiryCount)
                    {
                        //寻找集合中“掉线”的用户
                        var disconnectedChatClients = ChatClientOnLineCounter.Where(p => p.Value >= 3).ToList();
                        foreach (var disconnectedChatClient in disconnectedChatClients)
                        {
                            txtLog.Append("用户" + disconnectedChatClient.Key.name + "掉线！");

                            //删除集合中被视为掉线的用户
                            ChatClientLists.Remove(disconnectedChatClient.Key);
                            ChatClientOnLineCounter.Remove(disconnectedChatClient.Key);

                            //开始广播发送掉线用户
                            ChatMessageEntity entity = new ChatMessageEntity();
                            entity.MessageType = ChatStateEnum.OffLine;
                            EndPoint curOfflineChatClientEP = disconnectedChatClient.Key.client.Client.RemoteEndPoint;
                            string ChatClientName = disconnectedChatClient.Key.name;
                            entity.MessageContentEx.Add(curOfflineChatClientEP, ChatClientName);

                            ObjectInversion inversion = new ObjectInversion();
                            byte[] byteArr = inversion.SerializeTo((object)entity);

                            try
                            {
                                foreach (ChatClient ChatClient in ChatClientLists)
                                {
                                    ChatClient.writer.Write(byteArr);
                                    ChatClient.writer.Flush();
                                }
                            }
                            catch { }
                        }
                        expiryCountInStep = 0;
                    }
                }
                tickCountInStep = 0;
            }
        }
        #endregion

    }
}
