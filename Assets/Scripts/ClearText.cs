using UnityEngine;

public class ClearText : MonoBehaviour
{
    public static ClearText instance;
    public Animator anim;

    // ΩÃ±€≈Ê
    private void Awake()
    {
        if (instance == null) instance = this;
    }

    // æ÷¥œ∏ﬁ¿Ãº« 
    public void PlayClearAnimation()
    {
        anim.SetTrigger("Show");
    }
}