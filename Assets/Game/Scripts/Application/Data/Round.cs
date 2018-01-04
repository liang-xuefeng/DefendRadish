using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回合信息
/// </summary>
public class Round
{
    public int Monster;
    public int Count;

    public Round(int monsterId, int count)
    {
        Monster = monsterId;
        Count = count;
    }
}
