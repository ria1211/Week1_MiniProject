using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageBtn : MonoBehaviour
{
    public void StageSelect()
    {
        SceneManager.LoadScene("MainScene");
    }
}
