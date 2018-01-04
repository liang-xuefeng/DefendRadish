using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float Time = 1f;
    public float OffsetY = 1f;

    void Start()
    {
        iTween.MoveBy(this.gameObject, iTween.Hash(
            "y", OffsetY,
            "easeType", iTween.EaseType.easeInOutSine,
            "loopType", iTween.LoopType.pingPong,
            "time", Time));
    }
}
