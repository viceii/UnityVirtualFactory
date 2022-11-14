using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WaitQueue : MonoBehaviour
{
    [SerializeField]
    private int id;
    /// <summary>
    /// 队列最大容积
    /// </summary>
    [SerializeField]
    private int capacity = 3;

    /// <summary>
    /// 队列是否填满
    /// </summary>
    private bool isFull = false;

    /// <summary>
    /// 当前容量
    /// </summary>
    private int size = 0;

    /// <summary>
    /// 队列结点
    /// </summary>
    [SerializeField]
    private List<CollisionHandler> nodes = new List<CollisionHandler>();


    [SerializeField]
    private WaitQueue nextQueue;

    /// <summary>
    /// 队列存储对象
    /// </summary>
    private List<Transform> children = new List<Transform>();


    private Conveyor conveyor;

    public int Id { get => id; }
    public int Capacity { get => capacity; set => capacity = value; }
    public bool IsFull { get => isFull; }

    private void Awake()
    {
        this.conveyor = Conveyor.Instance;
        this.nodes = new List<CollisionHandler>(transform.GetComponentsInChildren<CollisionHandler>());

        if (this.nodes.Count > 0)
        {
            this.nodes[0].onEnter += (collider) =>
            {
                Push(collider.transform);
            };

        }
    }

    /// <summary>
    /// 进队列
    /// </summary>
    /// <param name="go"></param>
    public void Push(Transform go)
    {
        if (this.size >= this.capacity)
        {
            this.conveyor.Transport(go, nextQueue.transform.position);
            return;
        }
        this.children.Add(go.transform);
        ++this.size;
        if (this.size >= this.capacity)
        {
            this.isFull = true;
        }
        int index = this.capacity - this.size + 1;
        // 到达
        if (this.size == 1)
        {
            this.conveyor.Transport(go, nodes[index].transform.position, OnEnqueueCallBack);
        }
        else
        {
            this.conveyor.Transport(go, nodes[index].transform.position);
        }
    }

    private void OnEnqueueCallBack(Transform go)
    {
        MsgCenter.Call<int, Board>(MsgDefine.StartHandle, this.id, go.GetComponent<Board>());
    }

    /// <summary>
    /// 出队列
    /// </summary>
    public void Pop()
    {
        if (this.size == 0)
        {
            return;
        }
        --this.size;
        this.isFull = false;
        this.children.RemoveAt(0);
        AutoCarry();
    }

    private void AutoCarry()
    {
        for (int i = 0, j = this.nodes.Count - 1; i < this.children.Count; ++i)
        {
            if (i == 0)
            {
                this.conveyor.Transport(this.children[i].transform, nodes[j--].transform.position, OnEnqueueCallBack);
            }
            else
            {
                this.conveyor.Transport(this.children[i].transform, nodes[j--].transform.position);
            }
        }
    }
}
