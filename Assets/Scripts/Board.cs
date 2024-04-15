using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    [Tooltip("카드 프리팹")]
    [SerializeField] GameObject card;         // 카드 프리팹
    [Tooltip("생성할 카드의 총수량, 4배수만 가능")]
    [SerializeField] int totalCardCnt = 16;   // 총 카드 수량
    [Tooltip("카드 간격")]
    [SerializeField] float interval = 1.2f;   // 카드 간격
    [Tooltip("카드 배치 시작 X 좌표")]
    [SerializeField] float startX = -1.8f;    // 시작 x좌표
    [Tooltip("카드 배치 시작 Y 좌표")]
    [SerializeField] float startY = -1.9f;    // 시작 y좌표

    void Start()
    {

        int[] arr = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        arr = arr.OrderBy(x => Random.Range(0f, 7f).ToString()).ToArray();

        for (int i = 0; i< totalCardCnt; i++)
        {
            float x = (i % 4) * interval + startX;
            float y = (i / 4) * interval + startY;

            GameObject go = Instantiate(card, this.transform);
            go.transform.position = new Vector2(x,y);
        }
    }
}
