using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; 
using DG.Tweening;


namespace jeountaehyun
{
    public class ScrollTweenController : MonoBehaviour, IScrollHandler
    {
        
        public ScrollRect scrollRect;

      
        public float duration = 0.5f;

       
        public float wheelSensitivity = 0.1f;
       
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
}
