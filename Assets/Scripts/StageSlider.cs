using UnityEngine;
using DG.Tweening;

public class StageSlider : MonoBehaviour
{
    public Transform previewContainer; // ������ �θ� ������Ʈ
    public float spacing = 6f;         // ������ �� �Ÿ�
    public float slideTime = 0.3f;     // �����̵� �ӵ�

    public int currentIndex = 0;

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
        float targetX = -currentIndex * spacing;
        Vector3 targetPos = new Vector3(targetX, 0f, 0f);
        previewContainer.DOLocalMove(targetPos, slideTime).SetEase(Ease.OutQuart);
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