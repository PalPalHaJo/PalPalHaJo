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
    Card card;
    // Start is called before the first frame update
    void Start()
    {
        card = GetComponent<Card>();
        StartCoroutine(PlayAnim());
        audioSource = GameManager.instance.audioSourceCard;
    }

    //카드가 실제로 펼쳐져 이동하는 코루틴
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
        
    //게임이 시작되면 카드의 애니메이션 활성화
    IEnumerator PlayAnim()
    {
        while (!GameManager.instance.bIsPlaying)
        {
            yield return null;
        }
        anim.SetBool("isArrive", true);
    }


    public void Setting(int number)
    {
        idx = number;
        FrontImage.sprite = Resources.Load<Sprite>($"Images/TeamEight{idx}");
    }

    public void OpenCard()
    {
        //게임 플레이 중이 아니거나 두개의 카드를 뒤집은 상태라면 리턴을 통해 카드오픈을 막는다. 
        if (!GameManager.instance.bIsPlaying || (GameManager.instance.firstCard != null && GameManager.instance.secondCard != null))
            return;
        //카드 뒤집는 효과음 재생
        audioSource.PlayOneShot(clip);
        //카드 뒤집는 애니메이션 재생
        anim.SetBool("isOpen", true);
        //카드 앞면 활성화
        front.SetActive(true);
        //카드 뒷면 비활성화
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
    public IEnumerator CountDown()
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
        InitCardStorage();
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
        InitCardStorage();
        //그림이 있는 앞면 오브젝트를 비활성화 한다.
        front.SetActive(false);
        //'?'가 적힌 뒷면 오브젝트를 활성화 한다.
        back.SetActive(true);

    }

    //이 카드가 게임매니저의 카드 공간에 있는 카드와 같다면 공간을 널로 바꿔준다.
    void InitCardStorage()
    {
        if (GameManager.instance.firstCard == card)
        {
            GameManager.instance.firstCard = null;
        }
        else if(GameManager.instance.secondCard == card)
        {
            GameManager.instance.secondCard = null;
        }
    }
}