using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILost : View
{
    public Text Current;
    public Text Total;
    public Button Restart;

    private void Start()
    {
        Restart.onClick.AddListener(OnClickRestart);
        SetRoundInfo(0, 0);
    }

    public override string Name
    {
        get { return Consts.V_Lost; }
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

    public override void HandleEvent(string eventName, object data)
    {
        
    }

    private void OnClickRestart()
    {
        GameModel gameModel = GetModel<GameModel>();
        SendEvent(Consts.E_StartLevel, new StartLevelArgs { LevelIndex = gameModel.PlayLevelIndex });
    }

    /// <summary> 更新回合信息 </summary>
    private void SetRoundInfo(int currentRound, int totalRound)
    {
        Current.text = currentRound.ToString("D2");
        Total.text = totalRound.ToString();
    }
}
