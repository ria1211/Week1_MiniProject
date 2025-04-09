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
            // x�� ������, y�� ������ ���.(4*4) 1.4�� ������ �Ÿ�/-2.1f�� -3.0�� ��ġ����
            float x = (i % 4)*1.4f-2.1f;
            float y = (i / 4)*1.4f-3.0f;
           GameObject go = Instantiate(card, this.transform);
            go.transform.position = new Vector2(x, y);
            go.GetComponent<Card>().Setting(arr[i]);
        }
        GameManager.Instance.cardCount=arr.Length;
    }

}
