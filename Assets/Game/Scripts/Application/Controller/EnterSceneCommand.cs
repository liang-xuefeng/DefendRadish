using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterSceneCommand : Controller
{
    public override void Execute(object data)
    {
        //注册view
        SceneArgs sceneArgs = data as SceneArgs;
        Debug.Log("进入场景：" + sceneArgs.SceneId);
        switch (sceneArgs.SceneId)
        {
            case 0:
                break;
            case 1:
                RegisterView(GameObject.Find("UIStart").GetComponent<UIStart>());
                break;
            case 2:
                RegisterView(GameObject.Find("UISelect").GetComponent<UISelect>());
                break;
            case 3:
                RegisterView(GameObject.Find("UIBoard").GetComponent<UIBoard>());
                RegisterView(GameObject.Find("Map").GetComponent<Spawner>());
                RegisterView(GameObject.Find("TowerPopup").GetComponent<TowerPopup>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UICountDown").GetComponent<UICountDown>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UIWin").GetComponent<UIWin>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UILost").GetComponent<UILost>());
                RegisterView(GameObject.Find("Canvas").transform.Find("UISystem").GetComponent<UISystem>());
                break;
            case 4:
                RegisterView(GameObject.Find("UICompletes").GetComponent<UICompletes>());
                break;
        }
    }
}
