using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    string filePath = Path.Combine(Application.dataPath, "userData.json");

    [ContextMenu("To Json Data")]
    public void SaveToJson()
    {
        string strJsonData = JsonUtility.ToJson(SystemManager.instance.saveData);

        File.WriteAllText(filePath, strJsonData);
    }

    public void LoadToJson()
    {
        if (!File.Exists(filePath))
        {
            SaveData saveData = new SaveData();
            SaveToJson();
        }

        string jsonData = File.ReadAllText(filePath);

        SystemManager.instance.saveData = JsonUtility.FromJson<SaveData>(jsonData);
    }
}

[System.Serializable]
public class SaveData
{
    public Stage[] stage;
    public Sounds sounds;
}

[System.Serializable]
public class Sounds
{
    public bool bIsMute = false;
    public float fBgSoundSize = 0.5f;
    public float fEffectSoundSize = 0.5f;
}

[System.Serializable]
public class Stage
{
    public bool bIsClear = false;
    public float fClearTime = 0;
    public float fScore = 0;
    public int iTotalCardCnt = 0;
}