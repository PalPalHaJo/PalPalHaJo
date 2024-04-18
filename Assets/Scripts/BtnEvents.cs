using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnEvents : MonoBehaviour
{
    public Button startBtn, quitBtn, returnBtn;
    public Button stage1, stage2, stage3, stage4;
    public StageManager stageManager;
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
        stage1.onClick.AddListener(() => stageManager.StartStage(stage1.gameObject));
        stage2.onClick.AddListener(() => stageManager.StartStage(stage2.gameObject));
        stage3.onClick.AddListener(() => stageManager.StartStage(stage3.gameObject));
        stage4.onClick.AddListener(() => stageManager.StartStage(stage4.gameObject));
    }

}
