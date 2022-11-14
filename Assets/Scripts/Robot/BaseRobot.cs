using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BaseRobot : MonoBehaviour
{
    [SerializeField]
    private int id = 0;
    protected Animator animator;
    private bool working = true;

    public bool IsWorking { get => working;}
    public Animator Animator { get => animator; set => animator = value; }
    public int Id { get => id; set => id = value; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Stop()
    {
        if (animator != null)
        {
            animator.speed = 0;
            this.working = false;
        }
    }

    public void Running()
    {
        if (animator != null)
        {
            animator.speed = 1;
            this.working = true;
        }
    }
}
