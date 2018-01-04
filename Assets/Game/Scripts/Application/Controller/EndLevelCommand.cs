using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelCommand : Controller
{
    public override void Execute(object data)
    {
        EndLevelArgs e = data as EndLevelArgs;

        //保存游戏状态
        GameModel gameModel = GetModel<GameModel>();
        gameModel.StopLevel(e.IsSuccess);

        //显视UI
        if (e.IsSuccess)
        {
            GetView<UIWin>().Show();
        }
        else
        {
            GetView<UILost>().Show();
        }
    }
}
