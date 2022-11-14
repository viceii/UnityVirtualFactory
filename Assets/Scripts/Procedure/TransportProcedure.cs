using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class TransportProcedure : MonoBehaviour
{
    [SerializeField] private List<Transform> nodes = new List<Transform>();

    public UnityEvent<Transform> arriveFinalNodeCallBack;

    private Conveyor conveyor;
    private int currIndex = 0;


    private void Awake()
    {
        this.conveyor = Conveyor.Instance;
    }



    /// <summary>
    /// 开始传送
    /// </summary>
    /// <param name="go"></param>
    public void StartTransport(Transform go)
    {
        this.currIndex = 0;
        this.conveyor.Transport(go, this.nodes[this.currIndex].transform.position, ArriveNextNodeCallBack);
    }

    /// <summary>
    /// 达到下一个传送点
    /// </summary>
    /// <param name="go"></param>
    private void ArriveNextNodeCallBack(Transform go)
    {
        if (++this.currIndex >= this.nodes.Count)
        {
            if (arriveFinalNodeCallBack != null)
            {
                arriveFinalNodeCallBack.Invoke(go);
            }
            return;
        }
        this.conveyor.Transport(go, this.nodes[this.currIndex].transform.position, ArriveNextNodeCallBack);
    }
}
