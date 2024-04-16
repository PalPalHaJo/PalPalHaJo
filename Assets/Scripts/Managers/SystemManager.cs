using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    public static SystemManager instance;
    
    DataManager s_DataManager = new DataManager();
    public static DataManager data { get{ return SystemManager.instance.s_DataManager; }}

    SoundManager s_SoundManager = new SoundManager();
    public static SoundManager sound { get { return SystemManager.instance.s_SoundManager; }}

    public SaveData saveData;

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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
