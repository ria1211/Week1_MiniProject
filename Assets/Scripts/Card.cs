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

    // ī�� ����
    public void OpenCard()
    {
        // ī�� ���� ���� ����
        anim.SetBool("isOpen", true);

        // ������
        front.SetActive(true);
        back.SetActive(false);

        // ù ��° ī��, �� ��° ī�� ����
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

    // ���콺 Ŭ�� �ޱ�
    private void OnMouseDown()
    {
        // ī�� Ŭ�� ������ ������ ���� OpenCard ����
        if (!GameManager.instance.canClick) return;
        
        OpenCard();
    }

    // 0.5f ��ٷȴٰ� ī�� �ı�
    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 0.5f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    // 0.5f ��ٷȴٰ� ī�� �ݱ�
    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 0.5f);
    }

    void CloseCardInvoke()
    {
        // ī�� ���� ���� ����
        anim.SetBool("isOpen", false);

        // ī�� �ݱ�
        front.SetActive(false);
        back.SetActive(true);
    }
}
