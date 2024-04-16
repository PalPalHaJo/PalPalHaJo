
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //싱글톤 만들기 위한 변수
    public static GameManager instance;

    public int cardCount = 0;
    public Card firstCard;
    public Card secondCard;

    //게임 시간 셋팅하기
    public Text timeTxt;
    float time = 0.0f;

    //게임 끝내기 판넬 띄우기 변수 선언
    public GameObject endTxt;

    private void Awake()
    {
        //싱글톤 만들기
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        //게임 시작하기 위한 시간 셋팅
        Time.timeScale = 1.0f;
    }

    void Update()
    {
        //시간 흐르게 하기, 노출 시간 소숫점 두자릿수
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
