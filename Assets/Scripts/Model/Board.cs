using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 手机壳装载板
/// </summary>
public class Board : MonoBehaviour
{
    /// <summary>
    /// 装载板编号
    /// </summary>
    [SerializeField]
    private int id = 0;
    /// <summary>
    /// 是否已加工
    /// </summary>
    private bool processed = false;
    /// <summary>
    /// 手机壳数量
    /// </summary>
    private int amount = 8;

    public int Id { get => id; set => id = value; }
    public bool IsProcessed { get => processed; set => processed = value; }
    public int Amount { get => amount; set => amount = value; }
}
