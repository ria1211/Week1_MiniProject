using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // public variables
    public static GameManager instance;

    public Animator countdownAnim;
    public Animator panelAnim;
    public AudioSource warning;

    private Tween blinkTween;

    public Text timeTxt;
    public Text countdownTxt;
    public Text feedbackTxt;
    public Text resultTxt;

    public GameObject clearPanel;
    public GameObject failedPanel;
    public GameObject startPanel;
    public GameObject textContainer;
    public GameObject boardobject;
    public GameObject panelcontainer;

    public GameObject testBtn;

    public ResultText resultText;
    public FeedbackText feedbackText;
    public Card firstCard;
    public Card secondCard;
    public Board board;

    public int cardCount = 12;
    public bool canClick = true;

    // private variables
    private Color originalColor;

    private bool isPlay = false;
    private bool isWarning = false;

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
    }

    private void Start()
    {
        // 전부 초기화
        time = 30.0f;
        originalColor = timeTxt.color;

        // 카운트다운 함수 실행
        StartCoroutine(StartCountdown());
    }

    private void Update()
    {

        // 게임이 시작된 상태일 경우에만 시간 흐르도록
        if (!isPlay) return;

        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        // 경고 시점 처리
        if (!isWarning && time <= 10f && time > 0f)
        {
            isWarning = true;
            TriggerWarningEffect();
        }

        // 게임 Failed
        if (time <= 0f)
        {
            time = 0f;

            isPlay = false;
            canClick = false;

            StartCoroutine(GameEnd("GAME OVER", failedPanel));
        }

        if (Input.GetKeyDown(KeyCode.F10))
        {
            testBtn.SetActive(!testBtn.activeSelf);
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
            feedbackText.Show("정답!", new Color(255,255,255));

            // 카드 파괴
            firstCard.DestroyCard();
            secondCard.DestroyCard();

            // 카드 카운트 감소
            cardCount -= 2;

            StartCoroutine(WaitThenEnableClick());

            // 카드 전부 뒤집으면 게임 끝내는 함수 호출
            if (cardCount == 0)
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Clear", 1);
                StartCoroutine(GameEnd("CLEAR!", clearPanel));
            }
        }
        else
        {
            feedbackText.Show("다시!", new Color(255, 255, 255));

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
            countdownAnim.SetTrigger("Pop");

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
        // 뒷배경 전부 off 
        textContainer.SetActive(false);
        boardobject.SetActive(false);

        // 소리 stop
        StopWarningEffect();
        warning.Stop();


        yield return new WaitForSeconds(0.6f);

        Time.timeScale = 0.0f;

        resultText.gameObject.SetActive(true);
        resultText.Show(message);

        yield return new WaitForSecondsRealtime(3.0f);

        panelcontainer.SetActive(true);
        resultPanel.SetActive(true);
    }

    private void TriggerWarningEffect()
    {
        warning.Play();
        timeTxt.color = new Color(1f, 0.4f, 0.5f);

        blinkTween = timeTxt.DOFade(0f, 0.3f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void StopWarningEffect()
    {
        if (blinkTween != null && blinkTween.IsActive())
        {
            blinkTween.Kill();
            timeTxt.color = originalColor;
            timeTxt.DOFade(1f, 0f);
        }
    }

    public void TriggerDebugGameClear()
    {
        StartCoroutine(GameEnd("CLEAR!", clearPanel));
        Debug.Log("DebugLog : Game Clear triggered");
    }

    public void TriggerDebugGameFailed()
    {
        StartCoroutine(GameEnd("GAME OVER", failedPanel));
        Debug.Log("DebugLog : Game failed triggered");
    }
}