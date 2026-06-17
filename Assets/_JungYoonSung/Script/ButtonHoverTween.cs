using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening; 

namespace Scene_Quest_Á¤À±¼º_2023137028
{
    public class ButtonHoverTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Hover Settings")]
        [SerializeField] private Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.0f); 
        [SerializeField] private float duration = 0.2f; 
        [SerializeField] private Ease easeType = Ease.OutQuad; 

        private Vector3 originalScale; 

        void Awake()
        {
            originalScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOKill();

            transform.DOScale(hoverScale, duration).SetEase(easeType);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOKill();

            transform.DOScale(originalScale, duration).SetEase(easeType);
        }

        private void OnDisable()
        {
            transform.DOKill();
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }
    }
}