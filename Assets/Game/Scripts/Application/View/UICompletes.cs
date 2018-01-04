using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICompletes : View
{
    public Button Select;
    public Button Clear;

    void Start()
    {
        Select.onClick.AddListener(OnClickSelect);
        Clear.onClick.AddListener(OnClickClear);
    }

    public override string Name
    {
        get { return Consts.V_Completes; }
    }

    public override void HandleEvent(string eventName, object data)
    {
        
    }

    private void OnClickSelect()
    {
        Game.Instance.LoadScene(1);
    }

    private void OnClickClear()
    {
        
    }
}
