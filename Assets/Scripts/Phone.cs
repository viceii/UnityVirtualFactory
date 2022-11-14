using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public int id;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        MsgCenter.AddListener<int>(MsgDefine.StartPrint, (id) => { if (id == this.id) animator.SetTrigger("Disappear"); });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
