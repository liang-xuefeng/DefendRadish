using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// model基类
/// </summary>
public abstract class Model 
{
    public abstract string Name { get; }

    /// <summary> 发送事件 </summary>
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}
