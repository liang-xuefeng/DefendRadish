using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saver
{
    /// <summary> 记取存档 </summary>
    public static int GetProgress()
    {
        return PlayerPrefs.GetInt(Consts.GameProgress, -1);
    }

    /// <summary> 设置存档 </summary>
    public static void SetProgress(int levelIndex)
    {
        PlayerPrefs.SetInt(Consts.GameProgress, levelIndex);
    }
}
