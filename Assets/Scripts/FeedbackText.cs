using UnityEngine;
using UnityEngine.UI;

public class FeedbackText : MonoBehaviour
{
    public Animator anim;
    public Text feedbackText;

    public void Show(string message, Color color)
    {
        feedbackText.text = message;
        feedbackText.color = color;
        anim.SetTrigger("Pop");
    }
}