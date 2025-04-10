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
        // ���� �ʱ�ȭ
        time = 30.0f;
        originalColor = timeTxt.color;

        // ī��Ʈ�ٿ� �Լ� ����
        StartCoroutine(StartCountdown());
    }

    private void Update()
    {

        // ������ ���۵� ������ ��쿡�� �ð� �帣����
        if (!isPlay) return;

        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        // ��� ���� ó��
        if (!isWarning && time <= 10f && time > 0f)
        {
            isWarning = true;
            TriggerWarningEffect();
        }

        // ���� Failed
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

    // ���� ���� �Լ�
    private void StartGame()
    {

        // ���� ���� ���� ����
        isPlay = true;

        // ī�� ����
        board.CreateCards();
    }

    // ī�� �� ���� �´��� Ȯ��
    public void isMatched()
    {

        // Ŭ�� �Ұ� ���� ����
        canClick = false;

        // ��ġ�Ѵٸ�
        if (firstCard.index == secondCard.index)
        {
            feedbackText.Show("����!", new Color(255,255,255));

            // ī�� �ı�
            firstCard.DestroyCard();
            secondCard.DestroyCard();

            // ī�� ī��Ʈ ����
            cardCount -= 2;

            StartCoroutine(WaitThenEnableClick());

            // ī�� ���� �������� ���� ������ �Լ� ȣ��
            if (cardCount == 0)
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_Clear", 1);
                StartCoroutine(GameEnd("CLEAR!", clearPanel));
            }
        }
        else
        {
            feedbackText.Show("�ٽ�!", new Color(255, 255, 255));

            // ī�� �ݱ�
            firstCard.CloseCard();
            secondCard.CloseCard();

            StartCoroutine(WaitThenEnableClick());
        }

        // ī�� �Ҵ� ����ֱ�
        firstCard = null;
        secondCard = null;
    }


    // ���� �� ī��Ʈ�ٿ� �Լ�
    IEnumerator StartCountdown()
    {
        // ���� �ð� �������
        Time.timeScale = 0.0f;

        // ī��Ʈ�ٿ� �г� �ѱ�
        startPanel.SetActive(true);
        countdownTxt.gameObject.SetActive(true);

        // ���� ����
        string[] messages = { " ", "3", "2", "1", "START!" };

        foreach (string msg in messages)
        {
            countdownTxt.text = msg;
            countdownAnim.SetTrigger("Pop");

            yield return new WaitForSecondsRealtime(1.0f);
        }

        // ī��Ʈ�ٿ� �г� ����
        startPanel.SetActive(false);
        countdownTxt.gameObject.SetActive(false);

        // ���� �ð� �帣����
        Time.timeScale = 1.0f;
        StartGame();
    }

    // Ŭ�� ������ ������ ��ٸ���
    IEnumerator WaitThenEnableClick()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        canClick = true;
    }

    // ���� ���� ó��
    IEnumerator GameEnd(string message, GameObject resultPanel)
    {
        // �޹�� ���� off 
        textContainer.SetActive(false);
        boardobject.SetActive(false);

        // �Ҹ� stop
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