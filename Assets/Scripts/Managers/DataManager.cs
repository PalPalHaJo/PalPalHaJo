using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public string strFilePath;

    [ContextMenu("To Json Data")]
    public void SaveToJson()
    {
        string strJsonData = JsonUtility.ToJson(GameManager.instance.saveData);

        string path = Path.Combine(Application.dataPath, "userData.json");

        File.WriteAllText(path, strJsonData);
    }

    public void LoadToJson()
    {
        string path = Path.Combine(Application.dataPath, "userData.json");
        
        string jsonData = File.ReadAllText(path);

        GameManager.instance.saveData = JsonUtility.FromJson<saveData>(jsonData);
    }
}

[System.Serializable]
public class saveData
{
    public Stage[] stage;
}

[System.Serializable]
public class Stage
{
    public bool bIsClear = false;
    public float fClearTime = 0;
    public float fScore = 0;
}