using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Role
{
    public MonsterType MonsterType = MonsterType.Monster0;

    /// <summary> 到达终点 </summary>
    public event Action<Monster> Reached; 

    private float _moveSpeed;

    /// <summary> 路径拐点 </summary>
    private Vector3[] _path;
    /// <summary> 下一个拐点 </summary>
    private int _pointIndex = -1;
    /// <summary> 是否到达终点 </summary>
    private bool _isReached = false;

    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    private void Update()
    {
        if (_isReached) return;

        Vector3 pos = transform.position;
        Vector3 dest = _path[_pointIndex + 1];

        float dis = Vector3.Distance(pos, dest);
        if (dis < 0.1f)
        {
            MoveTo(dest);
            if (HasNext())
            {
                MoveNext();
            }
            else
            {
                _isReached = true;
                if (Reached != null) Reached(this);
            }
        }
        else
        {
            Vector3 direction = (dest - pos).normalized;
            transform.Translate(direction * _moveSpeed * Time.deltaTime);
        }
    }

    /// <summary> 加载路径拐点 </summary>
    public void LoadPath(Vector3[] path)
    {
        _path = path;
        MoveNext();
    }

    /// <summary> 移动到下一个点 </summary>
    public void MoveNext()
    {
        if (!HasNext()) return;

        if (_pointIndex == -1)
        {
            _pointIndex = 0;
            MoveTo(_path[_pointIndex]);
        }
        else _pointIndex++;
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
        MonsterInfo info = StaticData.Instance.GetMonsterInfo((int) MonsterType);
        MaxHp = info.Hp;
        Hp = info.Hp;
        MoveSpeed = info.MoveSpeed;
    }

    public override void OnUnspawn()
    {
        base.OnUnspawn();

        Hp = 0;
        MaxHp = 0;
        MoveSpeed = 0;
        _path = null;
        _pointIndex = -1;
        _isReached = false;
        Reached = null;
    }

    /// <summary> 是否还有下一个拐点 </summary>
    private bool HasNext()
    {
        return _pointIndex + 1 < _path.Length - 1;
    }

    /// <summary> 直接移动过去 </summary>
    private void MoveTo(Vector3 pos)
    {
        transform.position = pos;
    }
}
