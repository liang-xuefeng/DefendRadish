using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownCommand : Controller
{
    public override void Execute(object data)
    {
        //出怪
        RoundModel roundModel = GetModel<RoundModel>();
        roundModel.StartRound();
    }
}
