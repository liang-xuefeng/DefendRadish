using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 常量
/// </summary>
public static class Consts
{
    /// <summary> 关卡目录 </summary>
    public static string LevelDir = Application.dataPath + @"\Game\Resources\Res\Levels";
    /// <summary> 地图目录 </summary>
    public static string MapDir = Application.dataPath + @"\Game\Resources\Res\Maps";
    /// <summary> 小地图目录 </summary>
    public static string CardDir = Application.dataPath + @"\Game\Resources\Res\Cards";

    #region 存档
    /// <summary> 存档的最大通关索引 </summary>
    public const string GameProgress = "GameProgress";

    #endregion

    #region 事件
    /// <summary> 启动游戏 </summary>
    public const string E_StartUp = "E_StartUp";

    /// <summary> 进入场景 </summary>
    public const string E_EnterScene = "E_EnterScene";
    /// <summary> 离开场景 </summary>
    public const string E_ExitScene = "E_ExitScene";

    /// <summary> 加载关卡 </summary>
    public const string E_StartLevel = "E_StartLevel";
    /// <summary> 结束关卡 </summary>
    public const string E_EndLevel = "E_ExitLevel";

    /// <summary> 倒计时结束 </summary>
    public const string E_CountDownComlete = "E_CountDownComlete";

    /// <summary> 回合开始 </summary>
    public const string E_StartRound = "E_StartRound";
    /// <summary> 产生怪物 </summary>
    public const string E_SpawnMonster = "E_SpawnMonster";

    /// <summary> 显视创建面板 </summary>
    public const string E_ShowSpawnPanel = "E_ShowSpawnPanel";
    /// <summary> 显视升级面板 </summary>
    public const string E_ShowUpgradePanel = "E_UpgradePanel";
    /// <summary> 隐藏所有面板 </summary>
    public const string E_HiedPopup = "E_HiedPopup";

    /// <summary> 创建塔 </summary>
    public const string E_SpawnTower = "E_SpawnTower";
    /// <summary> 升级塔 </summary>
    public const string E_UpgradeTower = "E_UpgradeTower";
    /// <summary> 出售塔 </summary>
    public const string E_SellTower = "E_SellTower";
    #endregion

    #region Model

    public const string M_GameModel = "M_GameModel";
    public const string M_RoundModel = "M_RoundModel";

    #endregion

    #region View
    /// <summary> Start面板 </summary>
    public const string V_Start = "V_Start";
    /// <summary> Select面板 </summary>
    public const string V_Select = "V_Select";

    //level界面
    public const string V_Board = "V_Board";
    public const string V_CountDown = "V_CountDown";
    public const string V_Win = "V_Win";
    public const string V_Lost = "V_Lost";
    public const string V_System = "V_System";
    public const string V_TowerPopup = "TowerPopup";

    /// <summary> Completes面板 </summary>
    public const string V_Completes = "V_Completes";
    /// <summary> 怪物管理面板 </summary>
    public const string V_Spawner = "V_Spawner";

    #endregion
}

public enum GameSpeed
{
    One,
    Two,
}

public enum MonsterType
{
    Monster0,
    Monster1,
    Monster2,
    Monster3,
    Monster4,
    Monster5,
}
