using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace LeeSihyeon
{
    public class ToggleSwitch : MonoBehaviour
    {
        [Header("Color")]
        public Color enableColor;
        public Color disableColor;
        [Header("Sprite")]
        public RectTransform dot;
        public Image backgroundImage;
        public float moveDuration = 0.15f;
        [Header("Start")]
        public bool EnableAsStart = true;

        [HideInInspector] public bool isEnable;

        Vector2 dotPos;
        Button button;
        System.Action<bool> toggleListener;

        private void Awake()
        {
            if ( dot != null)
            {
                dotPos = dot.anchoredPosition;
            }
            else
            {
                Debug.LogError(gameObject.name + "은(는) Dot가 없음.");
                this.enabled = false;
            }

            button = GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => Toggle());
            }
            else
            {
                Debug.LogError(gameObject.name + "은(는) Button 컴포넌트가 필요함.");
                this.enabled = false;
            }

            if (backgroundImage == null)
            {
                Debug.LogError(gameObject.name + "은(는) backgroundImage가 없음.");
                this.enabled = false;
            }
        }

        void Start() => InitEnable(EnableAsStart);

        private void InitEnable(bool enable)
        {
            isEnable = enable;
            if (backgroundImage == null || dot == null) return;

            dot.DOKill();
            backgroundImage.DOKill();

            backgroundImage.color = enable ? enableColor : disableColor;
            dot.anchoredPosition = enable ? dotPos : -dotPos;
        }

        public void Toggle(bool instant = false) => SetEnable(!isEnable, instant);
        public void SetEnable(bool enable, bool instant = false) => SetEnableState(enable, instant);
        public void SetEnableWithNoCallback(bool enable, bool instant = false) => SetEnableState(enable, instant, false);

        void SetEnableState(bool enable, bool instant, bool hasCallback = true)
        {
            if (enable == isEnable) return;
            isEnable = enable;
            if (backgroundImage == null || dot == null) return;

            dot.DOKill();
            backgroundImage.DOKill();
            Color targetColor = enable ? enableColor : disableColor;
            Vector2 targetPos = enable ? dotPos : -dotPos;

            if (instant)
            {
                dot.anchoredPosition = targetPos;
                backgroundImage.color = targetColor;
            }
            else
            {
                dot.DOAnchorPos(targetPos, moveDuration);
                backgroundImage.DOColor(targetColor, moveDuration);
            }

            if (hasCallback) toggleListener?.Invoke(isEnable);
        }

        public void AddToggleListener(System.Action<bool> listener) => toggleListener += listener;
        public void RemoveToggleListener(System.Action<bool> listener) => toggleListener -= listener;
        private void OnDestroy() => toggleListener = null;
    }
}