using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LeeHyunWoo
{
    public class ButtonHoverTween : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float hoverScale = 1.05f;
        [SerializeField] private float clickScale = 0.92f;
        [SerializeField] private float time = 0.12f;
        [SerializeField] private Ease ease = Ease.OutBack;

        private Vector3 originScale;
        private Tween tween;
        private bool isHover;

        private void Awake()
        {
            originScale = transform.localScale;
        }

        private void OnDisable()
        {
            tween?.Kill();
            transform.localScale = originScale;
            isHover = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            isHover = true;
            Scale(hoverScale);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            isHover = false;
            Scale(1f);
        }

        public void OnPointerDown(PointerEventData eventData) => Scale(clickScale);

        public void OnPointerUp(PointerEventData eventData) => Scale(isHover ? hoverScale : 1f);

        private void Scale(float scale)
        {
            tween?.Kill();
            tween = transform.DOScale(originScale * scale, time).SetEase(ease).SetUpdate(true);
        }
    }
}