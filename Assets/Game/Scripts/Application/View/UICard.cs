using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICard : MonoBehaviour, IPointerDownHandler
{
    public Image CardImage;
    public Image LockImage;

    public event Action<Card> OnClickCard;

    /// <summary> 是否半透明 </summary>
    public bool IsTransparent
    {
        get { return _isTransparent; }
        set
        {
            _isTransparent = value;

            Image[] images = new[] {CardImage, LockImage};
            
            foreach (Image image in images)
            {
                Color color = image.color;
                color.a = value ? 0.5f : 1f;
                image.color = color;
            }
        }
    }

    private Card _card;
    private bool _isTransparent = false;

    public void DataBind(Card card)
    {
        //保存
        _card = card;

        //加载图片
        string cardFile = "file://" + Consts.CardDir + "/" + _card.CardImage;
        StartCoroutine(Tools.LoadImage(cardFile, CardImage));

        //是否透明
        //IsTransparent = card.IsLocked;

        //是否锁定
        LockImage.gameObject.SetActive(card.IsLocked);
    }

    /// <summary>
    /// 鼠标点击
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (OnClickCard != null)
            OnClickCard(_card);
    }

    private void OnDestroy()
    {
        while (OnClickCard != null)
            OnClickCard -= OnClickCard;
    }
}
