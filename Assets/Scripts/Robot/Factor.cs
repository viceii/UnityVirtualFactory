using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Factor : MonoSingleton<Factor>
{
    [SerializeField]
    private List<ProcessRobot> processRobots;
    [SerializeField]
    private List<HandleRobot> handleRobots;
    [SerializeField]
    private List<PackRobot> packRobots;
    [SerializeField]
    private List<PrintRobot> printRobots;
    [SerializeField]
    private List<Board> boards;

    public List<ProcessRobot> ProcessRobots { get => processRobots; }
    public List<HandleRobot> HandleRobots { get => handleRobots; set => handleRobots = value; }
    public List<PackRobot> PackRobots { get => packRobots; set => packRobots = value; }
    public List<PrintRobot> PrintRobots { get => printRobots; set => printRobots = value; }
    public List<Board> Boards { get => boards; }

    private List<BaseRobot> robots = new List<BaseRobot>();

    private void Awake()
    {
        for (int i = 0; i < this.processRobots.Count; i++)
        {
            this.processRobots[i].Id = i;
            this.robots.Add(this.processRobots[i]);
        }

        for (int i = 0; i < this.handleRobots.Count; i++)
        {
            this.handleRobots[i].Id = i;
            this.robots.Add(this.handleRobots[i]);
        }

        for (int i = 0; i < this.printRobots.Count; i++)
        {
            this.printRobots[i].Id = i;
            this.robots.Add(this.printRobots[i]);
        }

        for (int i = 0; i < this.packRobots.Count; i++)
        {
            this.packRobots[i].Id = i;
            this.robots.Add(this.packRobots[i]);
        }

        for (int i = 0; i < this.boards.Count; i++)
        {
            this.boards[i].Id = i;
        }

        MsgCenter.AddListener<int>(MsgDefine.RunRobot, RunRobot);
    }

    private void OnDestroy()
    {
        MsgCenter.RemoveListener<int>(MsgDefine.RunRobot, RunRobot);
    }

    /// <summary>
    /// WebGL接口
    /// 控制整个流水线速度
    /// </summary>
    /// <param name="value"></param>
    public void SetProcessSpeed(float value)
    {
        Time.timeScale = value;
    }

    /// <summary>
    /// WebGL接口
    /// 重新加载工厂
    /// </summary>
    public void RestartFactor()
    {
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// 接收新订单
    /// </summary>
    /// <param name="count"></param>
    public void SetNewOrders(int count = 0)
    {

    }

    /// <summary>
    /// WebGL接口
    /// 选择观察视角
    /// </summary>
    /// <param name="index"></param>
    public void SwitchCamera(int index)
    {
        MsgCenter.Call<int>(MsgDefine.SwitchCamera, index);

    }

    /// <summary>
    /// WebGL接口
    /// 控制某个机械臂的运转和暂停
    /// </summary>
    /// <param name="index"></param>
    /// <param name="working"></param>
    public void RunRobot(int index)
    {
        if (index < 0 || index >= this.robots.Count)
            return;
        int state = 0;
        if (this.robots[index].IsWorking)
        {
            this.robots[index].Stop();
            state = 0;
        }
        else
        {
            this.robots[index].Running();
            state = 1;
        }
        MsgCenter.Call<int, int>(MsgDefine.NET_SEND_ROBOTSTATE, index, state);
    }

    /// <summary>
    /// WebGL接口
    /// 调式机械臂动画速度
    /// </summary>
    /// <param name="arr"></param>
    public void AdjustAnimSpeed(float[] arr)
    {
        if (arr.Length < 2)
            return;
        int index = (int)arr[0];
        float speed = arr[1];
        if (index < 0 || index >= this.robots.Count)
            return;
        this.robots[index].Animator.speed = speed;
    }
}
