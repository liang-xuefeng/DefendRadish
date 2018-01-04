using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 瓶子炮
/// </summary>
public class Bottle : Tower
{
    private Transform _shotPoint;

    protected override void Awake()
    {
        base.Awake();
        _shotPoint = transform.Find("ShotPoint");
    }

    protected override void Shot(Monster monster)
    {
        base.Shot(monster);

        GameObject go = Game.Instance.ObjectPool.Spawn("BallBullet");
        BallBullet bullet = go.GetComponent<BallBullet>();
        bullet.transform.position = _shotPoint.position;
        bullet.Load(this.UseBulletId, this.Level, this.MapRect, monster);
    }

    public override void OnSpawn()
    {
        base.OnSpawn();
    }

    public override void OnUnspawn()
    {
        base.OnUnspawn();
    }
}
