using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// view基类
/// </summary>
public abstract class View : MonoBehaviour
{
    public abstract string Name { get; }

    /// <summary> 关心的事件 </summary>
    [HideInInspector]
    public List<string> AttationEvents = new List<string>();

    /// <summary> 处理事件 </summary>
    public abstract void HandleEvent(string eventName, object data);

    /// <summary> 注册事件 </summary>
    public virtual void RegisterEvents()
    {
        
    }

    /// <summary> 获取model </summary>
    protected T GetModel<T>() where T : Model
    {
        return MVC.GetModel<T>() as T;
    }

    /// <summary> 发送消息 </summary>
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}
