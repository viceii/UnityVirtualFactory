using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using Google.Protobuf;
using SampleFrame;
using CatFM;
using CatFM.Net;

public class Client
{
    private NetworkServer server;
    /// <summary>
    /// 套接字对象
    /// </summary>
    private Socket socket;
    /// <summary>
    /// 接收缓冲区
    /// </summary>
    private byte[] buffer = new byte[Constant.MaxBufferSize];
    /// <summary>
    /// 已用缓冲区大小指针
    /// </summary>
    private int bufferCount = 0;
    private int packetLengthCount = 4;

    private Thread praseThread;

    /// <summary>
    /// 消息队列
    /// </summary>
    private Queue<KeyValuePair<ProtocolDefine, IMessage>> receivedQueue = new Queue<KeyValuePair<ProtocolDefine, IMessage>>();

    /// <summary>
    /// 应答回调字典
    /// </summary>
    private Dictionary<ProtocolDefine, Action<IMessage>> responseMap = new Dictionary<ProtocolDefine, Action<IMessage>>();


    public bool IsConnected { get => socket.Connected; }
    public Queue<KeyValuePair<ProtocolDefine, IMessage>> ReceivedQueue { get => receivedQueue; }

    public Client(NetworkServer server, Socket socket)
    {
        this.server = server;
        this.socket = socket;
        praseThread = new Thread(Update);
        praseThread.Start();
        Console.WriteLine("客户端{0}已连接", socket.RemoteEndPoint);
        socket.BeginReceive(buffer, 0, Constant.MaxBufferSize, SocketFlags.None, OnReceived, buffer);
    }

    private void Update()
    {
        while (true)
        {
            if (receivedQueue.Count > 0)
            {
                KeyValuePair<ProtocolDefine, IMessage> pair;
                lock (receivedQueue)
                {
                    pair = receivedQueue.Dequeue();
                }
                DispenseMsg(pair.Key, pair.Value);
            }
        }
    }

    /// <summary>
    /// 分发消息
    /// </summary>
    /// <param name="message"></param>
    private void DispenseMsg(ProtocolDefine protocol, IMessage message)
    {
        if (!responseMap.ContainsKey(protocol))
        {
            throw new Exception(String.Format("不存在{}协议的回调", protocol));
        }
        responseMap[protocol].Invoke(message);
    }

    private void OnReceived(IAsyncResult ar)
    {
        try
        {
            int size = socket.EndReceive(ar);
            bufferCount += size;

            PraseData();

            socket.BeginReceive(buffer, 0, Constant.MaxBufferSize, SocketFlags.None, OnReceived, buffer);
        }
        catch (Exception)
        {

        }
    }

    private void PraseData()
    {
        // 当前缓冲区大小小于包大小位大小
        if (bufferCount < packetLengthCount)
        {
            return;
        }
        // 读取包大小
        byte[] packetLengthBuf = new byte[packetLengthCount];
        Array.Copy(buffer, packetLengthBuf, packetLengthCount);
        int packetLength = BitConverter.ToInt32(packetLengthBuf, 0);
        // 当前包还未接收完
        if (bufferCount - 4 < packetLength)
        {
            return;
        }
        // 解析包
        KeyValuePair<ProtocolDefine, IMessage> msg = PacketSerializer.Decode(buffer, packetLengthCount, packetLength);
        // 消息入队列
        receivedQueue.Enqueue(msg);
        // 更新缓冲区
        bufferCount -= packetLength - packetLengthCount;
        Array.Copy(buffer, packetLengthCount + packetLength, buffer, 0, bufferCount);
        if (bufferCount > 0)
        {
            PraseData();
        }
    }

    public void Send(byte[] buffer)
    {
        if (IsConnected)
        {
            socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, OnSended, buffer);
        }
        else
        {
            Console.WriteLine("网络未连接");
        }
    }

    private void OnSended(IAsyncResult ar)
    {
        try
        {
            int count = socket.EndSend(ar);
            Console.WriteLine("向服务器成功发送大小为{0}的数据", count);
        }
        catch (Exception)
        {

        }
    }
}
