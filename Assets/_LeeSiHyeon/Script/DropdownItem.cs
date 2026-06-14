using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LeeSihyeon
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(CanvasGroup))]
    public class DropdownItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textUI;
        public float startActionDuration = 0.125f;
        [HideInInspector] public Dropdown root;
        RectTransform rect;
        CanvasGroup cg;
        Button button;
        bool isClosing;

        private void Awake()
        {
            if (textUI == null)
            {
                Debug.LogError(gameObject.name + "은(는) TextMeshProUGUI 컴포넌트가 할당되지 않음.");
                this.enabled = false;
                return;
            }
            rect = GetComponent<RectTransform>();
            button = GetComponent<Button>();
            cg = GetComponent<CanvasGroup>();

            button.onClick.AddListener(OnClicked);
            cg.alpha = 0f;
        }

        private void Start()
        {
            rect.DOKill();

            Vector2 scale = rect.localScale;
            scale.y = 0;
            rect.localScale = scale;

            rect.DOScaleY(1, startActionDuration).SetEase(Ease.OutQuad);
            cg.DOFade(1, startActionDuration).SetEase(Ease.OutQuad);
        }

        /// <summary> 항목 닫기 애니메이션 실행 후 객체 파괴 </summary>
        /// <param name="actionDuration">애니메이션 소요 시간</param>
        public void Close(float actionDuration = 0)
        {
            if (isClosing || rect == null) return;
            isClosing = true;

            rect.DOKill();
            cg.DOKill();
            Sequence seq = DOTween.Sequence();
            seq.Append(rect.DOScaleY(0, actionDuration)
                .SetEase(Ease.InQuad)
                .SetLink(gameObject));
            seq.Join(cg.DOFade(0, actionDuration)
                .SetEase(Ease.InQuad)
                .SetLink(gameObject));
            seq.OnComplete(() => Destroy(gameObject));
        }

        /// <summary> 애니메이션 없이 객체 즉시 파괴 </summary>
        public void CloseInstantly()
        {
            if (this == null || gameObject == null) return;
            if (rect != null) rect.DOKill();
            Destroy(gameObject);
        }

        /// <summary> UI에 텍스트 적용 </summary>
        /// <param name="text">표시할 문자열.</param>
        public void SetText(string text) => textUI.text = text;

        /// <summary> 항목 클릭 시 부모(<see cref="root"/>)의 텍스트 갱신. </summary>
        void OnClicked()
        {
            if (root != null && root.IsOpen) root.SetText(textUI.text);
        }

        private void OnDestroy()
        {
            if (button != null) button.onClick.RemoveAllListeners();
        }
    }
}