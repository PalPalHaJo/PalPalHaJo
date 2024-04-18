using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 배경음악 이름, 클립 묶음
[System.Serializable]
public struct BgmType
{
    public string name;       // 음악 이름
    public AudioClip clip;    // 음악 클립
}

public class SoundManager
{   
    AudioSource audioSource;

    public void Init(AudioSource audioSource)
    {
        // this.audioSource : 사운드매니저의 audioSource
        // audioSource : 시스템매니저의 audioSource
        this.audioSource = audioSource;  
    }

    public void PlayBGM(BgmType BGM)
    {
        // 인트로씬 배경음악 재생
        audioSource.clip = BGM.clip;  

        audioSource.Play(); // 지속적인 재생
    }

    public void SoundMute(AudioSource audio , bool bIsMute)
    {
         audio.mute = bIsMute;
    }

    public void VolumeControl(AudioSource audio, int nKind ,float fSize = 0.5f)
    {
        audio.volume = fSize;
        if (nKind == (int)Define.SoundAudio.Background)
        {
            SystemManager.instance.saveData.sounds.fBgSoundSize = fSize;
        }
        else if (nKind == (int)Define.SoundAudio.Effect)
        {
            SystemManager.instance.saveData.sounds.fEffectSoundSize = fSize;
        }
        SystemManager.data.SaveToJson();
    }
}
