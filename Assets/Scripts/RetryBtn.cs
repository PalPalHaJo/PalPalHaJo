using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryBtn : MonoBehaviour
{
    //���� �����ϱ� ���ξ� �ҷ�����
    public void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }
}
