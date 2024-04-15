
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;

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
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }
}
