using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UIElements;

public class Board : MonoBehaviour
{
    [Tooltip("카드 프리팹")]
    public GameObject card;         // 카드 프리팹
    [Tooltip("생성할 카드의 총수량, 4배수만 가능")]
    [SerializeField] int totalCardCnt = 16;   // 총 카드 수량
    [Tooltip("카드 간격")]
    [SerializeField] float interval = 1.2f;   // 카드 간격
    [Tooltip("카드 배치 시작 X 좌표")]
    [SerializeField] float startX = -1.8f;    // 시작 x좌표
    [Tooltip("카드 배치 시작 Y 좌표")]
    [SerializeField] float startY = -1.9f;    // 시작 y좌표

    List<GameObject> cards = new List<GameObject>();

    void Start()
    {
        // 카드 숫자 배열 생성
        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        // 카드 숫자 배열을 무작위로 섞는다
        ShuffleArray(arr);

        // 카드 배치, 한 줄당 4장씩
        for (int i = 0; i < totalCardCnt; i++)
        {
            // 좌표 계산
            float x = (i % 4) * interval + startX;
            float y = (i / 4) * interval + startY;

            GameObject go = Instantiate(card, this.transform); // 임시 변수
            go.GetComponent<Card>().EndPos = new Vector2(x, y); // 배치
            go.GetComponent<Card>().Setting(arr[i]);
            cards.Add(go);
        }

        //게임끝내기 위한 배열 수 읽어오기
        GameManager.instance.cardCount = arr.Length;

        StartCoroutine(ThrowAboutCard());
    }

    IEnumerator ThrowAboutCard()
    {
        for(int i = 0; i < cards.Count; i++) 
        {
            StartCoroutine(cards[i].GetComponent<Card>().Move());
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1f);
        GameManager.instance.bIsPlaying = true;
    }

    // Fisher-Yates 셔플 알고리즘을 사용하여 무작위로 섞는 함수
    void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n; i++)
        {
            // 현재 인덱스부터 배열 끝까지 중에서 무작위로 다음 인덱스를 선택
            int r = i + Random.Range(0, n - i);
            // 선택된 인덱스의 값과 현재 인덱스의 값을 교환
            T temp = array[r];
            array[r] = array[i];
            array[i] = temp;
        }
    }
}