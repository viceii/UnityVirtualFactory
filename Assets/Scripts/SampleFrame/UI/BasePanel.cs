using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasePanel : MonoBehaviour
{
    // 窗体根节点
    protected Transform widgetRoot;

    // 是否可见
    private bool visible;
    private bool interactive;

    public bool Visible { get => visible; protected set => visible = value; }
    public bool Interactive { get => interactive; protected set => interactive = value; }

    /// <summary>
    /// 显示面板
    /// </summary>
    public virtual void OnShow()
    {
        visible = true;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    public virtual void OnHide()
    {
        visible = false;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 激活面板
    /// </summary>
    public virtual void OnActive()
    {
        interactive = false;
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 冻结面板
    /// </summary>
    public virtual void OnBlock()
    {
        interactive = false;
        gameObject.SetActive(true);
    }
}
