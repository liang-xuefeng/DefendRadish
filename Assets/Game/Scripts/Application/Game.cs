using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ObjectPool))]
[RequireComponent(typeof(Sound))]
[RequireComponent(typeof(StaticData))]
public class Game : ApplicationBase<Game>
{

    #region 全局访问变量
    [HideInInspector]
    public ObjectPool ObjectPool = null;
    [HideInInspector]
    public Sound Sound = null;
    [HideInInspector]
    public StaticData StaticData = null;

    #endregion 


    #region 全局方法

    /// <summary>
    /// 场景跳转
    /// </summary>
    /// <param name="level"></param>
    public void LoadScene(int level)
    {
        //---退出旧场景
        //事件参数
        SceneArgs e = new SceneArgs();
        e.SceneId = SceneManager.GetActiveScene().buildIndex;

        //发布事件
        SendEvent(Consts.E_ExitScene, e);

        //---加载新场景
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    #endregion


    /// <summary>
    /// 游戏入口
    /// </summary>
    private void Start ()
    {
        DontDestroyOnLoad(gameObject);

        ObjectPool = ObjectPool.Instance;
        Sound = Sound.Instance;
        StaticData = StaticData.Instance;

        RegisterController(Consts.E_StartUp, typeof(StartUpCommand));
        SendEvent(Consts.E_StartUp);
    }

    /// <summary>
    /// 进入一个场景会调用
    /// </summary>
    /// <param name="level"></param>
    private void OnLevelWasLoaded(int level)
    {
        //Debug.Log("OnLevelWasLoaded:" + level);

        //事件参数
        SceneArgs e = new SceneArgs();
        e.SceneId = level;
        //发布事件
        SendEvent(Consts.E_EnterScene, e);
    }
}
