using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICountDown : View
{
    public Image Count;
    public Sprite[] Sprites;

    public override string Name
    {
        get { return Consts.V_CountDown; }
    }

    public void StartCountDown()
    {
        Show();
        StartCoroutine(DisplayCountDown());
    }

    public override void RegisterEvents()
    {
        AttationEvents.Add(Consts.E_EnterScene);
    }

    public override void HandleEvent(string eventName, object data)
    {
        if (eventName != Consts.E_EnterScene) return;

        SceneArgs e = data as SceneArgs;
        if (e != null && e.SceneId == 3)
        {
            StartCountDown();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    IEnumerator DisplayCountDown()
    {
        int count = Sprites.Length;
        while (count > 0)
        {
            Count.sprite = Sprites[count - 1];
            count--;

            yield return new WaitForSeconds(1f);
        }

        Hide();
        SendEvent(Consts.E_CountDownComlete);
    }
}
