using UnityEngine.SceneManagement;
using UnityEngine;

public class Card : MonoBehaviour
{
    // public variables
    public SpriteRenderer frontImage;
    public Animator anim;

    public GameObject front;
    public GameObject back;

    public int index = 0;

    AudioSource audioSource;
    public AudioClip Flip;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // front image setting
    public void Setting(int number)
    {
        string stageName = SceneManager.GetActiveScene().name;
        string code = stageName[^2..];
       

        index = number;
        string path = $"{code}/{code}{number}";

        frontImage.sprite = Resources.Load<Sprite>(path);
    }

    // ī�� ����
    public void OpenCard()
    {
        //ī�� ������ �� �߰�
        audioSource.PlayOneShot(Flip);
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
