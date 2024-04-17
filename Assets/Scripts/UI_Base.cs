using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Base : MonoBehaviour
{
    public GameObject stagePanel;
    public void OnStagePanel()
    {
        stagePanel.SetActive(true);
    }
    public void ReturnIntro()
    {
        stagePanel.SetActive(false);
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }
}
