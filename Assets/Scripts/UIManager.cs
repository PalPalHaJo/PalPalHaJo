using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    public void OnUIPanerl(GameObject go)
    {
        go.SetActive(true);
    }

    public void OffUIPanerl(GameObject go)
    {
        go.SetActive(false);
    }

    public void TransitionScene(int SceneNum)
    {
        SceneManager.LoadScene(SceneNum);
        SystemManager.sound.PlayBGM(SystemManager.instance.bgmList[SceneNum]);
    }
}
