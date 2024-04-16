using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 배경음악 이름, 클립 묶음
[System.Serializable]
public struct BgmType
{
    public string name;       // 음악 이름
    public AudioClip clip;    // 음악 클립
}

[System.Serializable]
public class SoundManager
{   
    AudioSource audioSource;

    public void Init(AudioSource audioSource)
    {
        this.audioSource = audioSource;  
    }

    public void PlayBGM(BgmType BGM)
    {
        // 인트로씬 배경음악 재생
        audioSource.clip = BGM.clip;  

        audioSource.Play(); // 지속적인 재생
    }
}
