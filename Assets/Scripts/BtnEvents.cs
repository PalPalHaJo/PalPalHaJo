using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnEvents : MonoBehaviour
{
    public Button startBtn, quitBtn, returnBtn, initDataBtn;
    public Button stage1, stage2, stage3, stage4;
    public GameObject stagePanel;
    // Start is called before the first frame update
    void Start()
    {
        EventInit();
    }

    //각종 이벤트 UI에 해당하는 이벤트 삽입
    public void EventInit()
    {
        startBtn.onClick.AddListener(() => SystemManager.ui.OnUIPanerl(stagePanel));
        quitBtn.onClick.AddListener(() => SystemManager.ui.OnApplicationQuit());
        returnBtn.onClick.AddListener(() => SystemManager.ui.OffUIPanerl(stagePanel));
        stage1.onClick.AddListener(() => SystemManager.stage.StartStage(1));
        stage2.onClick.AddListener(() => SystemManager.stage.StartStage(2));
        stage3.onClick.AddListener(() => SystemManager.stage.StartStage(3));
        stage4.onClick.AddListener(() => SystemManager.stage.StartStage(4));
    }
}
