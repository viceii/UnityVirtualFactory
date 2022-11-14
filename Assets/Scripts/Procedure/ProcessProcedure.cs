using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using SampleFrame;

public class ProcessProcedure : MonoBehaviour
{

    [SerializeField] private int id = 0;
    /// <summary>
    /// 加工触发点
    /// </summary>
    public CollisionHandler handler;

    /// <summary>
    /// 机械臂
    /// </summary>
    public ProcessRobot robot;

    /// <summary>
    /// 传送到油墨打印流程
    /// </summary>
    public TransportProcedure transportProcedure;

    /// <summary>
    /// 加工次数
    /// </summary>
    [SerializeField]
    private int processCount = 1;

    /// <summary>
    /// 最大一次加工次数
    /// </summary>
    [SerializeField]
    private int maxProcessCount = 10;

    private Board board;


    public int Id { get => id; set => id = value; }
    public int ProcessCount
    {
        get => processCount;
        set
        {
            processCount = Mathf.Clamp(value, 0, maxProcessCount);
        }
    }


    private void Awake()
    {
        handler.onEnter += OnStartProcess;
        MsgCenter.AddListener<int>(MsgDefine.ProcessEnd, OnProcessEnd);
        MsgCenter.AddListener<int>(MsgDefine.ProcessOneEnd, OnProcessOneEnd);
        MsgCenter.AddListener<int>(MsgDefine.ProcessTwoEnd, OnProcessTwoEnd);
        MsgCenter.AddListener<int>(MsgDefine.ProcessThreeEnd, OnProcessThreeEnd);
    }

    private Transform[] children;
    private int currIndex = 0;
    /// <summary>
    /// 开始一次加工
    /// </summary>
    /// <param name="collider"></param>
    public void OnStartProcess(Collider collider)
    {
        if (collider.gameObject.layer != LayerMask.NameToLayer(LayerID.Board.ToString()))
            return;
        this.board = collider.GetComponent<Board>();
        this.children = this.board.transform.Find("Line059").GetComponentsInChildren<Transform>(true);
        for (int i = 1; i < children.Length; ++i)
        {
            children[i].gameObject.SetActive(false);
        }
        this.board.Amount = processCount;
        this.board.Id = this.Id;
        this.currIndex = 0;
        robot.StartProcess();
    }

    /// <summary>
    /// 一次加工结束
    /// </summary>
    /// <param name="id"></param>
    private void OnProcessEnd(int id)
    {
        if (this.Id != id)
            return;
        this.children[currIndex + 1].gameObject.SetActive(true);
        ++this.currIndex;
        robot.EndProcess();
        if (currIndex < processCount)
        {
            robot.StartProcess();
        }
        else
        {
            transportProcedure.StartTransport(this.board.transform);
            MsgCenter.Call<int>(MsgDefine.ProcessLineFinished, id);
        }
    }

    /// <summary>
    /// 加工一、二、三阶段结束
    /// </summary>
    /// <param name="id"></param>
    private void OnProcessOneEnd(int id)
    {
        if (this.Id != id)
            return;
        robot.EndProcessOne();
    }
    private void OnProcessTwoEnd(int id)
    {
        if (this.Id != id)
            return;
        robot.EndProcessTwo();
    }
    private void OnProcessThreeEnd(int id)
    {
        if (this.Id != id)
            return;
        robot.EndProcessThree();
    }
}
