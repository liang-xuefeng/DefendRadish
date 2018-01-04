using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 程序入口类
/// </summary>
public abstract class ApplicationBase<T> : Singleton<T> where T : MonoBehaviour
{

    /// <summary> 注册Controller </summary>
    protected void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }

    /// <summary> 启动Controller </summary>
    protected void SendEvent(string eventName, object args = null)
    {
        MVC.SendEvent(eventName, args);
    }
}
