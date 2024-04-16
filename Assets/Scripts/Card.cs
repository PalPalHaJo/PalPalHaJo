using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;

    public GameObject front;
    public GameObject back;

    public Animator anim;
    //ī�� �ı� �����ð�
    [SerializeField]
    float fDelayTime = 1.0f;

    [SerializeField]
    float fCountDownTime = 3.0f;

    public SpriteRenderer FrontImage;
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
        FrontImage.sprite = Resources.Load<Sprite>($"Images/TeamEight{idx}");
    }

    public void OpenCard()
    {
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);

        if(GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;
            //�ι��� ī�� ���ñ��� ī��Ʈ �ٿ��ϴ� �ڷ�ƾ ����
            StartCoroutine(CountDown());
        }
        else
        {
            //�ι�° ī�� ���ý� �ڷ�ƾ �ߴ�
            StopCoroutine(CountDown());
            GameManager.instance.secondCard = this;
            GameManager.instance.Matched();
        }
    }

    //5�� �� ù��° �����ϴ� ī�带 �ǵ���
    IEnumerator CountDown()
    {
        //'fCountDownTime'�� ��� ��
        yield return new WaitForSeconds(fCountDownTime);
        //���ӸŴ����� ù��° ī�忡 ��ϵ� ������ �ʱ�ȭ
        GameManager.instance.firstCard = null;
        //�ش�ī�带 ������ ���� �޼ҵ�
        CloseCard();
    }

    //�� ī���� �ε����� ���� �� ȣ��Ǿ� ī�带 �ı��ϴ� �Լ�
    public void DestoryCard()
    {
        //DelayDestroy()�ڷ�ƾ�� �����ض�
        StartCoroutine(DelayDestroy());
    }

    //ī�� �ı��� fDelayTime��ŭ ���� �� �����ϴ� �ڷ�ƾ
    IEnumerator DelayDestroy()
    {
        //������ �ð���ŭ ��ٸ� ��
        yield return new WaitForSeconds(fDelayTime);
        //���� ������Ʈ�� �ı�
        Destroy(gameObject);
    }

    //�� ī���� �ε����� �ٸ� �� ȣ��Ǿ� ī�带 �����·� ����� �Լ�
    public void CloseCard()
    {
        //DelayClose()�ڷ�ƾ�� �����ض�
        StartCoroutine(DelayClose());      
    }

    //ī�� �����⸦ fDelayTime��ŭ ���� �� �����ϴ� �ڷ�ƾ
    IEnumerator DelayClose()
    {
        //������ �ð���ŭ ��ٸ� ��
        yield return new WaitForSeconds(fDelayTime);
        //ī���� �ִϸ��̼� ���¸� Idle�� �ǵ�����.
        anim.SetBool("isOpen", false);
        //�׸��� �ִ� �ո� ������Ʈ�� ��Ȱ��ȭ �Ѵ�.
        front.SetActive(false);
        //'?'�� ���� �޸� ������Ʈ�� Ȱ��ȭ �Ѵ�.
        back.SetActive(true);
    }
}
