using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class StageManager
{
    // 해당 스테이지로 게임시작
    public void StartStage(int stageLv)
    {
        if(stageLv == 1 || SystemManager.instance.saveData.stage[stageLv - 1].bIsClear == true)
        {
            SystemManager.ui.TransitionScene(1);
        }
    }

    // 클리어한 스테이지 상태 저장
    public void CelarStage(int stageLv)
    {
        // 클리어 상태로 변경
        SystemManager.instance.saveData.stage[stageLv - 1].bIsClear = true;
        // 제이슨파일에 저장
        SystemManager.data.SaveToJson();
    }
}
