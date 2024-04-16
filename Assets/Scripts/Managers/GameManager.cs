
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int cardCount = 0;
    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;

    //카드 파괴 지연시간
    public float fDelayTime = 1.0f;

    float time = 0.0f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
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

    }

    public void Matched()
    {
        if(firstCard.idx == secondCard.idx) 
        {
            firstCard.DestoryCard();
            secondCard.DestoryCard();
        }
        else
        {
            firstCard.CloseCard(fDelayTime);
            secondCard.CloseCard(fDelayTime);
        }

        firstCard = null;
        secondCard = null;
    }
}
