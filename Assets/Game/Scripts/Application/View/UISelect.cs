using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UISelect : View
{
    public Button BackButton;
    public Button HelpButton;
    public Button StartButton;

    private List<Card> _cards = new List<Card>();
    private int _selectIndex = -1;

    private GameModel _gameModel = null;

    private void Start()
    {
        BackButton.onClick.AddListener(OnClickBack);
        HelpButton.onClick.AddListener(OnClickHelp);
        StartButton.onClick.AddListener(OnClickStart);
    }

    public override string Name
    {
        get { return Consts.V_Select; }
    }

    public override void RegisterEvents()
    {
        AttationEvents.Add(Consts.E_EnterScene);
    }

    public override void HandleEvent(string eventName, object data)
    {
        if (eventName == Consts.E_EnterScene)
        {
            SceneArgs sceneArgs = data as SceneArgs;
            if (sceneArgs != null && sceneArgs.SceneId == 2)
            {
                _gameModel = GetModel<GameModel>();
                LoadCards();
            }
        }
    }

    /// <summary>
    /// 加载地图小图片
    /// </summary>
    private void LoadCards()
    {
        List<Level> levels = _gameModel.AlLevels;

        for (int i = 0; i < levels.Count; i++)
        {
            Card card = new Card()
            {
                LevelId = i,
                CardImage = levels[i].CardImage,
                IsLocked = !(i <= _gameModel.GameProgress + 1)
            };
            _cards.Add(card);
        }

        UICard[] uiCard = transform.Find("UICards").GetComponentsInChildren<UICard>();
        foreach (UICard card in uiCard)
        {
            card.OnClickCard += (c) => { SelectCard(c.LevelId); };
        }

        SelectCard(0);
    }

    /// <summary>
    /// 选择那个卡片
    /// </summary>
    /// <param name="index"></param>
    private void SelectCard(int index)
    {
        if (_selectIndex == index) return;
        _selectIndex = index;

        //计算索引号
        int left = _selectIndex - 1;
        int current = _selectIndex;
        int right = _selectIndex + 1;

        //绑定数据
        Transform container = this.transform.Find("UICards");

        //左边
        if (left < 0)
        {
            container.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            container.GetChild(0).gameObject.SetActive(true);
            container.GetChild(0).GetComponent<UICard>().DataBind(_cards[left]);
            container.GetChild(0).GetComponent<UICard>().IsTransparent = true;
        }

        //当前
        container.GetChild(1).GetComponent<UICard>().DataBind(_cards[current]);
        container.GetChild(1).GetComponent<UICard>().IsTransparent = false;

        //设置是否可以开始游戏
        StartButton.gameObject.SetActive(!_cards[current].IsLocked);

        //右边
        if (right >= _cards.Count)
        {
            container.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            container.GetChild(2).gameObject.SetActive(true);
            container.GetChild(2).GetComponent<UICard>().DataBind(_cards[right]);
            container.GetChild(2).GetComponent<UICard>().IsTransparent = true;
        }
    }

    private void OnClickBack()
    {
        Game.Instance.LoadScene(1);
    }

    private void OnClickHelp()
    {
        
    }

    /// <summary> 开始游戏 </summary>
    private void OnClickStart()
    {
        StartLevelArgs e = new StartLevelArgs {LevelIndex = _selectIndex};
        SendEvent(Consts.E_StartLevel, e);
    }
}
