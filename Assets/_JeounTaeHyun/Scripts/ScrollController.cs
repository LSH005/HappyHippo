using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using DG.Tweening;

public class ScrollTweenController : MonoBehaviour, IScrollHandler 
{
    [Header("Scroll View 연결")]
    public ScrollRect scrollRect;

    [Header("설정")]
    public float duration = 0.5f;

    [Header("마우스 휠 부드러운 스크롤 설정")]
    [Tooltip("숫자가 클수록 휠을 굴렸을 때 더 많이 이동합니다.")]
    public float wheelSensitivity = 0.1f;
    [Tooltip("휠을 굴린 후 멈출 때까지의 부드러운 시간")]
    public float wheelDuration = 0.2f;

    public void ScrollToTop()
    {
        if (scrollRect == null) return;

        scrollRect.DOKill();
        scrollRect.DOVerticalNormalizedPos(1.0f, duration).SetEase(Ease.OutCubic);
    }

    public void ScrollToBottom()
    {
        if (scrollRect == null) return;

        scrollRect.DOKill();
        scrollRect.DOVerticalNormalizedPos(0.0f, duration).SetEase(Ease.OutCubic);
    }

    public void ScrollToPosition(float value)
    {
        if (scrollRect == null) return;

        scrollRect.DOKill();
        scrollRect.DOVerticalNormalizedPos(value, duration).SetEase(Ease.OutQuad);
    }

   
    public void OnScroll(PointerEventData eventData)
    {
        if (scrollRect == null) return;

       
        float wheelInput = eventData.scrollDelta.y;

        if (Mathf.Abs(wheelInput) > 0.01f)
        {
            
            scrollRect.DOKill();

            
            float targetPos = scrollRect.verticalNormalizedPosition + (wheelInput * wheelSensitivity);

            
            targetPos = Mathf.Clamp01(targetPos);

            
            scrollRect.DOVerticalNormalizedPos(targetPos, wheelDuration).SetEase(Ease.OutCubic);
        }
    }
}