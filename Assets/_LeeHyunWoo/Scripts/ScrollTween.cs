using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollTween : MonoBehaviour, IScrollHandler, IBeginDragHandler
{
    [Header("연결")]
    [SerializeField] private ScrollRect scrollRect;

    [Header("휠 설정")]
    [SerializeField] private float wheelPower = 0.05f;
    [SerializeField] private float tweenTime = 0.5f;
    [SerializeField] private Ease ease = Ease.OutCubic;

    private Tween tween;

    private void Awake()
    {
        if (scrollRect == null)
            scrollRect = GetComponent<ScrollRect>();
    }

    public void OnScroll(PointerEventData eventData)
    {
        if (scrollRect == null)
            return;

        float target = GetTargetValue(eventData.scrollDelta.y);
        MoveTo(target);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        StopTween();
    }

    private float GetTargetValue(float wheel)
    {
        float value = scrollRect.verticalNormalizedPosition - wheel * wheelPower;
        return Mathf.Clamp01(value);
    }

    private void MoveTo(float target)
    {
        StopTween();

        tween = DOTween.To(
            () => scrollRect.verticalNormalizedPosition,
            value => scrollRect.verticalNormalizedPosition = value,
            target,
            tweenTime
        )
        .SetEase(ease)
        .SetUpdate(true);
    }

    private void StopTween()
    {
        tween?.Kill();
    }

    private void OnDisable()
    {
        StopTween();
    }
}