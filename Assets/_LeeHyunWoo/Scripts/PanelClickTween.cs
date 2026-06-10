using DG.Tweening;
using UnityEngine;

namespace LeeHyunWoo
{
    public class PanelClickTween : MonoBehaviour
    {
        [SerializeField] private RectTransform targetPanel;

        [Header("스케일 설정")]
        [SerializeField] private float punchScale = 1.05f;
        [SerializeField] private float upTime = 0.08f;
        [SerializeField] private float downTime = 0.12f;
        [SerializeField] private Ease upEase = Ease.OutQuad;
        [SerializeField] private Ease downEase = Ease.OutBack;

        private Vector3 originScale;
        private Sequence sequence;

        private void Awake()
        {
            if (targetPanel == null)
                targetPanel = GetComponent<RectTransform>();

            originScale = targetPanel.localScale;
        }

        private void OnDisable()
        {
            sequence?.Kill();

            if (targetPanel != null)
                targetPanel.localScale = originScale;
        }

        public void PlayTween()
        {
            if (targetPanel == null)
                return;

            sequence?.Kill();
            targetPanel.localScale = originScale;

            sequence = DOTween.Sequence()
                .SetUpdate(true)
                .Append(targetPanel.DOScale(originScale * punchScale, upTime).SetEase(upEase))
                .Append(targetPanel.DOScale(originScale, downTime).SetEase(downEase));
        }
    }
}