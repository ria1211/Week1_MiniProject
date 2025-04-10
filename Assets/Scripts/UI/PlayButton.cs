using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void PlaySelectStage()
    {
        SceneManager.LoadScene("SelectStage");
    }
}
