
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Text timeTxt;
    public GameObject endTxt;

    public int cardCount = 0;

    float time = 0.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        time += Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        if (time > 30.0f)
        {
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }

    }
}
