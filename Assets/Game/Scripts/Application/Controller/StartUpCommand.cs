using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 启动游戏命令
/// </summary>
public class StartUpCommand : Controller
{

    public override void Execute(object data)
    {
        //注册model
        RegisterModel(new GameModel());
        RegisterModel(new RoundModel());

        //注册controller
        RegisterController(Consts.E_EnterScene, typeof(EnterSceneCommand));
        RegisterController(Consts.E_ExitScene, typeof(ExitSceneCommand));

        RegisterController(Consts.E_StartLevel,typeof(StartLevelCommand));
        RegisterController(Consts.E_EndLevel,typeof(EndLevelCommand));

        RegisterController(Consts.E_CountDownComlete,typeof(CountDownCommand));

        RegisterController(Consts.E_UpgradeTower, typeof(UpgradeTowerCommand));
        RegisterController(Consts.E_SellTower, typeof(SellTowerCommand));

        //初始化model
        GameModel gameModel = GetModel<GameModel>();
        gameModel.Init();

        //进入开始界面
        Game.Instance.LoadScene(1);
    }
}
