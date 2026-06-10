using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace JinJooYoung
{
    public class UIAdvertisementEffect :
        MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler
    {
        [Header("Characters")]
        public RectTransform character1;
        public RectTransform character2;
        public RectTransform character3;

        [Header("Text")]
        public RectTransform titleText;

        [Header("Animation")]
        public float duration = 0.6f;

        public float textOffsetX = 300f;

        public float char1OffsetY = -50f;
        public float char2OffsetY = -80f;
        public float char3OffsetY = -110f;

        Vector2 char1OriginPos;
        Vector2 char2OriginPos;
        Vector2 char3OriginPos;

        Vector2 textOriginPos;

        Quaternion char1OriginRot;
        Quaternion char2OriginRot;
        Quaternion char3OriginRot;

        bool isPlayed = false;

        private void Awake()
        {
            char1OriginPos = character1.anchoredPosition;
            char2OriginPos = character2.anchoredPosition;
            char3OriginPos = character3.anchoredPosition;

            textOriginPos = titleText.anchoredPosition;

            char1OriginRot = character1.localRotation;
            char2OriginRot = character2.localRotation;
            char3OriginRot = character3.localRotation;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (isPlayed)
                return;

            isPlayed = true;

            PlayCharacter(
                character1,
                char1OriginPos,
                char1OffsetY);

            PlayCharacter(
                character2,
                char2OriginPos,
                char2OffsetY);

            PlayCharacter(
                character3,
                char3OriginPos,
                char3OffsetY);

            titleText.anchoredPosition =
                textOriginPos +
                Vector2.right * textOffsetX;

            MyTween.MoveTo(
                titleText,
                textOriginPos,
                duration,
                Ease.OutCubic);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            character1.DOKill();
            character2.DOKill();
            character3.DOKill();
            titleText.DOKill();

            character1.anchoredPosition =
                char1OriginPos;

            character2.anchoredPosition =
                char2OriginPos;

            character3.anchoredPosition =
                char3OriginPos;

            titleText.anchoredPosition =
                textOriginPos;

            character1.localRotation =
                char1OriginRot;

            character2.localRotation =
                char2OriginRot;

            character3.localRotation =
                char3OriginRot;

            isPlayed = false;
        }

        void PlayCharacter(
            RectTransform target,
            Vector2 originalPos,
            float offsetY)
        {
            target.DOKill();

            target.anchoredPosition =
                originalPos +
                Vector2.up * offsetY;

            target.localRotation =
                Quaternion.Euler(
                    0,
                    0,
                    Random.Range(-20f, 20f));

            MyTween.MoveTo(
                target,
                originalPos,
                duration,
                Ease.OutCubic);

            target.DOLocalRotate(
                Vector3.zero,
                duration)
                .SetEase(Ease.OutCubic);
        }
    }
}