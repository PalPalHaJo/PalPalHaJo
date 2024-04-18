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
    public GameObject[] stages;

    TMP_Text stageTxt;
    int stageLv = 1;   // 현재 스테이지
    Image curStageImg; // 현재 스테이지 이미지

    public void StartStage()
    {
        stageTxt = GetComponentInChildren<TMP_Text>();
        stageLv = int.Parse(stageTxt.text); // text -> int 형변환

        // 스테이지 1 or 선택한 스테이지가 해금 상태
        if (stageLv == 1 || SystemManager.instance.saveData.stage[stageLv - 1].bIsClear == true)
        {
            TransitionScene(stageLv);
        }
            // 선택한 스테이지가 잠금 상태(아무것도 안함)
    }

    // 스테이지 해금
    public void OpenStage(int stageLv)
    {
        curStageImg = GetComponent<Image>();
        curStageImg.sprite = Resources.Load<Sprite>("UI/Lvl/lvl_block_hover");

        // 스테이지 숫자와 별이미지 활성화
        stages[stageLv-1].GetComponentInChildren<TMP_Text>().gameObject.SetActive(true);
        stages[stageLv-1].GetComponentInChildren<Image>().gameObject.SetActive(true);
    }

    public void TransitionScene(int SceneNum)
    {
        SceneManager.LoadScene(SceneNum);
        SystemManager.sound.PlayBGM(SystemManager.instance.bgmList[SceneNum]);
    }
}
