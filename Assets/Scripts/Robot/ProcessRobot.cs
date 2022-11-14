using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessRobot : BaseRobot
{
    public GameObject boardOne;
    public GameObject boardTwo;
    public GameObject boardThree;

    private bool isRunningEnable = true;

    public bool IsRunningEnable { get => isRunningEnable; set => isRunningEnable = value; }

    void Start()
    {
        boardOne.SetActive(false);
        boardTwo.SetActive(false);
        boardThree.SetActive(false);
    }

    public void StartProcess()
    {
        if (this.isRunningEnable)
        {
            animator.SetTrigger("Work");
        }
    }

    public void EndProcessOne()
    {
        boardOne.SetActive(true);
    }
    public void EndProcessTwo()
    {
        boardOne.SetActive(false);
        boardTwo.SetActive(true);
    }
    public void EndProcessThree()
    {
        boardTwo.SetActive(false);
        boardThree.SetActive(true);
    }
    public void EndProcess()
    {
        boardThree.SetActive(false);
    }
}
