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

    //음소거를 위한 메소드
    public void SoundMute(AudioSource audio , bool bIsMute)
    {
        audio.mute = bIsMute;
        SystemManager.instance.saveData.sounds.bIsMute = bIsMute;
        SystemManager.data.SaveToJson();
    }

    //배경음 혹은 효과음의 크기를 조절하고 데이터에 저장하는 메소드
    public void VolumeControl(AudioSource audio, int nKind ,float fSize = 0.5f)
    {
        //오디오 소스의 볼륨을 조절한다.
        audio.volume = fSize;
        if (nKind == (int)Define.SoundAudio.Background)
        {
            //배경음의 크기 정보를 매니저가 갖고 있는 데이터에 저장한다.
            SystemManager.instance.saveData.sounds.fBgSoundSize = fSize;
        }
        else if (nKind == (int)Define.SoundAudio.Effect)
        {
            //배경음의 크기 정보를 매니저가 갖고 있는 데이터에 저장한다.
            SystemManager.instance.saveData.sounds.fEffectSoundSize = fSize;
        }
        //json 저장파일에 매니저가 갖고 있는 데이터로 저장한다.
        SystemManager.data.SaveToJson();
    }
}
