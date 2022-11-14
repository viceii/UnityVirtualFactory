using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum MsgDefine
{
    ProcessEnd,
    ProcessOneEnd,
    ProcessTwoEnd,
    ProcessThreeEnd,
    Transport,
    TakeOneEnd,
    StartPrint,
    PlaceToPack,
    RunRobot,
    StartHandle,
    PackEnd,
    SinglePrintEnd,
    PlaceAnimOneEnd,
    PlaceAnimTwoEnd,
    StartPack,
    PackAnimOneEnd,
    PackAnimTwoEnd,
    SwitchCamera,

    NET_SEND_ROBOTSTATE,
    ProcessLineFinished,
}

public delegate void CallBack();
public delegate void CallBack<T>(T arg);
public delegate void CallBack<T1, T2>(T1 arg1, T2 arg2);
public delegate void CallBack<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);
public delegate void CallBack<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);
public delegate void CallBack<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);