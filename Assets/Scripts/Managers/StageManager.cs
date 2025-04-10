using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public StageSlider stageSlider;
    public List<Transform> previewObjects;
    public GameObject hiddenStagePreview;

    public Button hiddenBtn;

    void Start()
    {

        if (!HiddenStageActive()) return;

        hiddenStagePreview.SetActive(true);
        hiddenBtn.interactable = HiddenStageActive();

    }

    public void LoadCurrentStage()
    {
        int index = stageSlider.GetCurrentIndex();

        string sceneName = previewObjects[index].name;
        SceneManager.LoadScene(sceneName);
    }

    public bool HiddenStageActive()
    {
        string[] clearKeys = {
        "StageYA_Clear",
        "StageDE_Clear",
        "StageKY_Clear",
        "StageYJ_Clear",
        "StageMJ_Clear"
    };

        foreach (string key in clearKeys)
        {
            if (PlayerPrefs.GetInt(key, 0) != 1)
                return false;
        }

        return true;
    }
}