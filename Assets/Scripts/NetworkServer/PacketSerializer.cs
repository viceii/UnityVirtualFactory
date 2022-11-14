using Google.Protobuf;
using System;
using System.Collections.Generic;

namespace CatFM.Net
{
    /// <summary>
    /// ��Э�������
    /// </summary>
    public class PacketSerializer
    {
        /// <summary>
        /// ����Э��ί��
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public delegate IMessage PraseProtocol(byte[] buffer);

        /// <summary>
        /// Э���ֵ�
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
        /// ����Э��
        /// </summary>
        /// <param name="buffer">������</param>
        /// <param name="start">Э�����ʼ�±�</param>
        /// <param name="len">Э�������</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static KeyValuePair<ProtocolDefine, IMessage> Decode(byte[] buffer, int start, int len)
        {
            // ����Э���
            byte[] id = new byte[PROTOCOL_SIZE];
            Array.Copy(buffer, start, id, 0, PROTOCOL_SIZE);
            int protocolID = BitConverter.ToInt32(id, 0);
            // ���Э����Ƿ����
            if (!protocolMap.ContainsKey(protocolID))
            {
                throw new Exception(string.Format("Protocol ID {0} is not existed in Protocol Map", protocolID));
            }
            Console.WriteLine("���յ�{0}������Ϣ", (ProtocolDefine)protocolID);
            // ��ȡЭ��
            byte[] protocolBuf = new byte[len - PROTOCOL_SIZE];
            Array.Copy(buffer, start + PROTOCOL_SIZE, protocolBuf, 0, len - PROTOCOL_SIZE);
            IMessage message = protocolMap[protocolID].Invoke(protocolBuf);

            return new KeyValuePair<ProtocolDefine, IMessage>((ProtocolDefine)protocolID, message);
        }
    }
}