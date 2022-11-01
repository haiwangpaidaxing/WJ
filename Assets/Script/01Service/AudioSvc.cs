using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSvc : MonoSingle<AudioSvc>
{
    [SerializeField]
    AudioSource gameBgmAudioSource;
    [SerializeField]
    AudioSource uiAudioSource;
    public override void Init()
    {
        if (gameBgmAudioSource == null)
        {
            gameBgmAudioSource = gameObject.AddComponent<AudioSource>();
        }
        if (uiAudioSource == null)
        {
            uiAudioSource = gameObject.AddComponent<AudioSource>();
        }
        Debug.Log("音效服务初始化...");
    }


    /// <summary>
    /// 播放背景
    /// </summary>
    /// <returns></returns>
    public void PlayGameBGM(string soundEffectsName, bool loop = true)
    {
        Play(gameBgmAudioSource, SoundEffects.BGM + soundEffectsName, loop);
    }
    /// <summary>
    /// 播放UI音效
    /// </summary>
    /// <returns></returns>
    public void PlayUI(string soundEffectsPath  , bool loop = false)
    {
        Play(uiAudioSource,soundEffectsPath, loop);
    }
        
    private void Play(AudioSource aS, string path, bool loop)
    {
        AudioClip ac = ResourceSvc.Single.Load<AudioClip>(path);
        aS.clip = ac;
        aS.loop = loop;
        aS.Play();
    }

    public void StropGameBGM()
    {
        Stop(gameBgmAudioSource);
    }
    public void StropUI()
    {
        Stop(uiAudioSource);
    }
    private void Stop(AudioSource aS)
    {
        aS.Stop();
    }



}
