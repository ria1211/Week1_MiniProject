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

        // ī��Ʈ�ٿ� �Լ� ����
        StartCoroutine(StartCountdown());
    }

    private void Update()
    {

        // ������ ���۵� ������ ��쿡�� �ð� �帣����
        if (!isPlay) return;

        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        // ���� Failed
        if (time <= 0f)
        {
            time = 0f;

            isPlay = false;
            canClick = false;

            StartCoroutine(GameEnd("GAME\nOVER", failedPanel));
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
            feedbackText.Show("����!", new Color(0.42f, 0.73f, 0.29f));

            // ī�� �ı�
            firstCard.DestroyCard();
            secondCard.DestroyCard();

            // ī�� ī��Ʈ ����
            cardCount -= 2;

            StartCoroutine(WaitThenEnableClick());

            // ī�� ���� �������� ���� ������ �Լ� ȣ��
            if (cardCount == 0)
            {
                StartCoroutine(GameEnd("CLEAR!", clearPanel));
            }
        }
        else
        {
            feedbackText.Show("�ٽ�!", new Color(1f, 0.4f, 0.5f));

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
            countdownAnimator.SetTrigger("Pop");

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
        yield return new WaitForSeconds(0.6f);

        Time.timeScale = 0.0f;

        resultText.gameObject.SetActive(true);
        resultText.Show(message);

        yield return new WaitForSecondsRealtime(3.0f);

        resultPanel.SetActive(true);

    }
}