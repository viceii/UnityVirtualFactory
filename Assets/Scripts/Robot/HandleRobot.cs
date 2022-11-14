using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRobot : BaseRobot
{
    public GameObject boardOne;
    public GameObject boardTwo;
    public GameObject boardThree;

    [SerializeField]
    private WaitQueue waitQueue;
    [SerializeField]
    private TransportProcedure transportProcedure;

    private float handleTime = 0;
    private int printCount = 10;

    private int currIndex = 0;

    private void Start()
    {
        // �����ֻ��Ƿ����ڴ�ӡ���϶���
        AnimationClip handleClip = animator.runtimeAnimatorController.animationClips[0];
        handleTime = handleClip.length;

        MsgCenter.AddListener<int, Board>(MsgDefine.StartHandle, HandleShell);
        MsgCenter.AddListener<int>(MsgDefine.PlaceToPack, PlaceToPack);
        MsgCenter.AddListener<int>(MsgDefine.PackEnd, PackEnd);
        
        MsgCenter.AddListener<int>(MsgDefine.TakeOneEnd, TakeOneEnd);
        MsgCenter.AddListener<int>(MsgDefine.StartPrint, StartPrint);
        MsgCenter.AddListener<int>(MsgDefine.PlaceAnimOneEnd, PlaceAnimOneEnd);
        MsgCenter.AddListener<int>(MsgDefine.PlaceAnimTwoEnd, PlaceAnimTwoEnd);

        boardOne.SetActive(false);
        boardTwo.SetActive(false);
        boardThree.SetActive(false);
    }

    private Board board;
    private Transform[] gos;
    public void HandleShell(int id, Board board)
    {
        if (this.Id != id)
            return;
        this.board = board;
        this.printCount = board.Amount;
        this.gos = board.transform.Find("Line059").GetComponentsInChildren<Transform>();
        this.currIndex = 0;
        base.animator.SetTrigger("Handle");
        Call.Instance.Delay(handleTime / 1.2f, () =>
        {
            this.gos[currIndex + 1].gameObject.SetActive(false);
        });
    }

    public void PlaceToPack(int id)
    {
        if (this.Id == id)
            base.animator.SetTrigger("PrintOver");
    }

    private void PackEnd(int id)
    {
        if (this.Id != id)
            return;
        ++this.currIndex;
        if (currIndex >= printCount)
        {
            transportProcedure.StartTransport(this.board.transform);
            waitQueue.Pop();
        }
        else
        {
            animator.SetTrigger("Handle");
        }
    }


    private void TakeOneEnd(int id)
    {
        if (this.Id == id)
        {
            boardOne.SetActive(true);
        }
    }
    private void StartPrint(int id)
    {
        if (this.Id == id)
        {
            boardOne.SetActive(false);
            boardTwo.SetActive(true);
        }
    }
    private void PlaceAnimOneEnd(int id)
    {
        if (this.Id == id)
        {
            boardTwo.SetActive(false);
            boardOne.SetActive(true);
        }
    }
    private void PlaceAnimTwoEnd(int id)
    {
        if (this.Id == id)
        {
            boardOne.SetActive(false);
            boardThree.SetActive(true);
        }
    }
}
