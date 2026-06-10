using UnityEngine;
using UnityEngine.UI;

namespace JinJooYoung
{
    public class UIPanelController : MonoBehaviour
    {
        [Header("Panel")]
        public CanvasGroup panelGroup;
        public RectTransform panelRect;

        [Header("Background")]
        public Image dimmedImage;
        public CanvasGroup blurGroup;

        [Header("Slide")]
        public Vector2 hiddenPosition;
        public Vector2 showPosition;

        [Header("Settings")]
        public float fadeDuration = 0.25f;
        public float slideDuration = 0.35f;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (panelGroup != null)
            {
                panelGroup.alpha = 0f;
                panelGroup.interactable = false;
                panelGroup.blocksRaycasts = false;
            }

            if (panelRect != null)
            {
                panelRect.anchoredPosition = hiddenPosition;
            }

            if (dimmedImage != null)
            {
                Color color = dimmedImage.color;
                color.a = 0f;
                dimmedImage.color = color;
            }

            if (blurGroup != null)
            {
                blurGroup.alpha = 0f;
            }
        }

        public void Open()
        {
            panelGroup.interactable = true;
            panelGroup.blocksRaycasts = true;

            if (dimmedImage != null)
            {
                MyTween.Fade(
                    dimmedImage,
                    0.7f,
                    fadeDuration);
            }

            if (blurGroup != null)
            {
                MyTween.Fade(
                    blurGroup,
                    1f,
                    fadeDuration);
            }

            MyTween.Fade(
                panelGroup,
                1f,
                fadeDuration);

            MyTween.Slide(
                panelRect,
                hiddenPosition,
                showPosition,
                slideDuration);
        }

        public void Close()
        {
            panelGroup.interactable = false;
            panelGroup.blocksRaycasts = false;

            if (dimmedImage != null)
            {
                MyTween.Fade(
                    dimmedImage,
                    0f,
                    fadeDuration);
            }

            if (blurGroup != null)
            {
                MyTween.Fade(
                    blurGroup,
                    0f,
                    fadeDuration);
            }

            MyTween.Fade(
                panelGroup,
                0f,
                fadeDuration);

            MyTween.Slide(
                panelRect,
                showPosition,
                hiddenPosition,
                slideDuration);
        }
    }
}