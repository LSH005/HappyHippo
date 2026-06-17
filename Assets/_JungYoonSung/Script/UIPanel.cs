using UnityEngine;
using DG.Tweening;

namespace QuestScene_JungYoonSung_2023137028
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIPanel : MonoBehaviour
    {
        [Header("Animation Settings")]
        [SerializeField] private float duration = 0.3f;
        [SerializeField] private Ease showEase = Ease.OutBack;
        [SerializeField] private Ease hideEase = Ease.InBack;

        private CanvasGroup canvasGroup;
        private RectTransform rectTransform;
        private Vector3 originalScale;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
            originalScale = Vector3.one;
        }

        public void Show()
        {
            rectTransform.DOKill();
            canvasGroup.DOKill();

            gameObject.SetActive(true);

            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 0f;
            rectTransform.localScale = originalScale * 0.5f;

            canvasGroup.DOFade(1f, duration).SetUpdate(true);
            rectTransform.DOScale(originalScale, duration).SetEase(showEase).SetUpdate(true);
        }

        public void Hide()
        {
            rectTransform.DOKill();
            canvasGroup.DOKill();

            canvasGroup.blocksRaycasts = false;

            canvasGroup.DOFade(0f, duration).SetUpdate(true);
            rectTransform.DOScale(Vector3.zero, duration).SetEase(hideEase).SetUpdate(true)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    rectTransform.localScale = originalScale;
                });
        }

        private void OnDisable()
        {
            rectTransform.DOKill();
            canvasGroup.DOKill();
        }

        private void OnDestroy()
        {
            rectTransform.DOKill();
            canvasGroup.DOKill();
        }
    }
}