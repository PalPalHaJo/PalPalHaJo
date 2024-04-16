using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public static SystemManager instance;
    
    DataManager s_DataManager = new DataManager();
    public static DataManager data { get{ return SystemManager.instance.s_DataManager; }}

    SoundManager s_SoundManager = new SoundManager();
    public static SoundManager sound { get { return SystemManager.instance.s_SoundManager; }}

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
        Application.targetFrameRate = 60; // 어떤 기기든지 1초에 60번만 계산될 수 있게끔 설정
        soundManager.PlayBGM(bgmList[0]); // 인트로씬 배경음악 재생
        data.LoadToJson();
    }

    void Update()
    {
        
    }
}
