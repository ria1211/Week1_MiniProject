using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour
{
    public Animator anim;
    public Text resultText;

    public void Show(string message)
    {
        GetComponent<Text>().text = message;
        GetComponent<Animator>().SetTrigger("Show");
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}