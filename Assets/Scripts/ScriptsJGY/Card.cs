using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;
    public GameObject front;
    public GameObject back;
    public Animator anim;

    AudioSource audioSource;
    public AudioClip clip;

    public SpriteRenderer frontImage;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"JGY{idx}");
    }
    public void OnMouseDown()
    {
        if (GameManager.Instance.canClick == true)OpenCard();

    }
    public void OpenCard()
    {
        if (GameManager.Instance.secondCard != null) return; //Ŭ���Ұ�����or2��ī�尡 �̹� ������ ��.
        audioSource.PlayOneShot(clip);
        anim.SetBool("isOpen", true);
        front.SetActive(true);
        back.SetActive(false);


        if (GameManager.Instance.firstCard == null)
        {
            GameManager.Instance.firstCard = this;
        }
        else if(GameManager.Instance.secondCard == null)
        {
            if (GameManager.Instance.firstCard != this) // ���� ī�尡 �ƴ� ���
            {
                GameManager.Instance.secondCard = this;
                GameManager.Instance.Matched();
            }
            else
            {
                // ���� ī�尡 Ŭ���� ���, ��ġ���� �ʰ� null�� ����
                GameManager.Instance.secondCard = null;
            }            
        }        
    }
    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }
    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        front.SetActive(false);
        back.SetActive(true);
    }
}
