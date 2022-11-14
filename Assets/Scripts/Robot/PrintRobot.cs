using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintRobot : BaseRobot
{
    // Start is called before the first frame update
    void Start()
    {
        MsgCenter.AddListener<int>(MsgDefine.StartPrint, StartPrint);
    }

    private void StartPrint(int id)
    {
        if (this.Id == id)
        {
            base.animator.SetTrigger("Print");
        }
    }
}
