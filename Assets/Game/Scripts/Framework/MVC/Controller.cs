using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// controller基类
/// </summary>
public abstract class Controller
{
    /// <summary> 获取model </summary>
    protected T GetModel<T>() where T : Model
    {
        return MVC.GetModel<T>() as T;
    }

    /// <summary> 获取view </summary>
    protected T GetView<T>() where T : View
    {
        return MVC.GetView<T>() as T;
    }

    /// <summary> 注册model </summary>
    public void RegisterModel(Model model)
    {
        MVC.RegisterModel(model);
    }

    /// <summary> 注册view </summary>
    public void RegisterView(View model)
    {
        MVC.RegisterView(model);
    }

    /// <summary> 注册Controller </summary>
    public static void RegisterController(string eventName, Type controllerType)
    {
        MVC.RegisterController(eventName, controllerType);
    }

    /// <summary> 处理事件 </summary>
    public abstract void Execute(object data);
}
