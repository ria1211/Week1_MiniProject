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
        if (GameManager.Instance.secondCard != null) return; //클릭불가상태or2번카드가 이미 차있을 때.
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
            if (GameManager.Instance.firstCard != this) // 같은 카드가 아닌 경우
            {
                GameManager.Instance.secondCard = this;
                GameManager.Instance.Matched();
            }
            else
            {
                // 같은 카드가 클릭된 경우, 매치하지 않고 null로 변경
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
