using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : Singleton<StaticData>
{
    /// <summary> 怪物列表 </summary>
    private Dictionary<int,MonsterInfo> _monster = new Dictionary<int, MonsterInfo>();

    /// <summary> 萝卜列表 </summary>
    private Dictionary<int, LuoboInfo> _luobo = new Dictionary<int, LuoboInfo>();

    /// <summary> 炮塔列表 </summary>
    private Dictionary<int, TowerInfo> _towerInfos = new Dictionary<int, TowerInfo>();

    /// <summary> 子弹列表 </summary>
    private Dictionary<int, BulletInfo> _bulletInfos = new Dictionary<int, BulletInfo>();

    protected override void Awake()
    {
        base.Awake();
        InitLuobo();
        InitMonsters();
        InitTowers();
        InitBullets();
    }

    private void InitLuobo()
    {
        _luobo.Add(0, new LuoboInfo { Id = 0, Hp = 10 });
    }

    private void InitMonsters()
    {
        _monster.Add(0, new MonsterInfo { Id = 0, Hp = 1, MoveSpeed = 1.5f });
        _monster.Add(1, new MonsterInfo { Id = 1, Hp = 2, MoveSpeed = 1f });
        _monster.Add(2, new MonsterInfo { Id = 2, Hp = 5, MoveSpeed = 1f });
        _monster.Add(3, new MonsterInfo { Id = 3, Hp = 10, MoveSpeed = 1f });
        _monster.Add(4, new MonsterInfo { Id = 4, Hp = 10, MoveSpeed = 1f });
        _monster.Add(5, new MonsterInfo { Id = 5, Hp = 100, MoveSpeed = 0.5f });
    }

    private void InitTowers()
    {
        _towerInfos.Add(0, new TowerInfo()
                    { Id = 0, PrefabName = "Bottle", NormalIcon = "Bottle/Bottle01",DisabledIcon = "Bottle/Bottle00",
                    MaxLevel = 3, BasePrice = 1, ShotRate = 2, GuardRange = 3f, UseBulletId = 0 });
        _towerInfos.Add(1, new TowerInfo()
                    { Id = 1, PrefabName = "Fan", NormalIcon = "Fan/Fan01", DisabledIcon = "Fan/Fan00",
                    MaxLevel = 3, BasePrice = 2, ShotRate = 1, GuardRange = 3f, UseBulletId = 1 });
    }

    private void InitBullets()
    {
        _bulletInfos.Add(0, new BulletInfo { Id = 0, PrefabName = "BallBullet", BaseSpeed = 5f, BaseAttack = 1});
        _bulletInfos.Add(1, new BulletInfo { Id = 1, PrefabName = "FanBullet", BaseSpeed = 2f, BaseAttack = 1});
    }

    /// <summary>
    /// 获得怪物属性
    /// </summary>
    public MonsterInfo GetMonsterInfo(int monsterType)
    {
        if (_monster.ContainsKey(monsterType))
            return _monster[monsterType];

        return null;
    }

    /// <summary>
    /// 获得萝卜属性
    /// </summary>
    public LuoboInfo GetLuoboInfo()
    {
        return _luobo[0];
    }

    /// <summary>
    /// 获得炮塔属性
    /// </summary>
    public TowerInfo GetTowerInfo(int towerId)
    {
        if (_towerInfos.ContainsKey(towerId))
            return _towerInfos[towerId];
        return null;
    }

    /// <summary>
    /// 获得子弹属性
    /// </summary>
    public BulletInfo GetBulletInfo(int bulletId)
    {
        if (_bulletInfos.ContainsKey(bulletId))
            return _bulletInfos[bulletId];
        return null;
    }
}
