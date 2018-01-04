using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameModel : Model
{
    private List<Level> _levels = new List<Level>();
    private int _playLevelIndex = -1;
    private int _gameProgress = -1;

    private int _gold = 0;
    private bool _isPlaying = false;

    /// <summary> 所有关卡数据 </summary>
    public List<Level> AlLevels { get { return _levels; } }
    /// <summary> 要玩的关卡 </summary>
    public Level PlayLevel { get { return _levels[_playLevelIndex]; } }

    /// <summary> 当前分数 </summary>
    public int  Gold { get { return _gold; } set { _gold = value; } }
    /// <summary> 是否在进行中 </summary>
    public bool IsPlaying { get { return _isPlaying; } set { _isPlaying = value; } }
    /// <summary> 关卡的数量 </summary>
    public int LevelCount { get { return _levels.Count; } }
    /// <summary> 是否通关 </summary>
    public bool IsGamePassed { get { return _gameProgress >= _levels.Count - 1; } }
    /// <summary> 当前关索引 </summary>
    public int PlayLevelIndex { get { return _playLevelIndex; } }
    /// <summary> 最大通关索引 </summary>
    public int GameProgress { get { return _gameProgress; } }

    public override string Name
    {
        get { return Consts.M_GameModel; }
    }

    /// <summary>
    /// 初始化，记取所有关卡信息
    /// </summary>
    public void Init()
    {
        _levels.Clear();
        List<FileInfo> files = Tools.GetLevelFileInfos();

        foreach (FileInfo file in files)
        {
            Level level = new Level();
            Tools.FillLevel(file.FullName, ref level);
            _levels.Add(level);
        }

        _gameProgress = 1; //Saver.GetProgress();
    }

    /// <summary>
    /// 开始关卡
    /// </summary>
    public void StartLevel(int levelIndex)
    {
        _playLevelIndex = levelIndex;
        _isPlaying = true;
    }

    /// <summary>
    /// 结束关卡
    /// </summary>
    public void StopLevel(bool isWin)
    {
        if (isWin && PlayLevelIndex > GameProgress)
        {
            _gameProgress = PlayLevelIndex;
            Saver.SetProgress(PlayLevelIndex);
        }
        _isPlaying = false;
    }

    public void Clear()
    {
        _playLevelIndex = -1;
        _gameProgress = -1;
        _isPlaying = false;
        Saver.SetProgress(_gameProgress);
    }
}
