using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public GameObject front;
    public GameObject back;

    public Animator anim;

    [SerializeField]
    float fCountDownTime = 3.0f;

    // 앞면 이미지
    public SpriteRenderer FrontImage;
    // 배치될 좌표
    public Vector2 EndPos;
    public SpriteRenderer BackImage; // BackImage 변수 할당
    // 카드 효과 음악
    AudioSource audioSource;
    public AudioClip clip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayAnim());
    }

    public IEnumerator Move()
    {
        float fMoveTime = 0f;
        while (fMoveTime <= 1.0f)
        {
            fMoveTime += Time.deltaTime;
            transform.position = Vector2.Lerp(transform.position, EndPos, fMoveTime);
            yield return null;
        }
    }

    IEnumerator PlayAnim()
    {
        while (!GameManager.instance.bIsPlaying)
        {
            yield return null;
        }
        anim.SetBool("isArrive", true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setting(int number)
    {
        idx = number;
        FrontImage.sprite = Resources.Load<Sprite>($"Images/TeamEight{idx}");
    }

    public void OpenCard()
    {
        if (!GameManager.instance.bIsPlaying)
            return;
        audioSource.PlayOneShot(clip);
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        if (GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;
            //두번쨰 카드 선택까지 카운트 다운하는 코루틴 시작
            StartCoroutine(CountDown());
        }
        else if(GameManager.instance.secondCard == null)
        {
            //두번째 카드 선택시 코루틴 중단
            StopCoroutine(CountDown());
            GameManager.instance.secondCard = this;
            GameManager.instance.Matched();
        }
        else
        {
            return;
        }
    }

    //5초 후 첫번째 선택하는 카드를 되돌림
    IEnumerator CountDown()
    {
        //'fCountDownTime'초 대기 후
        yield return new WaitForSeconds(fCountDownTime);
        //해당카드를 뒤집기 위한 메소드
        CloseCard(0);
        //게임매니저의 첫번째 카드에 등록된 정보를 초기화
        GameManager.instance.firstCard = null;
    }

    //두 카드의 인덱스가 같을 시 호출되어 카드를 파괴하는 함수
    public void DestoryCard()
    {
        //DelayDestroy()코루틴을 시작해라
        StartCoroutine(DelayDestroy());
    }

    //카드 파괴를 fDelayTime만큼 지연 후 실행하는 코루틴
    IEnumerator DelayDestroy()
    {
        //딜레이 시간만큼 기다린 후
        yield return new WaitForSeconds(GameManager.instance.fDelayTime);
        //게임 오브젝트를 파괴
        Destroy(gameObject);
    }

    //두 카드의 인덱스가 다를 시 호출되어 카드를 원상태로 만드는 함수
    public void CloseCard(float fTime)
    {
        //DelayClose()코루틴을 시작해라
        StartCoroutine(DelayClose(fTime));
        BackImage.color = new Color32(255, 166, 0, 255); // 카드가 닫혔을시 지정색으로 바뀜
    }

    //카드 뒤집기를 fDelayTime만큼 지연 후 실행하는 코루틴
    IEnumerator DelayClose(float fTime)
    {
        //딜레이 시간만큼 기다린 후
        yield return new WaitForSeconds(fTime);
        //카드의 애니메이션 상태를 Idle로 되돌린다.
        anim.SetBool("isOpen", false);
        //그림이 있는 앞면 오브젝트를 비활성화 한다.
        front.SetActive(false);
        //'?'가 적힌 뒷면 오브젝트를 활성화 한다.
        back.SetActive(true);
    }
}