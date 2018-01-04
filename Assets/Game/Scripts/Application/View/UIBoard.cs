using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBoard : View
{
    public Text Scorce;
    public Text Current;
    public Text Total;
    public Image RoundInfo;
    public Image PauseInfo;
    public Button Speed1;
    public Button Speed2;
    public Button PlayBtn;
    public Button PauseBtn;
    public Button SystemBtn;

    private bool _isPlaying = false;
    private GameSpeed _gameSpeed = GameSpeed.One;
    private int _scorce = 0;

    /// <summary> 是否正在游戏 </summary>
    public bool IsPlaying {
        get
        {
            return _isPlaying;
        }
        set
        {
            _isPlaying = value;
            RoundInfo.gameObject.SetActive(value);
            PauseInfo.gameObject.SetActive(!value);

            PlayBtn.gameObject.SetActive(!value);
            PauseBtn.gameObject.SetActive(value);
        }
    }

    /// <summary> 游戏速度 </summary>
    public GameSpeed GameSpeed
    {
        get { return _gameSpeed; }
        set
        {
            _gameSpeed = value;
            if (value == GameSpeed.One)
            {
                Speed1.gameObject.SetActive(true);
                Speed2.gameObject.SetActive(false);
            }
            else
            {
                Speed1.gameObject.SetActive(false);
                Speed2.gameObject.SetActive(true);
            }
        }
    }

    /// <summary> 金币数量 </summary>
    public int ScorceGold
    {
        get { return _scorce; }
        set
        {
            _scorce = value;
            Scorce.text = value.ToString();
        }
    }

    private void Awake()
    {
        Speed1.onClick.AddListener(OnClickSpeed1);
        Speed2.onClick.AddListener(OnClickSpeed2);
        PlayBtn.onClick.AddListener(OnClickPlay);
        PauseBtn.onClick.AddListener(OnClickPause);
        SystemBtn.onClick.AddListener(OnClickSystem);

        IsPlaying = true;
        GameSpeed = GameSpeed.One;
        ScorceGold = 0;
    }

    public override string Name
    {
        get { return Consts.V_Board; }
    }

    public override void RegisterEvents()
    {
        
    }

    public override void HandleEvent(string eventName, object data)
    {
        if (eventName == Consts.E_CountDownComlete)
        {
           
        }
    }

    /// <summary> 更新回合信息 </summary>
    public void SetRoundInfo(int currentRound, int totalRound)
    {
        Current.text = currentRound.ToString("D2");
        Total.text = totalRound.ToString();
    }

    #region 点击事件
    private void OnClickSpeed1()
    {
        GameSpeed = GameSpeed.Two;
    }

    private void OnClickSpeed2()
    {
        GameSpeed = GameSpeed.One;
    }

    private void OnClickPlay()
    {
        IsPlaying = true;
    }

    private void OnClickPause()
    {
        IsPlaying = false;
    }

    private void OnClickSystem()
    {

    } 
    #endregion
}
