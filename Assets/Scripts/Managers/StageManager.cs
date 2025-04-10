using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public StageSlider stageSlider;
    public List<Transform> previewObjects;

    public Button hiddenBtn;

    void Start()
    {

        int savedIndex = PlayerPrefs.GetInt("LastPlayedIndex", 0);
        stageSlider.ResetToIndex(savedIndex);

        if (!HiddenStageActive()) return;

        hiddenBtn.interactable = HiddenStageActive();

    }

    public void LoadCurrentStage()
    {
        int index = stageSlider.GetCurrentIndex();
        PlayerPrefs.SetInt("LastPlayedIndex", index);

        string sceneName = previewObjects[index].name;
        SceneManager.LoadScene(sceneName);
    }

    public bool HiddenStageActive()
    {
        string[] clearKeys = {
        "StageYA_Clear",
        "StageDE_Clear",
        "StageGY_Clear",
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

    public void UnlockHiddenStageDebug()
    {
        PlayerPrefs.SetInt("StageYA_Clear", 1);
        PlayerPrefs.SetInt("StageDE_Clear", 1);
        PlayerPrefs.SetInt("StageKY_Clear", 1);
        PlayerPrefs.SetInt("StageYJ_Clear", 1);
        PlayerPrefs.SetInt("StageMJ_Clear", 1);

        Debug.Log("히든 스테이지 강제 해금 완료");

        // 강제로 UI 다시 검사
        if (HiddenStageActive())
        {
            hiddenBtn.interactable = true;
        }
    }
}