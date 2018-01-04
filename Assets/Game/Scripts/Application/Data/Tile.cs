using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储格子的信息
/// </summary>
public class Tile
{
    public int X;
    public int Y;
    /// <summary> 是否可以建造 </summary>
    public bool CanHold;
    /// <summary> 格子中存储的数据 </summary>
    public object Data;

    public Tile(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return string.Format("[X:{0}, Y:{1}, CanHold:{2}]", X, Y, CanHold);
    }
}

