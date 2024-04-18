using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    TMP_Text stageNum;
    public void StartStage()
    {
        stageNum.text = GetComponentInChildren<TMP_Text>().text;
    }
}
