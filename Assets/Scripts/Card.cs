using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    // public variables
    public SpriteRenderer frontImage;
    public Animator anim;

    public GameObject front;
    public GameObject back;

    public int index = 0;

    // front image setting
    public void Setting(int number)
    {
        index = number;
        frontImage.sprite = Resources.Load<Sprite>($"rtan{index}");
    }

    // 카드 열기
    public void OpenCard()
    {
        // 카드 열린 상태 설정
        anim.SetBool("isOpen", true);

        // 뒤집기
        front.SetActive(true);
        back.SetActive(false);

        // 첫 번째 카드, 두 번째 카드 설정
        if (GameManager.instance.firstCard == null)
        {
            GameManager.instance.firstCard = this;
        }
        else if (GameManager.instance.secondCard == null && GameManager.instance.firstCard != this)
        {
            GameManager.instance.secondCard = this;
            GameManager.instance.isMatched();
        }
    }

    // 마우스 클릭 받기
    private void OnMouseDown()
    {
        // 카드 클릭 가능한 상태일 때만 OpenCard 실행
        if (!GameManager.instance.canClick) return;
        
        OpenCard();
    }

    // 0.5f 기다렸다가 카드 파괴
    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    // 0.5f 기다렸다가 카드 닫기
    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    void CloseCardInvoke()
    {
        // 카드 닫힌 상태 설정
        anim.SetBool("isOpen", false);

        // 카드 닫기
        front.SetActive(false);
        back.SetActive(true);
    }
}
