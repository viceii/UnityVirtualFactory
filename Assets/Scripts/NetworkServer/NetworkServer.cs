using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using Google.Protobuf;

namespace CatFM
{
    public class NetworkServer
    {
        private Socket server;
        private IPEndPoint iPEndPoint;
        private int maxAcceptAmount = 1000;

        private Dictionary<string, Client> clientMap = new Dictionary<string, Client>();

        public NetworkServer(int port)
        {
            this.iPEndPoint = new IPEndPoint(IPAddress.Any, port);
        }

        public void Connect()
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            server.Bind(this.iPEndPoint);
            Debug.LogFormat("服务器创建成功，IP：{0}，Port：{1}", iPEndPoint.Address, iPEndPoint.Port);
            server.Listen(maxAcceptAmount);
            Debug.Log("开始监听");
            server.BeginAccept(OnAccepted, server);
        }

        private void OnAccepted(IAsyncResult ar)
        {
            try
            {
                Socket socket = server.EndAccept(ar);
                EndPoint clientEndPort = socket.RemoteEndPoint;
                Debug.LogFormat("客户端：{0}请求连接", clientEndPort);
                Client client = new Client(this, socket);
                clientMap.Add(clientEndPort.ToString(), client);
                server.BeginAccept(OnAccepted, server);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Send(ProtocolDefine protocol, IMessage message)
        {
            byte[] data = Pack(protocol, message);
            foreach(Client client in clientMap.Values)
            {
                client.Send(data);
            }
        }

        /// <summary>
        /// 打包协议
        /// 添加包长度、协议号
        /// </summary>
        /// <param name="protocol"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private byte[] Pack(ProtocolDefine protocol, IMessage message)
        {
            List<byte> buffer = new List<byte>();
            byte[] id = BitConverter.GetBytes((int)protocol);
            byte[] proto = message.ToByteArray();
            byte[] length = BitConverter.GetBytes(id.Length + proto.Length);
            buffer.AddRange(length);
            buffer.AddRange(id);
            buffer.AddRange(proto);
            return buffer.ToArray();
        }
    }


}
