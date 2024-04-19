using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    string filePath = Path.Combine(Application.dataPath, "userData.json");

    //Json파일로 세이브하여 정보를 저장한다.
    [ContextMenu("To Json Data")]
    public void SaveToJson()
    {
        string strJsonData = JsonUtility.ToJson(SystemManager.instance.saveData);

        File.WriteAllText(filePath, strJsonData);
    }

    //Json파일을 로드하여 정보를 가져온다.
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

//유저의 세이브파일
[System.Serializable]
public class SaveData
{
    public Stage[] stage = new Stage[4];
    public Sounds sounds = new Sounds();
}

//게임 내 사운드 정보를 저장 클래스
[System.Serializable]
public class Sounds
{
    //음소거 여부
    public bool bIsMute = false;
    //배경 사운드 크기
    public float fBgSoundSize = 0.5f;
    //효과 사운드 크기
    public float fEffectSoundSize = 0.5f;
}

//스테이지 관련 정보를 저장 클래스
[System.Serializable]
public class Stage
{
    //스테이지클리어 여부
    public bool bIsClear = false;
    //스테이지 최단 기록
    public float fClearTime = 0;
    //스테이지 점수
    public float fScore = 0;
    //스테이지 카드 개수
    public int iTotalCardCnt = 0;
}