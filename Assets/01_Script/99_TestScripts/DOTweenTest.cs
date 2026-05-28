using UnityEngine;
using DG.Tweening;

public class DOTweenTest : MonoBehaviour
{
    void Start()
    {
        RectTransform rect = GetComponent<RectTransform>();
        Vector3 originalScale = rect.localScale;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(rect.DOScale(originalScale * 0.8f, 0.3f).SetEase(Ease.OutQuad));
        sequence.Append(rect.DOScale(originalScale, 0.3f).SetEase(Ease.OutQuad));
        sequence.SetLoops(10);
    }
}
