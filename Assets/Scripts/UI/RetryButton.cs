using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void Retry()
    {
        string stagename = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(stagename);
    }

    public void SelectStage()
    {
        SceneManager.LoadScene("SelectStage");
    }
}