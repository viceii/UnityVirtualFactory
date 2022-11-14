using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonitorPanel : BasePanel
{
    private Button[] monitors;

    private void Awake()
    {
        this.monitors = GetComponentsInChildren<Button>();
        int size = this.monitors.Length;
        for (int i = 0; i < size; i++)
        {
            AddListener(this.monitors[i], i);
        }
    }

    private int run = 0;
    private void AddListener(Button button, int index)
    {
        button.onClick.AddListener(delegate
        {
            MsgCenter.Call<int>(MsgDefine.SwitchCamera, index);
            run = 1 - run;
            NetworkMgr.Instance.SendRobotState(index, run);
            Factor.Instance.RunRobot(index);
        });
    }
}
