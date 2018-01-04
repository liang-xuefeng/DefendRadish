using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 怪物管理
/// </summary>
public class Spawner : View
{
    private Map _map;
    private Luobo _luobo;

    public override string Name
    {
        get { return Consts.V_Spawner; }
    }

    public override void RegisterEvents()
    {
        AttationEvents.Add(Consts.E_EnterScene);
        AttationEvents.Add(Consts.E_SpawnMonster);

        AttationEvents.Add(Consts.E_SpawnTower);
    }

    public override void HandleEvent(string eventName, object data)
    {
        switch (eventName)
        {
            case Consts.E_EnterScene:
                SceneArgs sceneArgs = data as SceneArgs;
                if (sceneArgs.SceneId == 3)
                {
                    //加载数据
                    GameModel gameModel = GetModel<GameModel>();

                    //加载地图
                    _map = GetComponent<Map>();
                    _map.LoadLevel(gameModel.PlayLevel);
                    _map.OnTileClick += MapOnOnTileClick;

                    //加载萝卜
                    SpawnLuobo(_map.Path[_map.Path.Length - 1]);
                }
                break;
            case Consts.E_SpawnMonster:
                SpawnMonsterArgs spawnMonsterArgs = data as SpawnMonsterArgs;
                SpawnMonster(spawnMonsterArgs.MosterType);
                break;
            case Consts.E_SpawnTower:
                SpawnTowerArgs spawnTower = data as SpawnTowerArgs;
                SpawnTower(spawnTower.TowerId, spawnTower.Pos);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 点击地图格子事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="tileClickEventArgs"></param>
    private void MapOnOnTileClick(object sender, TileClickEventArgs tileArgs)
    {
        GameModel gameModel = GetModel<GameModel>();
        if (gameModel.IsPlaying && tileArgs.Tile.CanHold)
        {
            Tile tile = tileArgs.Tile;

            if (tile.Data == null)
            {
                //空格子，可以放塔
                ShowSpawnPanelArgs show = new ShowSpawnPanelArgs { Pos = _map.GetPosition(tile), UpSide = tile.Y < Map.RowCount / 2 };
                SendEvent(Consts.E_ShowSpawnPanel, show);
            }
            else
            {
                //有塔，可以操作塔
                Tower tower = tile.Data as Tower;
                ShowUpgradePanelArgs show = new ShowUpgradePanelArgs { Tower = tower};
                SendEvent(Consts.E_ShowUpgradePanel, show);
            }
        }
    }

    /// <summary>
    /// 生产一个怪
    /// </summary>
    /// <param name="mosterType"></param>
    private void SpawnMonster(int mosterType)
    {
        string prefabName = "Monster" + mosterType;
        GameObject go = Game.Instance.ObjectPool.Spawn(prefabName);
        Monster monster = go.GetComponent<Monster>();
        monster.Reached += MonsterOnReached;
        monster.HpChanged += MonsterOnHpChanged;
        monster.Dead += MonsterOnDead;
        monster.LoadPath(_map.Path);
    }

    /// <summary>
    /// 产生一个炮塔
    /// </summary>
    private void SpawnTower(int towerId, Vector3 pos)
    {
        //找到Tile
        Tile tile = _map.GetTile(pos);

        TowerInfo info = Game.Instance.StaticData.GetTowerInfo(towerId);
        GameObject go = Game.Instance.ObjectPool.Spawn(info.PrefabName);

        Tower tower = go.GetComponent<Tower>();
        tower.transform.position = pos;
        tower.Load(towerId, tile, _map.MapRect);

        tile.Data = tower;
    }

    /// <summary>
    /// 生产萝卜
    /// </summary>
    private void SpawnLuobo(Vector3 pos)
    {
        GameObject go = Game.Instance.ObjectPool.Spawn("Luobo");
        _luobo = go.GetComponent<Luobo>();
        _luobo.Dead += LuoboOnDead;
        _luobo.transform.position = pos;
    }

    /// <summary> 怪物血量变化 </summary>
    private void MonsterOnHpChanged(int hp, int maxHp)
    {
        
    }

    /// <summary> 怪物死亡 </summary>
    private void MonsterOnDead(Role role)
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        RoundModel roundModel = GetModel<RoundModel>();

        //游戏胜利
        if (!_luobo.IsDead && monsters.Length <= 0 && roundModel.AllRoundsComplete)
        {
            GameModel gameModel = GetModel<GameModel>();
            SendEvent(Consts.E_EndLevel, new EndLevelArgs { LevelIndex = gameModel.PlayLevelIndex, IsSuccess = true });
        }
    }

    /// <summary> 萝卜死亡 </summary>
    private void LuoboOnDead(Role role)
    {
        GameModel gameModel = GetModel<GameModel>();
        SendEvent(Consts.E_EndLevel, new EndLevelArgs { LevelIndex = gameModel.PlayLevelIndex, IsSuccess = false });
    }

    /// <summary> 到达终点 </summary>
    private void MonsterOnReached(Monster monster)
    {
        //萝卜掉血
        _luobo.Damage(1);
        
        //回收
        Game.Instance.ObjectPool.Unspawn(monster.gameObject);

        monster.Hp = 0;
    }
}
