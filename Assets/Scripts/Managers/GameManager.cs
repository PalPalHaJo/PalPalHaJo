
using System.Collections;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameManager : MonoBehaviour
{
    //싱글톤 만들기 위한 변수
    public static GameManager instance;

    public int cardCount = 0;
    public Card firstCard;
    public Card secondCard;

    //게임 시간 셋팅하기
    public Text timeTxt;
    //30초에서 시간 줄어들기
    float time = 30.0f;
    public int cardTry = 0; //카드 매칭 시도횟수
    //이름 띄울 텍스트
    public Text nameTxt;
    public TextMeshProUGUI tryTxt; // 시도횟수 텍스트
    public TextMeshProUGUI scoreTxt; // 점수 텍스트

    //카드 파괴 지연시간
    public float fDelayTime = 1.0f;

    //게임 끝내기 END 띄우기 변수 선언
    public GameObject endPanel;

    //경고 텍스트 표시
    bool bIsWarnig = false;
    //경고 표시 시간
    float fLimitTime = 10.0f;
    //경고 텍스트 애니메이션
    public Animator anim;

    public int StageLv = 1;
    //최고 기록 텍스트
    public TextMeshProUGUI recordText;
    //게임 플레이 여부
    public bool bIsPlaying = false;

    // 카드 효과 음악
    AudioSource audioSource;
    public AudioClip correctClip;
    public AudioClip wrongClip;
    public AudioClip warningClip;

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
        audioSource = GetComponent<AudioSource>();
        string strFormat = SystemManager.instance.saveData.stage[StageLv - 1].fClearTime.ToString("N2");
        recordText.text = strFormat;
        //게임 시작하기 위한 시간 셋팅
        Time.timeScale = 1.0f;
        timeTxt.text = 30.ToString("N2");
    }

    void Update()
    {
        
        if (!bIsPlaying)
            return;
        //시간 흐르게 하기, 노출 시간 소숫점 두자릿수
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        //게임시간이 0초가 되면 멈추고 END 띄우기
        if (time <= 0.0f)
        {
            endPanel.SetActive(true);
            timeTxt.text = 0.ToString("N2");
            Time.timeScale = 0.0f;
            bIsPlaying = false;
            GameOver();
        }

        if (time <= fLimitTime && !bIsWarnig)
        {
            bIsWarnig = true;
            anim.SetBool("bIsWarning", true);
            audioSource.PlayOneShot(warningClip); // 시간 촉박 효과음
        }
    }


    public void Matched()
    {
        firstCard.StopAllCoroutines();
        // 성공
        if(firstCard.idx == secondCard.idx) 
        {
            audioSource.PlayOneShot(correctClip); // 성공 효과음
            //이름 배열 선언하기
            string[] nameArray = { "이강혁", "안보연", "김현수", "안보연", "김현수", "성지윤", "성지윤", "이강혁" };
            //이름 띄우기
            nameTxt.text = nameArray[firstCard.idx];
            time += 1.0f;
            firstCard.DestoryCard();
            secondCard.DestoryCard();
            //카드가 맞으면 cardCount 에서 2 빼기
            cardCount -= 2;
            StartCoroutine(DelayTextClear(fDelayTime)); // 이름 텍스트 지우는 코루틴
            //카드를 전부 맞추면 게임 멈추고 END 띄우기
            if (cardCount == 0)
            {
                //최단 기록 달성 시 
                if (SystemManager.instance.saveData.stage[StageLv - 1].fClearTime < time)
                {
                    //현재 남은 시간을 데이터에 기록
                    SystemManager.instance.saveData.stage[StageLv - 1].fClearTime = time;
                    //제이슨파일에 저장
                    SystemManager.data.SaveToJson();
                }
                Time.timeScale = 0.0f;
                endPanel.SetActive(true);
                GameOver();
}
        }
        else // 실패
        {
            audioSource.PlayOneShot(wrongClip); // 실패 효과음
            nameTxt.text = "실패-0.5s"; // 실패 띄우기
            cardTry += 1; // 실패시 시도횟수 추가
            time -= 0.5f;
            firstCard.CloseCard(fDelayTime);
            secondCard.CloseCard(fDelayTime);
            StartCoroutine(DelayTextClear(fDelayTime)); // 실패 텍스트 지우는 코루틴
        }
        
        //firstCard = null;
        //secondCard = null;
    }

    //END 판넬에 들어갈 시도횟수, 점수 표기
    public void GameOver()
    {
        tryTxt.text = cardTry.ToString("N0");
        scoreTxt.text = (cardTry + time - cardCount + 80).ToString("N0");
    }

    //이름과 실패 텍스트를 일정 시간 후 지워주는 코루틴
    IEnumerator DelayTextClear(float fTime2)
    {
        //일정 시간을 기다린 후
        yield return new WaitForSeconds(fTime2);
        //텍스트 값 비우기
        nameTxt.text = "";
    }
}
