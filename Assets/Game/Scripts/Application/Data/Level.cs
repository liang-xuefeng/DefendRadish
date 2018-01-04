using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡信息
/// </summary>
public class Level
{
    /// <summary> 关卡名 </summary>
    public string Name;
    /// <summary> 小地图名 </summary>
    public string CardImage;
    /// <summary> 背景图 </summary>
    public string Background;
    /// <summary> 路径图 </summary>
    public string Road;
    /// <summary> 初始分 </summary>
    public int InitScore;
    /// <summary> 可建造位置 </summary>
    public List<Point> Holders = new List<Point>();
    /// <summary> 怪物行走路径 </summary>
    public List<Point> Path = new List<Point>();
    /// <summary> 出怪回合信息 </summary>
    public List<Round> Rounds = new List<Round>();
}
