using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 0号子弹
/// </summary>
public class BallBullet : Bullet
{
    private Monster _target;
    private Vector3 _direction;

    public void Load(int bulletId, int level, Rect mapRect, Monster monster)
    {
        base.Load(bulletId, level, mapRect);

        _target = monster;
        _direction = (_target.transform.position - transform.position).normalized;
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
        if (IsExploded) return;

        if (_target != null)
        {
            if (!_target.IsDead)
                _direction = (_target.transform.position - transform.position).normalized;

            //角度
            LookAt();

            //移动
            transform.Translate(_direction*Speed*Time.deltaTime, Space.World);

            //击中
            if (Vector3.Distance(_target.transform.position, transform.position) <= 0.1f)
            {
                _target.Damage((int) Attack);
                Explode();
            }
        }
        else
        {
            //移动中，monster被消灭
            transform.Translate(_direction * Speed * Time.deltaTime, Space.World);

            if(!MapRect.Contains(transform.position) && !IsExploded) Explode();
        }
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnspawn()
    {
        base.OnUnspawn();
    }

    private void LookAt()
    {
        float angles = Mathf.Atan2(_direction.y, _direction.x);

        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.z = angles * Mathf.Rad2Deg - 90;
        transform.eulerAngles = eulerAngles;
    }
}
