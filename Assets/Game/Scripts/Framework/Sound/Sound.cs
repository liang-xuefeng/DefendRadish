using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 声音管理类
/// </summary>
public class Sound : Singleton<Sound>
{
    /// <summary> 资源路径 </summary>
    public string ResourcesDir = "";

    private AudioSource _bgSource;
    private AudioSource _effectSource;

    /// <summary> 背景音乐大小 </summary>
    public float BgVolume
    {
        get { return _bgSource.volume; }
        set { _bgSource.volume = value; }
    }

    /// <summary> 音效大小 </summary>
    public float EffectVolume
    {
        get { return _effectSource.volume; }
        set { _effectSource.volume = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        _bgSource = gameObject.AddComponent<AudioSource>();
        _bgSource.playOnAwake = false;
        _bgSource.loop = true;

        _effectSource = gameObject.AddComponent<AudioSource>();
        _effectSource.playOnAwake = false;
        _effectSource.loop = false;
    }

    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="audioName">音乐名</param>
    public void PlayBg(string audioName)
    {
        string oldName = "";

        if (_bgSource.clip.name != null) oldName = _bgSource.clip.name;

        if (oldName != audioName)
        {
            string path;
            if (string.IsNullOrEmpty(ResourcesDir))
                path = audioName;
            else
                path = ResourcesDir + "/" + audioName;

            AudioClip clip = Resources.Load<AudioClip>(path);

            if (clip != null)
            {
                _bgSource.clip = clip;
                _bgSource.Play();
            }
        }
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopBg()
    {
        _bgSource.Stop();
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioName"></param>
    public void PlayEffect(string audioName)
    {
        string path;
        if (string.IsNullOrEmpty(ResourcesDir))
            path = audioName;
        else
            path = ResourcesDir + "/" + audioName;

        AudioClip clip = Resources.Load<AudioClip>(path);

        if (clip != null)
            _effectSource.PlayOneShot(clip);
    }
}
