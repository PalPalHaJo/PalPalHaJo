
using System.Collections;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using Unity.Collections;

public class GameManager : MonoBehaviour
{
    //싱글톤 만들기 위한 변수
    public static GameManager instance;

    public int cardCount = 0;
    public Card firstCard;
    public Card secondCard;

    //게임 시간 셋팅하기
    public Text timeTxt;
    //타이머 시계
    public Image timerImg;
    //30초에서 시간 줄어들기
    float time = 0;
    float fTotalTime = 0;
    
    public int cardTry = 0; //카드 매칭 시도횟수
    //이름 띄울 텍스트
    public Text nameTxt;
    public TextMeshProUGUI tryTxt; // 시도횟수 텍스트
    public TextMeshProUGUI scoreTxt; // 점수 텍스트
    public Camera mainCamera; //배경화면을 조절할 카메라

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
    public AudioSource audioSourceCaution , audioSourceCard;
    public AudioClip correctClip;
    public AudioClip wrongClip;
    public AudioClip warningClip;

    //UI요소 이벤트 넣기위한 변수
    public Button[] retryBtn, stageBtn;
    public Button settingBtn, closeBtn;
    public Toggle muteToggle;
    public Slider bgSoundSlider, effectSoundSlider;
    //환경설정 화면
    public GameObject SettingPanel;
    private void Awake()
    {
        //싱글톤 만들기
        if (instance == null)
        {
            instance = this;
        }
        EventInit();
    }
    void Start()
    {
        StageLv = SystemManager.instance.StageLv;
        SetCamera();        
        string strFormat = SystemManager.instance.saveData.stage[StageLv - 1].fClearTime.ToString("N2");
        recordText.text = strFormat;
        //게임 시작하기 위한 시간 셋팅
        Time.timeScale = 1.0f;
        timeTxt.text = fTotalTime.ToString("N2");
    }

    public void SetCamera()
    {
        time = fTotalTime = StageLv * 20.0f;
        Camera cam = Camera.main;
        if(StageLv == 1) 
        {            
            cam.orthographicSize = 5;
            cam.transform.position = new Vector3(cam.transform.position.x, -1.5f ,cam.transform.position.z);
        }
        else if(StageLv == 2)
        {
            Camera.main.orthographicSize = 5;
            cam.transform.position = new Vector3(cam.transform.position.x, 0, cam.transform.position.z);
        }
        else if(StageLv == 3)
        {
            Camera.main.orthographicSize = 6;
            cam.transform.position = new Vector3(cam.transform.position.x, 1.5f , cam.transform.position.z);
        }
        else
        {
            Camera.main.orthographicSize = 8;
            cam.transform.position = new Vector3(cam.transform.position.x, 3, cam.transform.position.z);
        }
    }
    void Update()
    {
        
        if (!bIsPlaying)
            return;
        //시간 흐르게 하기, 노출 시간 소숫점 두자릿수
        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");
        timerImg.fillAmount = time / fTotalTime;
        //게임시간이 0초가 되면 멈추고 END 띄우기
        if (time <= 0.0f)
        {
            endPanel.SetActive(true);
            timeTxt.text = 0.ToString("N2");
            Time.timeScale = 0.0f;
            bIsPlaying = false;
            GameOver();
        }

        //제한시간보다 현재시간이 작고 경고가 활성화 되지 않았다면
        if (time <= fLimitTime && !bIsWarnig)
        {
            //경고값 참으로 변경
            bIsWarnig = true;
            //경고 애니메이션 실행
            anim.SetBool("bIsWarning", true);
            audioSourceCaution.PlayOneShot(warningClip); // 시간 촉박 효과음
        }
    }

    //각 버튼의 클리과 슬라이더 및 토글의 값 변화에 따른 실행 이벤트 삽입
    void EventInit()
    {
        retryBtn[0].onClick.AddListener(() => SystemManager.ui.TransitionScene(1));
        retryBtn[1].onClick.AddListener(() => SystemManager.ui.TransitionScene(1));
        settingBtn.onClick.AddListener(() => SystemManager.ui.OnUIPanerl(SettingPanel));
        stageBtn[0].onClick.AddListener(() => SystemManager.ui.TransitionScene(0));
        stageBtn[1].onClick.AddListener(() => SystemManager.ui.TransitionScene(0));
        closeBtn.onClick.AddListener(() => SystemManager.ui.OffUIPanerl(SettingPanel));
        muteToggle.onValueChanged.AddListener(delegate { SystemManager.sound.SoundMute(audioSourceCaution, muteToggle.isOn); });
        muteToggle.onValueChanged.AddListener(delegate { SystemManager.sound.SoundMute(audioSourceCard, muteToggle.isOn); });
        muteToggle.onValueChanged.AddListener(delegate { SystemManager.sound.SoundMute(SystemManager.instance.sysAudio, muteToggle.isOn); });
        bgSoundSlider.onValueChanged.AddListener(delegate { SystemManager.sound.VolumeControl(SystemManager.instance.sysAudio, 1, bgSoundSlider.value); });
        effectSoundSlider.onValueChanged.AddListener(
            delegate { SystemManager.sound.VolumeControl(audioSourceCaution, 2, effectSoundSlider.value);
                       SystemManager.sound.VolumeControl(audioSourceCard, 2, effectSoundSlider.value);
                      });
        SetLoadUI();
    }

    void SetLoadUI()
    {
        Sounds sounds  =  SystemManager.instance.saveData.sounds;
        muteToggle.isOn = sounds.bIsMute;
        bgSoundSlider.value = sounds.fBgSoundSize;
        effectSoundSlider.value = sounds.fEffectSoundSize;
    }

    public void Matched()
    {
        firstCard.StopAllCoroutines();
        // 성공
        if(firstCard.idx == secondCard.idx) 
        {
            audioSourceCard.PlayOneShot(correctClip); // 성공 효과음
            //이름 배열 선언하기
            string[] nameArray = { "이강혁", "안보연", "김현수", "안보연", "김현수", "성지윤", "성지윤", "이강혁" , "성지윤", "김현수", "안보연", "이강혁", "성지윤", "김현수", "안보연", "이강혁" };
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
                SystemManager.stage.CelarStage(StageLv);
                endPanel.SetActive(true);
                GameOver();
            }
        }
        else // 실패
        {
            audioSourceCard.PlayOneShot(wrongClip); // 실패 효과음
            nameTxt.text = "실패-0.5s"; // 실패 띄우기
            StartCoroutine(FailBackgroundColor());
            cardTry += 1; // 실패시 시도횟수 추가
            time -= 0.5f;
            firstCard.CloseCard(fDelayTime);
            secondCard.CloseCard(fDelayTime);
            StartCoroutine(DelayTextClear(fDelayTime)); // 실패 텍스트 지우는 코루틴
        }
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

    IEnumerator FailBackgroundColor()
    {
        //원래 배경색을 저장하고
        Color originalColor = mainCamera.backgroundColor;
        //실패시 색 변경
        mainCamera.backgroundColor = Color.red;
        //일정시간 대기
        yield return new WaitForSeconds(0.5f);
        //원래대로 복구
        mainCamera.backgroundColor = originalColor;
    }
}
