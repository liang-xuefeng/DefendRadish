using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炮塔属性
/// </summary>
public class TowerInfo
{
    public int Id;
    public int BasePrice;
    /// <summary>
    /// 正常icon
    /// </summary>
    public string NormalIcon;
    /// <summary>
    /// 灰色icon
    /// </summary>
    public string DisabledIcon;

    public string PrefabName;
    public int MaxLevel;
    /// <summary>
    /// 警戒范围
    /// </summary>
    public float GuardRange;
    /// <summary>
    /// 发射子弹速度  颗/s
    /// </summary>
    public float ShotRate;
    /// <summary>
    /// 使用子弹ID
    /// </summary>
    public int UseBulletId;
}
