using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class PackRobot : BaseRobot
{
    public GameObject boxUpperOne;
    public GameObject boxUpperTwo;
    public GameObject boxUpperThree;

    private void Start()
    {
        //MsgCenter.AddListener<int>(MsgDefine.TakeOneEnd, TakeOneEnd);
        MsgCenter.AddListener<int>(MsgDefine.PackAnimOneEnd, PackAnimOneEnd);
        MsgCenter.AddListener<int>(MsgDefine.PackAnimTwoEnd, PackAnimTwoEnd);
        MsgCenter.AddListener<int>(MsgDefine.StartPack, Pack);
        boxUpperTwo.SetActive(false);
        boxUpperThree.SetActive(false);
    }

    public void Pack(int id)
    {
        if (this.Id == id)
        {
            animator.SetTrigger("Pack");
        }
    }
    public void PackAnimOneEnd(int id)
    {
        if (this.Id == id)
        {
            boxUpperOne.SetActive(false);
            boxUpperTwo.SetActive(true);
        }
    }
    public void PackAnimTwoEnd(int id)
    {
        if (this.Id == id)
        {
            boxUpperTwo.SetActive(false);
            boxUpperThree.SetActive(true);
        }
    }
}
