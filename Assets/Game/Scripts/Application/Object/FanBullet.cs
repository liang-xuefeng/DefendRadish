using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 1号子弹
/// </summary>
public class FanBullet : Bullet
{
    //旋转速度（度/秒）
    public float RotateSpeed = 180f;

    public Vector2 Direction { get; private set; }

    public void Load(int bulletID, int level, Rect mapRect, Vector3 direction)
    {
        Load(bulletID, level, mapRect);
        Direction = direction;
    }

    protected override void Update()
    {
        //已爆炸跳过
        if (IsExploded)
            return;

        //移动
        transform.Translate(Direction * Speed * Time.deltaTime, Space.World);

        //旋转
        transform.Rotate(Vector3.forward, RotateSpeed * Time.deltaTime, Space.World);

        //检测（存活/死亡）
        GameObject[] monsterObjects = GameObject.FindGameObjectsWithTag("Monster");

        foreach (GameObject monsterObject in monsterObjects)
        {
            Monster monster = monsterObject.GetComponent<Monster>();

            //忽略已死亡的怪物
            if (monster.IsDead)
                continue;

            if (Vector3.Distance(transform.position, monster.transform.position) <= 0.7f)
            {
                //敌人受伤
                monster.Damage((int)Attack);

                //爆炸
                Explode();

                //退出(重点)
                break;
            }
        }

        //边间检测
        if (!IsExploded && !MapRect.Contains(transform.position))
            Explode();
    }
}
