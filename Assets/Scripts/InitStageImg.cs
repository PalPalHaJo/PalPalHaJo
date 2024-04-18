using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InitStageImg : MonoBehaviour
{
    public int stageLv;

    private void Start()
    {
        Debug.Log(SystemManager.instance.saveData.stage.Length);
        Debug.Log(stageLv);
        if (SystemManager.instance.saveData.stage[stageLv-1].bIsClear == true)
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Lvl/lvl_block_hover"); // 해금된 이미지
            transform.GetChild(0).gameObject.SetActive(true); // 스테이지 숫자
            transform.GetChild(1).gameObject.SetActive(true); // 별 3개 이미지
        }
        else // 잠금된 상태일 때
        {
            gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Lvl/lvl_lock_block_pressed"); // 잠금된 이미지
        }
    }
}
