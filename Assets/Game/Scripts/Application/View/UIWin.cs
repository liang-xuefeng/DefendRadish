using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWin : View
{
    public Text Current;
    public Text Total;
    public Button Restart;
    public Button Continue;

    private void Start()
    {
        Restart.onClick.AddListener(OnClickRestart);
        Continue.onClick.AddListener(OnClickContinue);
        SetRoundInfo(0, 0);
    }

    public override string Name
    {
        get { return Consts.V_Win; }
    }

    public override void HandleEvent(string eventName, object data)
    {
        
    }

    public void Show()
    {
        gameObject.SetActive(true);

        RoundModel roundModel = GetModel<RoundModel>();
        SetRoundInfo(roundModel.RoundIndex + 1, roundModel.RoundTotal);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 重新玩一次
    /// </summary>
    private void OnClickRestart()
    {
        GameModel gameModel = GetModel<GameModel>();
        SendEvent(Consts.E_StartLevel, new StartLevelArgs { LevelIndex = gameModel.PlayLevelIndex });
    }

    /// <summary>
    /// 玩下一关
    /// </summary>
    private void OnClickContinue()
    {
        GameModel gameModel = GetModel<GameModel>();

        if (gameModel.PlayLevelIndex >= gameModel.LevelCount - 1)
        {
            Game.Instance.LoadScene(4);
            return;
        }
        //加载下一关
        SendEvent(Consts.E_StartLevel, new StartLevelArgs { LevelIndex = gameModel.PlayLevelIndex + 1 });
    }

    /// <summary> 更新回合信息 </summary>
    private void SetRoundInfo(int currentRound, int totalRound)
    {
        Current.text = currentRound.ToString("D2");
        Total.text = totalRound.ToString();
    }
}
