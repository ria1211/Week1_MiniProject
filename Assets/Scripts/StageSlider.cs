using UnityEngine;
using DG.Tweening;
using System.Xml.Serialization;

public class StageSlider : MonoBehaviour
{
   
    public static float Spacing = 6f;         // ������ �� �Ÿ�
    public static float SlideTime = 0.3f;     // �����̵� �ӵ�

    public Transform previewContainer; // ������ �θ� ������Ʈ

    public int currentIndex = 0;


    private void Start()
    {
        int savedIndex = PlayerPrefs.GetInt("LastPlayerIndex", 0);
        currentIndex = savedIndex;
        SlideToCurrent();
    }

    public void SlideLeft()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            SlideToCurrent();
        }
    }

    public void SlideRight()
    {
        if (currentIndex < 4)
        {
            currentIndex++;
            SlideToCurrent();
        }
    }

    private void SlideToCurrent()
    {
        float targetX = -currentIndex * Spacing;
        Vector3 targetPos = new Vector3(targetX, 0f, 0f);
        previewContainer.transform.position = targetPos;
        //previewContainer.DOLocalMove(targetPos, SlideTime).SetEase(Ease.OutQuart);
        Debug.Log($"{targetX}, {currentIndex}, {Spacing}");
    }

    public void MoveToHidden()
    {
        currentIndex = 5;
        SlideToCurrent();
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }

    
}