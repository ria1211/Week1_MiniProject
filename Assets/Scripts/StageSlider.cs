using UnityEngine;
using DG.Tweening;
using System.Xml.Serialization;

public class StageSlider : MonoBehaviour
{
   
    public static float Spacing = 6f;         // 프리뷰 간 거리
    public static float SlideTime = 0.3f;     // 슬라이드 속도

    public Transform previewContainer; // 프리뷰 부모 오브젝트

    public int currentIndex = 0;


    private void Start()
    {

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
        if (currentIndex < 3)
        {
            currentIndex++;
            SlideToCurrent();
        }
    }

    private void SlideToCurrent()
    {
        float targetX = -currentIndex * Spacing;
        Vector3 targetPos = new Vector3(targetX, 0f, 0f);
        previewContainer
            .DOLocalMove(targetPos, SlideTime)
            .SetEase(Ease.OutQuart)
            .SetUpdate(true);
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

    public void ResetToIndex(int index)
    {
        currentIndex = index;
        SlideToCurrent();
    }
}