using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Card firstCard;
    public Card secondCard;

    public int cardCount = 0;
    float time = 30.00f;
    public Text timeTxt;
    public GameObject FailBoard;
    public GameObject ClearBoard;
    public GameObject PlayBoard;
    public GameObject CardBoard;
    private bool GStart = false; 
    public bool canClick = true;

    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip buzor;

    //타이머
    private int Timer = 0;

    public GameObject CPanel;
    public GameObject NumA;   //3
    public GameObject NumB;   //2
    public GameObject NumC;   //1
    public GameObject NumGO;

    //타이머
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Application.targetFrameRate = 60;
        
    }
    void Start()
    {
        Time.timeScale = 1.0f;
        audioSource = GetComponent<AudioSource>();
        Timer = 0;
        CPanel.SetActive(false);
        NumA.SetActive(false);
        NumB.SetActive(false);
        NumC.SetActive(false);
        NumGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //타이머 진행
        if (Timer == 0)
        {
            CPanel.SetActive(true);
        }
        if (Timer <= 200)
        {
            Timer++;
            if (Timer > 0)
            {
                NumA.SetActive(true);
            }
            if (Timer > 50)
            {
                NumB.SetActive(true);
                NumA.SetActive(false);
            }
            if (Timer > 100)
            {
                NumC.SetActive(true);
                NumB.SetActive(false);
            }
            if (Timer >= 150)
            {
                NumGO.SetActive(true);
                NumC.SetActive(false);
            }
            if (Timer >= 200)
            {
                NumGO.SetActive(false);
                CPanel.SetActive(false);
                GStart = true;
            }
        }

        //타이머가 끝나면 게임이 시작됨
        if (GStart)
        {
             if (time >= 0f)
            {
                CardBoard.SetActive(true);
                time -= Time.deltaTime;
                timeTxt.text = time.ToString("N2");
            }
            else
            {
                canClick = false;
                time = 0f;

                FailBoard.SetActive(true);
                PlayBoard.SetActive(false);
                CardBoard.SetActive(false);
                Time.timeScale = 0.0f;
                timeTxt.text = time.ToString("N2");

            }
        }

    }
    public void Matched()
    {
        canClick = false;

        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCount -= 2;
            StartCoroutine(WaitClick());

            if (cardCount == 0)
            {
                CardBoard.SetActive(false);
                ClearBoard.SetActive(true);
                PlayBoard.SetActive(false);                
                Time.timeScale = 0.0f;
            }
        }
        else
        {            
            firstCard.CloseCard();
            secondCard.CloseCard();
            audioSource.PlayOneShot(buzor);
            StartCoroutine(WaitClick());
        }
        firstCard= null;
        secondCard= null;
    }
    IEnumerator WaitClick()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        canClick = true;
    }
}
