using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 怪物与萝卜的基类
/// </summary>
public abstract class Role:ReusableObject, IReusable
{
    private int _hp;
    private int _maxHp;

    /// <summary> 血量变化 </summary>
    public event Action<int, int> HpChanged;
    /// <summary> 人物死亡 </summary>
    public event Action<Role> Dead;

    // <summary> 当前血量 </summary>
    public int Hp
    {
        get{ return _hp; }
        set
        {
            _hp = Mathf.Clamp(value, 0, _maxHp);
            if (HpChanged != null) HpChanged(value, _maxHp);
            if (value <= 0 && Dead != null) Dead(this);
        }
    }

    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    /// <summary> 是否死亡 </summary>
    public bool IsDead { get { return _hp == 0; } }

    /// <summary> 受伤 </summary>
    public virtual void Damage(int hit)
    {
        if (IsDead) return;
        Hp -= hit;
    }

    /// <summary>
    /// 死亡处理
    /// </summary>
    protected virtual void Die(Role role)
    {
        
    }


    public override void OnSpawn()
    {
        Dead += Die;
    }

    public override void OnUnspawn()
    {
        Hp = 0;
        MaxHp = 0;

        while (HpChanged != null)
        {
            HpChanged -= HpChanged;
        }

        while (Dead != null)
        {
            Dead -= Dead;
        }
    }


}
