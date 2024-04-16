
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //싱글톤 만들기 위한 변수
    public static GameManager instance;

    public int tryCount = 0; //시도횟수 변수 선언
    public int cardCount = 0;
    public Card firstCard;
    public Card secondCard;

    //게임 시간 셋팅하기
    public Text timeTxt;
    //30초에서 시간 줄어들기
    float time = 30.0f;

    //게임 끝내기 END 띄우기 변수 선언
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
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        //게임시간이 0초가 되면 멈추고 END 띄우기
        if (time < 0.0f)
        {
            endTxt.SetActive(true);
            Time.timeScale = 0.0f;
        }

    }

    public void Matched()
    {
        if(firstCard.idx == secondCard.idx) 
        {
            firstCard.DestoryCard();
            secondCard.DestoryCard();
            //카드가 맞으면 cardCount 에서 2 빼기
            cardCount -= 2;
            //카드를 전부 맞추면 게임 멈추고 END 띄우기
            if (cardCount == 0)
            {
                Time.timeScale = 0.0f;
                endTxt.SetActive(true);
            }
        }
        else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
            //틀릴 경우 시간 감소하기
            time -= 0.5f;
            //틀릴 경우 시도횟수 추가하기
            tryCount += 1;
        }

        firstCard = null;
        secondCard = null;
    }
}
