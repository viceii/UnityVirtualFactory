using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public Action<Collider> onEnter;
    public Action<Collider> onExit;

    private void OnTriggerEnter(Collider other)
    {
        if (onEnter != null)
        {
            onEnter.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (onExit != null)
        {
            onExit.Invoke(other);
        }

    }
}
