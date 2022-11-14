using Google.Protobuf;
using System;
using System.Collections.Generic;

namespace CatFM.Net
{
    /// <summary>
    /// 包协议解析类
    /// </summary>
    public class PacketSerializer
    {
        /// <summary>
        /// 解析协议委托
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public delegate IMessage PraseProtocol(byte[] buffer);

        /// <summary>
        /// 协议字典
        /// </summary>
        private static Dictionary<int, PraseProtocol> protocolMap = new Dictionary<int, PraseProtocol>();
        private const int PROTOCOL_SIZE = 4;

        public static void RegisterProtocol(int protocol, PraseProtocol praseProtocol)
        {
            if (protocolMap == null)
            {
                protocolMap = new Dictionary<int, PraseProtocol>();
            }
            if (protocolMap.ContainsKey(protocol))
            {
                protocolMap[protocol] = praseProtocol;
            }
            else
            {
                protocolMap.Add(protocol, praseProtocol);
            }
        }

        /// <summary>
        /// 解析协议
        /// </summary>
        /// <param name="buffer">缓冲区</param>
        /// <param name="start">协议包起始下标</param>
        /// <param name="len">协议包长度</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static KeyValuePair<ProtocolDefine, IMessage> Decode(byte[] buffer, int start, int len)
        {
            // 解析协议号
            byte[] id = new byte[PROTOCOL_SIZE];
            Array.Copy(buffer, start, id, 0, PROTOCOL_SIZE);
            int protocolID = BitConverter.ToInt32(id, 0);
            // 检查协议号是否存在
            if (!protocolMap.ContainsKey(protocolID))
            {
                throw new Exception(string.Format("Protocol ID {0} is not existed in Protocol Map", protocolID));
            }
            Console.WriteLine("接收到{0}类型消息", (ProtocolDefine)protocolID);
            // 读取协议
            byte[] protocolBuf = new byte[len - PROTOCOL_SIZE];
            Array.Copy(buffer, start + PROTOCOL_SIZE, protocolBuf, 0, len - PROTOCOL_SIZE);
            IMessage message = protocolMap[protocolID].Invoke(protocolBuf);

            return new KeyValuePair<ProtocolDefine, IMessage>((ProtocolDefine)protocolID, message);
        }
    }
}