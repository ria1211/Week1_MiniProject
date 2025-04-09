using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Board : MonoBehaviour
{
    public GameObject card;

    void Start()
    {
        int[] arr = {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5};
        arr = arr.OrderBy(x => Random.Range(0f, 5f)).ToArray();
        for(int i = 0; i<12; i++)
        {   
            // x는 나머지, y는 몫으로 계산.(4*4) 1.4는 각각의 거리/-2.1f와 -3.0은 위치조정
            float x = (i % 4)*1.4f-2.1f;
            float y = (i / 4)*1.4f-3.0f;
           GameObject go = Instantiate(card, this.transform);
            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
        }
        GameManager.Instance.cardCount=arr.Length;
    }

}
