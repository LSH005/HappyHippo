using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace QuestScene_JungYoonSung_2023137028
{
    public class ItemPopupController : MonoBehaviour
    {
        [Header("Target UI")]
        [SerializeField] private GameObject itemDetailPopup;
        [SerializeField] private float duration = 0.2f;

        private Vector3 originalScale = Vector3.one;
        private CanvasGroup popupCanvasGroup;

        void Awake()
        {
            if (itemDetailPopup != null)
            {
                popupCanvasGroup = itemDetailPopup.GetComponent<CanvasGroup>();
                if (popupCanvasGroup == null)
                {
                    popupCanvasGroup = itemDetailPopup.AddComponent<CanvasGroup>();
                }
            }
        }

        public void ToggleItemPopup()
        {
            if (itemDetailPopup == null) return;

            itemDetailPopup.transform.DOKill();
            if (popupCanvasGroup != null) popupCanvasGroup.DOKill();

            if (itemDetailPopup.activeSelf && itemDetailPopup.transform.localScale.x > 0.1f)
            {
                popupCanvasGroup.blocksRaycasts = false;
                popupCanvasGroup.DOFade(0f, duration).SetUpdate(true);
                itemDetailPopup.transform.DOScale(Vector3.zero, duration)
                    .SetEase(Ease.InQuad)
                    .SetUpdate(true)
                    .OnComplete(() => itemDetailPopup.SetActive(false));
            }
            else
            {
                itemDetailPopup.SetActive(true);
                popupCanvasGroup.alpha = 0f;
                itemDetailPopup.transform.localScale = originalScale * 0.5f;

                popupCanvasGroup.blocksRaycasts = true;
                popupCanvasGroup.DOFade(1f, duration).SetUpdate(true);
                itemDetailPopup.transform.DOScale(originalScale, duration)
                    .SetEase(Ease.OutQuad)
                    .SetUpdate(true);
            }
        }

        private void OnDisable()
        {
            if (itemDetailPopup != null) itemDetailPopup.transform.DOKill();
            if (popupCanvasGroup != null) popupCanvasGroup.DOKill();
        }

        private void OnDestroy()
        {
            if (itemDetailPopup != null) itemDetailPopup.transform.DOKill();
            if (popupCanvasGroup != null) popupCanvasGroup.DOKill();
        }
    }
}