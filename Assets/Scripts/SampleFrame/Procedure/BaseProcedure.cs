using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BaseProcedure : MonoBehaviour
{
    [SerializeField]
    private bool running = true;

    public bool Running { get => running; set => running = value; }

    public virtual void OnEnter()
    {

    }

    public virtual void OnStart()
    {
        MonoController.AddUpdateListener(OnUpdateListener);
        MonoController.AddFixedUpdateListener(OnFixedUpdateListener);
        MonoController.AddLateUpdateListener(OnLateUpdateListener);
    }

    public virtual void OnExit()
    {
        MonoController.AddUpdateListener(OnUpdate);
        MonoController.RemoveFixedUpdateListener(OnFixedUpdate);
        MonoController.RemoveLateUpdateListener(OnLateUpdate);

        running = false;
    }

    private void OnUpdateListener()
    {
        if (Running) OnUpdate();
    }

    private void OnFixedUpdateListener()
    {
        if (Running) OnFixedUpdate();
    }

    private void OnLateUpdateListener()
    {
        if(Running) OnLateUpdate();
    }

    public virtual void OnUpdate()
    {

    }

    public virtual void OnFixedUpdate()
    {

    }

    public virtual void OnLateUpdate()
    {

    }
}