using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // public variables
    public static GameManager instance;

    public int cardCount = 12;

    public Animator countdownAnimator;

    public Text timeTxt;
    public Text countdownTxt;
    public Text feedbackTxt;
    public Text resultTxt;

    public GameObject clearPanel;
    public GameObject failedPanel;
    public GameObject startPanel;

    public ResultText resultText;
    public FeedbackText feedbackText;
    public Card firstCard;
    public Card secondCard;
    public Board board;

    public bool canClick = true;

    // private variables
    private bool isPlay = false;
    private float time;


    private void Awake()
    {
        // singletone setting
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        time = 30.0f;

        // 카운트다운 함수 실행
        StartCoroutine(StartCountdown());
    }

    private void Update()
    {

        // 게임이 시작된 상태일 경우에만 시간 흐르도록
        if (!isPlay) return;

        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        // 게임 Failed
        if (time <= 0f)
        {
            time = 0f;

            isPlay = false;
            canClick = false;

            StartCoroutine(GameEnd("GAME\nOVER", failedPanel));
        }
    }

    // 게임 시작 함수
    private void StartGame()
    {

        // 게임 시작 상태 설정
        isPlay = true;

        // 카드 생성
        board.CreateCards();
    }

    // 카드 두 개가 맞는지 확인
    public void isMatched()
    {

        // 클릭 불가 상태 설정
        canClick = false;

        // 일치한다면
        if (firstCard.index == secondCard.index)
        {
            feedbackText.Show("정답!", new Color(0.42f, 0.73f, 0.29f));

            // 카드 파괴
            firstCard.DestroyCard();
            secondCard.DestroyCard();

            // 카드 카운트 감소
            cardCount -= 2;

            StartCoroutine(WaitThenEnableClick());

            // 카드 전부 뒤집으면 게임 끝내는 함수 호출
            if (cardCount == 0)
            {
                StartCoroutine(GameEnd("CLEAR!", clearPanel));
            }
        }
        else
        {
            feedbackText.Show("다시!", new Color(1f, 0.4f, 0.5f));

            // 카드 닫기
            firstCard.CloseCard();
            secondCard.CloseCard();

            StartCoroutine(WaitThenEnableClick());
        }

        // 카드 할당 비워주기
        firstCard = null;
        secondCard = null;
    }


    // 시작 전 카운트다운 함수
    IEnumerator StartCountdown()
    {
        // 게임 시간 멈춰놓기
        Time.timeScale = 0.0f;

        // 카운트다운 패널 켜기
        startPanel.SetActive(true);
        countdownTxt.gameObject.SetActive(true);

        // 글자 띄우기
        string[] messages = { " ", "3", "2", "1", "START!" };

        foreach (string msg in messages)
        {
            countdownTxt.text = msg;
            countdownAnimator.SetTrigger("Pop");

            yield return new WaitForSecondsRealtime(1.0f);
        }

        // 카운트다운 패널 끄기
        startPanel.SetActive(false);
        countdownTxt.gameObject.SetActive(false);

        // 게임 시간 흐르도록
        Time.timeScale = 1.0f;
        StartGame();
    }

    // 클릭 가능할 때까지 기다리기
    IEnumerator WaitThenEnableClick()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        canClick = true;
    }

    // 게임 종료 처리
    IEnumerator GameEnd(string message, GameObject resultPanel)
    {
        yield return new WaitForSeconds(0.6f);

        Time.timeScale = 0.0f;

        resultText.gameObject.SetActive(true);
        resultText.Show(message);

        yield return new WaitForSecondsRealtime(3.0f);

        resultPanel.SetActive(true);

    }
}