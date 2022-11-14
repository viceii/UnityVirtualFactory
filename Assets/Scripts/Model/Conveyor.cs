using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Conveyor : Singleton<Conveyor>
{
    public void Transport(Transform go, Vector3 target, Action<Transform> callback = null)
    {
        MonoController.StartCoroutine(Transporting(go, target, callback));
    }

    [SerializeField]
    private float speed = 1f;
    IEnumerator Transporting(Transform go, Vector3 target, Action<Transform> callback = null)
    {
        while (true)
        {
            if (Vector3.Distance(go.position, target) < 0.1f)
            {
                yield return new WaitForSeconds(0.2f);
                break;
            }
            go.position = Vector3.MoveTowards(go.position, target, speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        if (callback != null)
        {
            callback.Invoke(go);
        }
        yield break;
    }
}
