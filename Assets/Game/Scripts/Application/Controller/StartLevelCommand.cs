using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelCommand : Controller
{
    public override void Execute(object data)
    {
        StartLevelArgs e = data as StartLevelArgs;

        GameModel gameModel = GetModel<GameModel>();
        gameModel.StartLevel(e.LevelIndex);

        RoundModel roundModel = GetModel<RoundModel>();
        roundModel.LoadLevel(gameModel.PlayLevel);

        Game.Instance.LoadScene(3);
    }
}


