using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public static SystemManager instance;

    public SaveData saveData;
    public BgmType[] bgmList;
    public SoundManager soundManager;
    // public SoundManager SM => instance.soundManager; // 간단하게 가져올 수 있게함
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            soundManager = new SoundManager();
            soundManager.Init(GetComponent<AudioSource>());
            DontDestroyOnLoad(gameObject); // 씬을 이동해도 사운드매니저가 삭제되지 않음
        }
        else // 이미 존재할 때
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        soundManager.PlayBGM(bgmList[0]);
    }

    void Update()
    {
        
    }
}
