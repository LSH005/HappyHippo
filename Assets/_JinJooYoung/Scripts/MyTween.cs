using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace JinJooYoung
{
    public static class MyTween
    {
        //==============================
        // Fade
        //==============================

        public static Tween Fade(CanvasGroup group, float targetAlpha, float duration, Ease ease = Ease.Linear)
        {
            group.DOKill();

            return group
                .DOFade(targetAlpha, duration)
                .SetEase(ease);
        }

        public static Tween Fade(Image image, float targetAlpha, float duration, Ease ease = Ease.Linear)
        {
            image.DOKill();

            Color color = image.color;
            color.a = targetAlpha;

            return image
                .DOFade(targetAlpha, duration)
                .SetEase(ease);
        }

        //==============================
        // Scale
        //==============================

        public static Tween Scale(Transform target, Vector3 scale, float duration, Ease ease = Ease.OutBack)
        {
            target.DOKill();

            return target
                .DOScale(scale, duration)
                .SetEase(ease);
        }

        public static Tween PunchScale(Transform target, float strength = 0.2f, float duration = 0.3f)
        {
            target.DOKill();

            return target.DOPunchScale(
                Vector3.one * strength,
                duration,
                10,
                1f);
        }

        //==============================
        // Popup
        //==============================

        public static Sequence Popup(CanvasGroup group, RectTransform target, float duration = 0.3f)
        {
            group.DOKill();
            target.DOKill();

            group.alpha = 0f;
            target.localScale = Vector3.zero;

            Sequence seq = DOTween.Sequence();

            seq.Join(
                group.DOFade(1f, duration));

            seq.Join(
                target
                .DOScale(Vector3.one, duration)
                .SetEase(Ease.OutBack));

            return seq;
        }

        //==============================
        // Slide
        //==============================

        public static Tween Slide(RectTransform target, Vector2 startPos, Vector2 endPos, float duration, Ease ease = Ease.OutCubic)
        {
            target.DOKill();

            target.anchoredPosition = startPos;

            return target
                .DOAnchorPos(endPos, duration)
                .SetEase(ease);
        }

        //==============================
        // Dimmed
        //==============================

        public static Tween Dimmed(Image image, float targetAlpha, float duration)
        {
            image.DOKill();

            return image
                .DOFade(targetAlpha, duration)
                .SetEase(Ease.Linear);
        }
    }
}