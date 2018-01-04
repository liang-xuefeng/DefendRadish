using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炮塔的基类
/// </summary>
public abstract class Tower : ReusableObject
{
    private int _level;
    protected Animator _animator;
    /// <summary> 攻击目标 </summary>
    private Monster _target;
    private Tile _tile;
    private float _lastAttackTime = 0;

    public int Id { get; private set; }

    public bool IsTopLevel {get { return _level >= MaxLevel; } }

    public int BasePrice { get; private set; }

    public int Price { get { return BasePrice*_level; } }

    public int Level
    {
        get { return _level; }
        set
        {
            _level = Mathf.Clamp(value, 0, MaxLevel);
            transform.localScale = Vector3.one * (1 + _level * 0.25f);
        }
    }

    public int MaxLevel { get; private set; }
    /// <summary> 警戒范围 </summary>
    public float GuardRange { get; private set; }
    /// <summary> 发射子弹速度  颗/s </summary>
    public float ShotRate { get; private set; }
    /// <summary> 使用子弹ID </summary>
    public int UseBulletId { get; private set; }
    /// <summary> 地图范围 </summary>
    public Rect MapRect { get; private set; }

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //用来让塔搜索目标
        if (_target == null)
        {
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
            foreach (GameObject monster in monsters)
            {
                Monster m = monster.GetComponent<Monster>();
                if (!m.IsDead && Vector3.Distance(m.transform.position, transform.position) <= GuardRange)
                {
                    _target = m;
                    break;
                }
            }
        }
        else
        {
            if (_target.IsDead || Vector3.Distance(_target.transform.position, transform.position) > GuardRange)
            {
                _target = null;
                return;
            }

            if (Time.time >= _lastAttackTime + 1f/ShotRate)
            {
                Shot(_target);
                _lastAttackTime = Time.time;
            }
        }
        LookAt();
    }

    /// <summary>
    /// 看向目标
    /// </summary>
    private void LookAt()
    {
        if (_target == null)
        {
            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.z = 0;
            transform.eulerAngles = eulerAngles;
        }
        else
        {
            Vector3 dir = (_target.transform.position - transform.position).normalized;
            float dy = dir.y;
            float dx = dir.x;

            float angles = Mathf.Atan2(dy, dx);

            Vector3 eulerAngles = transform.eulerAngles;
            eulerAngles.z = angles * Mathf.Rad2Deg - 90;
            transform.eulerAngles = eulerAngles;
        }
    }

    public void Load(int towerId, Tile tile, Rect mapRect)
    {
        TowerInfo info = Game.Instance.StaticData.GetTowerInfo(towerId);
        Id = info.Id;
        BasePrice = info.BasePrice;
        MaxLevel = info.MaxLevel;
        Level = 1;
        GuardRange = info.GuardRange;
        ShotRate = info.ShotRate;
        UseBulletId = info.UseBulletId;

        _tile = tile;
        MapRect = mapRect;
    }

    /// <summary>
    /// 攻击
    /// </summary>
    protected virtual void Shot(Monster monster)
    {
        _animator.SetTrigger("IsAttack");
    }

    public override void OnSpawn()
    {
        _animator = GetComponent<Animator>();
        _animator.Play("Idle");
    }

    public override void OnUnspawn()
    {
        _animator.ResetTrigger("IsAttack");
        _animator = null;
        _target = null;
        _tile = null;

        Id = 0;
        BasePrice = 0;
        Level = 0;
        MaxLevel = 0;
        GuardRange = 0;
        ShotRate = 0;
        UseBulletId = 0;
    }
}
