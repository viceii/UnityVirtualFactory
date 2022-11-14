using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Call : Singleton<Call>
{
    public delegate void Function();

    public void Delay(float time, Function function)
    {
        MonoController.StartCoroutine(DelayInvoke(time, function));
    }

    IEnumerator DelayInvoke(float time, Function function)
    {
        yield return new WaitForSeconds(time);
        function.Invoke();
    }
}