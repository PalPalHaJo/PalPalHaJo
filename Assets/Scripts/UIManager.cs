using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager
{
    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void OnUIPanerl(GameObject go)
    {
        Time.timeScale = 0.0f;
        go.SetActive(true);
    }

    public void OffUIPanerl(GameObject go)
    {
        Time.timeScale = 1.0f;
        go.SetActive(false);
    }

    public void TransitionScene(int SceneNum)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneNum);
        SystemManager.sound.PlayBGM(SystemManager.instance.bgmList[SceneNum]);
    }
}
