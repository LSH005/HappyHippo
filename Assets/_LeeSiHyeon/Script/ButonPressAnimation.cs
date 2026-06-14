using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LeeSihyeon
{
    public class ButonPressAnimation : MonoBehaviour
    {
        public float pressedScale = 0.9f;
        public float reductionDuration = 0.15f;
        public float expansionDuration = 0.15f;

        Button button;
        RectTransform rect;
        Vector3 originalScale;

        private void Awake()
        {
            button = GetComponent<Button>();
            rect = GetComponent<RectTransform>();

            if (rect == null || button == null)
            {
                Debug.LogError(gameObject.name + "은(는) RectTransform 또는 Button 컴포넌트가 필요함.");
                return;
            }

            originalScale = rect.localScale;
            button.onClick.AddListener(PlayButtonEffect);
        }

        private void Start()
        {
            button?.onClick.AddListener(DropdownManager.Instance.CloseAllDropdowns);
        }

        /// <summary> 버튼 클릭 시 축소 및 확대 애니메이션 실행. </summary>
        public void PlayButtonEffect()
        {
            rect.DOKill();

            Sequence seq = DOTween.Sequence();
            seq.Append(rect.DOScale(originalScale * pressedScale, reductionDuration));
            seq.Append(rect.DOScale(originalScale, expansionDuration));
        }

        private void OnDestroy()
        {
            if (button != null) button.onClick.RemoveAllListeners();
        }
    }
}