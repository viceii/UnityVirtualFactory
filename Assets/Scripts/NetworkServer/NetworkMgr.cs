using CatFM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkMgr : MonoSingleton<NetworkMgr>
{
    [SerializeField] private int port = 8888;


    private NetworkServer networkServer;

    private void Awake()
    {
        networkServer = new NetworkServer(port);
        networkServer.Connect();

        MsgCenter.AddListener<int, int>(MsgDefine.NET_SEND_ROBOTSTATE, SendRobotState);
    }

    public void SendRobotState(int index, int state)
    {
        RobotState robotState = new RobotState();
        robotState.Index = index;
        robotState.State = state;
        networkServer.Send(ProtocolDefine.UpdateRobotState, robotState);
        Debug.LogFormat("发送机械臂：{0}状态信息：{1}", index, state);
    }
}
