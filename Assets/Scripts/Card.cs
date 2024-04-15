using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    int idx = 0;
    //ī���� �ִϸ����� ����
    Animator animator;
    //�ո�� �޸� ������Ʈ
    GameObject[] cards = new GameObject[2];
    // Start is called before the first frame update
    void Start()
    {
        //�ִϸ����� ������Ʈ ��������
        animator = GetComponent<Animator>();
        //�� ������Ʈ�� ù��° �ڽ��� ������Ʈ�� cards�迭 0��°�� ����
        cards[0] = transform.GetChild(0).gameObject;
        //�� ������Ʈ�� �ι�° �ڽ��� ������Ʈ�� cards�迭 1��°�� ����
        cards[1] = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OpenCard()
    {
        //�Ķ���� IsOpen�� true ���·� �����Ͽ� �ִϸ��̼��� ����ǰ� �Ѵ�.
        animator.SetBool("IsOpen", true);
        //�ι�° �ڽ�(Back)�� ��Ȱ��ȭ �Ѵ�.
        cards[1].SetActive(false);
        //ù��° �ڽ�(Front)�� Ȱ��ȭ �Ѵ�.
        cards[0].SetActive(true);
    }
    
    public void Setting(int number)
    {
        idx = number;
        Resources.Load<Sprite>($"TeamEight{idx}");
    }
}
