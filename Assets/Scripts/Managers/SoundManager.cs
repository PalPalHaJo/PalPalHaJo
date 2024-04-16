using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 필요한 컴포넌트, 없으면 자동으로 추가됨
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{   
    // 배경음악 이름, 클립 묶음
    [System.Serializable]
    public struct BgmType
    {
        public string name;       // 음악 이름
        public AudioClip clip;    // 음악 클립
    }

    // Inspector 에표시할 배경음악 목록
    // 0 : 인트로씬 배경음악
    // 1 : 인게임씬 배경음악
    public BgmType[] BGMList;

    private AudioSource audioSource;

    Dictionary<string, AudioClip> BGM;  // 배경음악 Dictionary

    private void Start()
    {
        BGM = new Dictionary<string, AudioClip>();

        // 배경음악 목록을 딕셔너리에 등록
        for (int i = 0; i < BGMList.Length; i++)
        {
            BGM.Add(BGMList[i].name, BGMList[i].clip);
        }
    }

    public void PlayIntroBGM()
    {
        // 인트로씬 배경음악 재생
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BGM["IntroBGM"];

        audioSource.Play(); // 지속적인 재생
    }

    // 인게임씬 배경음악 재생
    public void PlayInGameBGM()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BGM["InGameBGM"];

        audioSource.Play(); // 지속적인 재생
    }
}
