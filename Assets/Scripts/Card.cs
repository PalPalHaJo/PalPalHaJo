using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;
    //����ī����� ����
    public GameObject front;
    //����� �ϴ� ���Ӻ���
    public GameObject back;
    //�ִϸ��̼� ����
    public Animator anim;
    //ī�� �̹��� �ҷ����� ����
    public SpriteRenderer frontImage;

    //ī���� �ִϸ����� ����
    Animator animator;
    //�ո�� �޸� ������Ʈ
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
