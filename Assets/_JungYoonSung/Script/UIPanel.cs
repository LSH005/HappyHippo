using UnityEngine;
using DG.Tweening;

namespace Scene_Quest_Á¤À±¼º_2023137028
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

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            rectTransform = GetComponent<RectTransform>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            canvasGroup.alpha = 0f;
            rectTransform.localScale = Vector3.one * 0.8f;
            canvasGroup.DOFade(1f, duration);
            rectTransform.DOScale(Vector3.one, duration).SetEase(showEase);
        }

        public void Hide()
        {
            canvasGroup.DOFade(0f, duration);
            rectTransform.DOScale(Vector3.one * 0.8f, duration).SetEase(hideEase)
                .OnComplete(() => gameObject.SetActive(false));
        }

        private void OnDestroy()
        {
            canvasGroup.DOKill();
            rectTransform.DOKill();
        }
    }
}