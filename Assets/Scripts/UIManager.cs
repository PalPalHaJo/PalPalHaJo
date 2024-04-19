using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager
{
    //게임 실행을 종료
    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    //오브젝트 활성화
    public void OnUIPanerl(GameObject go)
    {
        Time.timeScale = 0.0f;
        go.SetActive(true);
    }

    //오브젝트 비활성화
    public void OffUIPanerl(GameObject go)
    {
        Time.timeScale = 1.0f;
        go.SetActive(false);
    }

    // 씬전환
    public void TransitionScene(int SceneNum)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneNum);        
        SystemManager.sound.PlayBGM(SystemManager.instance.bgmList[SceneNum]);
    }
}
