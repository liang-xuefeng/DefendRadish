using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹基类
/// </summary>
public abstract class Bullet : ReusableObject
{
    public int Id { get; private set; }
    public int Level { get; private set; }
    public float BaseSpeed { get; private set; }
    public int BaseAttack { get; private set; }
    public float Speed  { get { return BaseSpeed*Level; } }
    public float Attack { get { return BaseAttack*Level; } }

    //地图范围
    public Rect MapRect { get; private set; }
    public float DelayToDestory = 1f;   //延时回收
    protected bool IsExploded = false;  //是否爆炸

    private Animator _animator;

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        
    }

    /// <summary>
    /// 加载子弹
    /// </summary>
    public void Load(int id, int level, Rect rect)
    {
        Id = id;
        Level = level;
        MapRect = rect;

        BulletInfo info = Game.Instance.StaticData.GetBulletInfo(id);
        BaseSpeed = info.BaseSpeed;
        BaseAttack = info.BaseAttack;
    }

    /// <summary>
    /// 爆炸
    /// </summary>
    public void Explode()
    {
        IsExploded = true;
        _animator.SetTrigger("IsExploded");
        StartCoroutine(DestoryCoroutine());
    }

    private IEnumerator DestoryCoroutine()
    {
        yield return new WaitForSeconds(DelayToDestory);

        Game.Instance.ObjectPool.Unspawn(gameObject);
    }

    public override void OnSpawn()
    {
        
    }

    public override void OnUnspawn()
    {
        IsExploded = false;
        _animator.Play("Play");
        _animator.ResetTrigger("IsExploded");
    }
}
