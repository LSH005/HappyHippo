using UnityEngine;
using DG.Tweening;

namespace LeeHyunWoo
{
    public class ButtonToggle : MonoBehaviour
    {
        [SerializeField] private CanvasGroup[] canvasGroups;
        [SerializeField] private float fadeDuration = 0.3f;
        [SerializeField] private bool startVisible = false;

        private bool isVisible;

        private void Awake()
        {
            isVisible = startVisible;

            foreach (CanvasGroup cg in canvasGroups)
            {
                SetInstant(cg, isVisible);
            }
        }

        public void OnClickToggle()
        {
            isVisible = !isVisible;

            foreach (CanvasGroup cg in canvasGroups)
            {
                SetVisible(cg, isVisible);
            }
        }

        private void SetVisible(CanvasGroup cg, bool visible)
        {
            if (cg == null)
                return;

            cg.DOKill();

            if (visible)
            {
                cg.gameObject.SetActive(true);
                cg.alpha = 0f;
            }

            cg.interactable = visible;
            cg.blocksRaycasts = visible;

            cg.DOFade(visible ? 1f : 0f, fadeDuration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    if (!visible)
                        cg.gameObject.SetActive(false);
                });
        }

        private void SetInstant(CanvasGroup cg, bool visible)
        {
            if (cg == null)
                return;

            cg.alpha = visible ? 1f : 0f;
            cg.interactable = visible;
            cg.blocksRaycasts = visible;
            cg.gameObject.SetActive(visible);
        }
    }
}