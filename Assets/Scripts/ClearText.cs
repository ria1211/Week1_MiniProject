using UnityEngine;

public class ClearText : MonoBehaviour
{
    public static ClearText instance;
    public Animator anim;

    // �̱���
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // �ִϸ��̼� 
    public void PlayClearAnimation()
    {
        anim.SetTrigger("Show");
    }
}