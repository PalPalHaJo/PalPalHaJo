using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;
    //오픈카드로직 변수
    public GameObject front;
    //꺼줘야 하는 게임변수
    public GameObject back;
    //애니메이션 변수
    public Animator anim;
    //카드 이미지 불러오기 변수
    public SpriteRenderer frontImage;

    //카드의 애니메이터 변수
    Animator animator;
    //앞면과 뒷면 오브젝트
    GameObject[] cards = new GameObject[2];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setting(int number)
    {
      idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"TeamEight{idx}");
    }

    public void OpenCard()
    {
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        if(GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;
        }

        else
        {
            GameManager.instance.secondCard = this;
            GameManager.instance.Matched();
        }
    }

    public void DestoryCard()
    {
        Invoke("DestroyCardInvok", 1.0f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }
    public void CloseCard()
    {
        Invoke("CloseCardInvok", 1.0f);
    }

    void CloseCardInvok()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
