using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    int idx = 0;
    //카드의 애니메이터 변수
    Animator animator;
    //앞면과 뒷면 오브젝트
    GameObject[] cards = new GameObject[2];
    // Start is called before the first frame update
    void Start()
    {
        //애니메이터 컴포넌트 가져오기
        animator = GetComponent<Animator>();
        //이 오브젝트의 첫번째 자식인 오브젝트를 cards배열 0번째에 대입
        cards[0] = transform.GetChild(0).gameObject;
        //이 오브젝트의 두번째 자식인 오브젝트를 cards배열 1번째에 대입
        cards[1] = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OpenCard()
    {
        //파라미터 IsOpen을 true 상태로 변경하여 애니메이션이 재생되게 한다.
        animator.SetBool("IsOpen", true);
        //두번째 자식(Back)을 비활성화 한다.
        cards[1].SetActive(false);
        //첫번째 자식(Front)를 활성화 한다.
        cards[0].SetActive(true);
    }
    
    public void Setting(int number)
    {
        idx = number;
        Resources.Load<Sprite>($"TeamEight{idx}");
    }
}
