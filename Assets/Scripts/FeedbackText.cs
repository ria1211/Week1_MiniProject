using UnityEngine;
using UnityEngine.UI;

public class FeedbackText : MonoBehaviour
{
    public static FeedbackText instance;

    public Animator anim;
    public Text feedbackText;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void Show(string message, Color color)
    {
        feedbackText.text = message;
        feedbackText.color = color;
        anim.SetTrigger("Pop");
    }
}