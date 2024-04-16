using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 필요한 컴포넌트, 없으면 자동으로 추가됨
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // 싱글톤
    
    // 사운드 관련
    AudioSource audioSource;
    public AudioClip clip;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬을 이동해도 사운드매니저가 삭제되지 않음
        }
        else // 이미 존재할 때
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = this.clip;

        audioSource.Play(); // 지속적인 재생
    }
}
