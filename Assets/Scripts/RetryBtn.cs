using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryBtn : MonoBehaviour
{
    //게임 시작하기 메인씬 불러오기
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }
}
