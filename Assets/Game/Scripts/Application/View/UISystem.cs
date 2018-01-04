using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : View
{
    public Button Continue;
    public Button Restart;
    public Button Select;

    private void Start()
    {
        Continue.onClick.AddListener(OnClickContinue);
        Restart.onClick.AddListener(OnClickRestart);
        Select.onClick.AddListener(OnClickSelect);
    }

    public override string Name
    {
        get { return Consts.V_System; }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public override void HandleEvent(string eventName, object data)
    {
        
    }

    private void OnClickContinue()
    {
        
    }

    private void OnClickRestart()
    {

    }

    private void OnClickSelect()
    {

    }
}
