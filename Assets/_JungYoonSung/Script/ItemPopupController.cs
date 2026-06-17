using UnityEngine;
using DG.Tweening;

namespace Scene_Quest_Á¤À±¼º_2023137028
{
    public class ItemPopupController : MonoBehaviour
    {
        [Header("Target UI")]
        [SerializeField] private GameObject itemDetailPopup; // ¶ç¿ï »ó¼¼ ¼³¸í ÆË¾÷ ¸ÅÄª
        [SerializeField] private float duration = 0.2f;      // ÆË¾÷ ¿¬Ãâ ½Ã°£

        private Vector3 originalScale;

        void Awake()
        {
            if (itemDetailPopup != null)
            {
                originalScale = itemDetailPopup.transform.localScale;
            }
        }

        public void ToggleItemPopup()
        {
            if (itemDetailPopup == null) return;
            if (itemDetailPopup.activeSelf)
            {
                itemDetailPopup.transform.DOScale(Vector3.zero, duration)
                    .SetEase(Ease.InQuad)
                    .OnComplete(() => itemDetailPopup.SetActive(false));
            }

            else
            {
                itemDetailPopup.SetActive(true);
                itemDetailPopup.transform.localScale = Vector3.zero;

                itemDetailPopup.transform.DOScale(originalScale, duration).SetEase(Ease.OutQuad);
            }
        }

        private void OnDestroy()
        {
            if (itemDetailPopup != null)
            {
                itemDetailPopup.transform.DOKill();
            }
        }
    }
}