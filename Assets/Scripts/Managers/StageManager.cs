using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Button[] stages;

    TMP_Text stageTxt;
    int stageLv = 1;   // 현재 스테이지
    Image curStageImg; // 현재 스테이지 이미지

    // 해당 스테이지로 게임시작
    public void StartStage(Button stage, int stageLv)
    {
        if(stageLv == 1 || SystemManager.instance.saveData.stage[stageLv - 2].bIsClear == true)
        {
            SystemManager.ui.TransitionScene(1);
        }
    }

    // 스테이지 해금
    public void OpenStage(int stageLv)
    {
        curStageImg = GetComponent<Image>();
        curStageImg.sprite = Resources.Load<Sprite>("UI/Lvl/lvl_block_hover");

        // 스테이지, 별이미지 활성화
        SystemManager.instance.saveData.stage[stageLv - 1].bIsClear = true;
        stages[stageLv - 1].GetComponentInChildren<TMP_Text>().gameObject.SetActive(true);
        stages[stageLv - 1].GetComponentInChildren<Image>().gameObject.SetActive(true);

        //제이슨파일에 저장
        SystemManager.data.SaveToJson();
    }
}
