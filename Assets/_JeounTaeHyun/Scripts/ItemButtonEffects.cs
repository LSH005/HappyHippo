using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;


namespace jeountaehyun
{
    public class ItemButtonEffects : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float shrinkScale = 0.95f;
        [SerializeField] private float duration = 0.1f;

        private Vector3 originalScale;

        void Start()
        {

            originalScale = transform.localScale;
           
        }


        public void OnPointerDown(PointerEventData eventData)
        {

            transform.DOKill();


            transform.DOScale(originalScale * shrinkScale, duration).SetEase(Ease.OutQuad);
        }


        public void OnPointerUp(PointerEventData eventData)
        {
            transform.DOKill();


            transform.DOScale(originalScale, duration * 1.5f).SetEase(Ease.OutBack);
        }


        void OnDestroy()
        {
            transform.DOKill();
        }
    }
}

