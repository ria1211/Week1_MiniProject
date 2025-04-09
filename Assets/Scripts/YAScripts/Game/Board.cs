using UnityEngine;
using System.Linq;

namespace YA
{ 
    public class Board : MonoBehaviour
    {
        public GameObject card;

        public void CreateCards()
        {
            // ·£´ı ¼ÅÇÃ
            int[] arr = {0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5};
            arr = arr.OrderBy(x => Random.Range(0f, 7f)).ToArray();

            // ÁÂÇ¥ Á¤·Ä, index ÁöÁ¤
            for (int i = 0; i < 12; i++)
            {
                GameObject go = Instantiate(card, this.transform);

                float x = (i % 4) * 1.4f - 2.1f;
                float y = (i / 4) * 1.4f - 3.0f;

                go.transform.position = new Vector2(x, y);
                go.GetComponent<Card>().Setting(arr[i]);
            }
        }


        private void Update()
        {
        
        }
    }
}