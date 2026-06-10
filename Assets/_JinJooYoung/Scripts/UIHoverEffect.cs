using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JinJooYoung
{
    public class UIHoverEffect :
        MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler
    {
        [Header("Hover")]
        public Vector3 hoverScale = Vector3.one * 1.1f;

        public float duration = 0.15f;

        Vector3 originalScale;

        private void Awake()
        {
            originalScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            MyTween.Scale(
                transform,
                hoverScale,
                duration,
                Ease.OutCirc);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            MyTween.Scale(
                transform,
                originalScale,
                duration,
                Ease.OutCirc);
        }
    }
}