using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public StageSlider stageSlider;
    public List<Transform> previewObjects;

    public void LoadCurrentStage()
    {
        int index = stageSlider.GetCurrentIndex();

        string sceneName = previewObjects[index].name;
        SceneManager.LoadScene(sceneName);
    }
}