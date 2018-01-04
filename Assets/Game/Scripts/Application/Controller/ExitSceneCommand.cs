using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSceneCommand : Controller
{
    public override void Execute(object data)
    {
        Game.Instance.ObjectPool.UnspawnAll();
    }
}
