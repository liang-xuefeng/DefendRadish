using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStart : View
{
    public Button Adventure;

    void Start()
    {
        Adventure.onClick.AddListener(GoToSelect);
    }

    public override string Name
    {
        get { return Consts.V_Start; }
    }

    public override void HandleEvent(string eventName, object data)
    {
        
    }

    private void GoToSelect()
    {
        Game.Instance.LoadScene(2);
    }
}
