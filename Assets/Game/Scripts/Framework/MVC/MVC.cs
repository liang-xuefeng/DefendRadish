using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 事件分发，储存
/// </summary>
public static class MVC
{
    //存储MVC
    public static Dictionary<string, Model> Models = new Dictionary<string, Model>();
    public static Dictionary<string, View> Views = new Dictionary<string, View>();
    public static Dictionary<string, Type> CommandMap = new Dictionary<string, Type>();  //事件名--控制器类型

    //注册MVC 
    public static void RegisterModel(Model model)
    {
        Models[model.Name] = model;
    }

    public static void RegisterView(View view)
    {
        if (Views.ContainsKey(view.name))
            Views.Remove(view.name);

        view.RegisterEvents();
        Views[view.Name] = view;
    }

    public static void RegisterController(string eventName, Type controllerType)
    {
        CommandMap[eventName] = controllerType;
    }

    //获取
    public static Model GetModel<T>() where T : Model
    {
        foreach (Model model in Models.Values)
        {
            if (model is T) return model;
        }
        return null;
    }

    public static View GetView<T>() where T : View
    {
        foreach (View view in Views.Values)
        {
            if (view is T) return view;
        }
        return null;
    }

    //事件处理，发送事件
    public static void SendEvent(string eventName, object data = null)
    {
        //控制器响应
        if (CommandMap.ContainsKey(eventName))
        {
            Type type = CommandMap[eventName];
            Controller controller = Activator.CreateInstance(type) as Controller;

            //控制器执行
            if (controller != null) controller.Execute(data);
        }

        //视图响应
        foreach (View view in Views.Values)
        {
            //关心某个事件
            if (view.AttationEvents.Contains(eventName))
            {
                view.HandleEvent(eventName, data);
            }
        }
    }

}
