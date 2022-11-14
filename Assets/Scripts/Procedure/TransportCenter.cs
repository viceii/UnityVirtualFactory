using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class TransportCenter : MonoBehaviour
{
    [SerializeField]
    private TransportProcedure[] transportProcedures;

    private CollisionHandler collisionHandler;
    private bool[] isTransporting = new bool[6];

    private void Awake()
    {
        this.collisionHandler = GetComponent<CollisionHandler>();
        this.collisionHandler.onEnter += SendBack;

        for (int i = 0; i < isTransporting.Length; ++i)
        {
            isTransporting[i] = true;
        }

        MsgCenter.AddListener<int>(MsgDefine.ProcessLineFinished, (id) => { isTransporting[id] = false; });
    }

    private void SendBack(Collider collider)
    {
        for (int i = 0; i < isTransporting.Length; ++i)
        {
            if (!isTransporting[i])
            {
                this.transportProcedures[i].StartTransport(collider.transform);
                isTransporting[i] = true;
                break;
            }
        }
    }
}
