using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace JinJooYoung
{
    public static class MyTween
    {
        public enum SlideDirection
        {
            Left,
            Right,
            Top,
            Bottom
        }

        static readonly Dictionary<RectTransform, Vector3> originalScales = new();
        static readonly Dictionary<RectTransform, Vector2> originalPositions = new();

        //==============================
        // Popup
        //==============================

        public static Sequence OpenPopup(CanvasGroup canvasGroup, RectTransform popupBox, float fadeDuration = 0.25f, float scaleDuration = 0.35f)
        {
            canvasGroup.DOKill();
            popupBox.DOKill();

            canvasGroup.alpha = 0f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            popupBox.localScale = Vector3.zero;

            Sequence seq = DOTween.Sequence();

            seq.Join(canvasGroup.DOFade(1f, fadeDuration));
            seq.Join(
                popupBox.DOScale(Vector3.one, scaleDuration)
                .SetEase(Ease.OutBack));

            return seq;
        }

        public static Sequence ClosePopup(CanvasGroup canvasGroup, RectTransform popupBox, float fadeDuration = 0.2f, float scaleDuration = 0.2f)
        {
            canvasGroup.DOKill();
            popupBox.DOKill();

            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            Sequence seq = DOTween.Sequence();

            seq.Join(canvasGroup.DOFade(0f, fadeDuration));
            seq.Join(
                popupBox.DOScale(Vector3.zero, scaleDuration)
                .SetEase(Ease.InBack));

            return seq;
        }

        //==============================
        // Fade
        //==============================

        public static Tween FadeIn(CanvasGroup canvasGroup, float duration)
        {
            canvasGroup.DOKill();

            canvasGroup.alpha = 0f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            return canvasGroup.DOFade(1f, duration);
        }

        public static Tween FadeOut(CanvasGroup canvasGroup, float duration)
        {
            canvasGroup.DOKill();

            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            return canvasGroup.DOFade(0f, duration);
        }

        //==============================
        // Slide
        //==============================

        public static Tween SlideIn(RectTransform rect, SlideDirection direction, float distance = 1000f, float duration = 0.4f)
        {
            rect.DOKill();

            if (!originalPositions.ContainsKey(rect))
            {
                originalPositions.Add(
                    rect,
                    rect.anchoredPosition);
            }

            Vector2 targetPos =
                originalPositions[rect];

            Vector2 startPos =
                targetPos;

            switch (direction)
            {
                case SlideDirection.Left:
                    startPos += Vector2.left * distance;
                    break;

                case SlideDirection.Right:
                    startPos += Vector2.right * distance;
                    break;

                case SlideDirection.Top:
                    startPos += Vector2.up * distance;
                    break;

                case SlideDirection.Bottom:
                    startPos += Vector2.down * distance;
                    break;
            }

            rect.anchoredPosition = startPos;

            return rect
                .DOAnchorPos(targetPos, duration)
                .SetEase(Ease.OutBack);
        }

        public static Tween SlideOut(RectTransform rect, SlideDirection direction, float distance = 1000f, float duration = 0.3f)
        {
            rect.DOKill();

            if (!originalPositions.ContainsKey(rect))
            {
                originalPositions.Add(
                    rect,
                    rect.anchoredPosition);
            }

            Vector2 targetPos =
                originalPositions[rect];

            switch (direction)
            {
                case SlideDirection.Left:
                    targetPos += Vector2.left * distance;
                    break;

                case SlideDirection.Right:
                    targetPos += Vector2.right * distance;
                    break;

                case SlideDirection.Top:
                    targetPos += Vector2.up * distance;
                    break;

                case SlideDirection.Bottom:
                    targetPos += Vector2.down * distance;
                    break;
            }

            return rect
                .DOAnchorPos(targetPos, duration)
                .SetEase(Ease.InBack);
        }

        //==============================
        // Hover
        //==============================

        public static Tween HoverEnter(RectTransform rect, float targetScale = 1.1f, float duration = 0.15f)
        {
            rect.DOKill();

            if (!originalScales.ContainsKey(rect))
            {
                originalScales.Add(
                    rect,
                    rect.localScale);
            }

            Vector3 originScale =
                originalScales[rect];

            return rect
                .DOScale(originScale * targetScale, duration)
                .SetEase(Ease.OutCirc);
        }

        public static Tween HoverExit(RectTransform rect, float duration = 0.15f)
        {
            rect.DOKill();

            if (!originalScales.ContainsKey(rect))
                return null;

            return rect
                .DOScale(originalScales[rect], duration)
                .SetEase(Ease.OutCirc);
        }

        //==============================
        // Scale
        //==============================

        public static Tween Scale(RectTransform rect, Vector3 targetScale, float duration)
        {
            rect.DOKill();

            return rect
                .DOScale(targetScale, duration);
        }

        //==============================
        // Position
        //==============================

        public static void SavePosition(RectTransform rect)
        {
            if (!originalPositions.ContainsKey(rect))
            {
                originalPositions.Add(
                    rect,
                    rect.anchoredPosition);
            }
        }

        public static Tween MoveToOrigin(RectTransform rect, float duration)
        {
            rect.DOKill();

            if (!originalPositions.ContainsKey(rect))
                return null;

            return rect
                .DOAnchorPos(originalPositions[rect], duration);
        }

        //==============================
        // Init
        //==============================

        public static void InitPopup(CanvasGroup canvasGroup, RectTransform popupBox)
        {
            canvasGroup.DOKill();
            popupBox.DOKill();

            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            popupBox.localScale = Vector3.zero;
        }

        public static void InitCanvasGroup(CanvasGroup canvasGroup)
        {
            canvasGroup.DOKill();

            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public static void InitScale(RectTransform rect)
        {
            rect.DOKill();

            rect.localScale = Vector3.zero;
        }

        public static void InitScale(RectTransform rect, Vector3 scale)
        {
            rect.DOKill();

            rect.localScale = scale;
        }

        public static void InitPosition(RectTransform rect, Vector2 position)
        {
            rect.DOKill();

            rect.anchoredPosition = position;
        }

        public static void InitImageAlpha(Image image)
        {
            image.DOKill();

            Color color = image.color;
            color.a = 0f;
            image.color = color;
        }

        public static void InitImageAlpha(Image image, float alpha)
        {
            image.DOKill();

            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}