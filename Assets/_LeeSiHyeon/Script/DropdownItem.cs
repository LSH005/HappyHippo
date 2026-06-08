using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LeeSihyeon
{
    public class DropdownItem : MonoBehaviour
    {
        public TextMeshProUGUI textUI;
        public float actionDuration = 0.125f;
        [HideInInspector] public Dropdown root;
        RectTransform rect;
        Button button;

        private void Awake()
        {
            if (textUI == null)
            {
                Debug.LogError(gameObject.name + "은(는) TextMeshProUGUI 컴포넌트가 할당되지 않음.");
                return;
            }
            rect = GetComponent<RectTransform>();
            if (rect == null)
            {
                Debug.LogError(gameObject.name + "은(는) RectTransform 컴포넌트가 필요함.");
                return;
            }
            button = GetComponent<Button>();
            if (button == null)
            {
                Debug.LogError(gameObject.name + "은(는) Button 컴포넌트가 필요함.");
                return;
            }
            button.onClick.AddListener(OnClicked);
        }

        private void Start()
        {
            rect.DOKill();

            Vector2 scale = rect.localScale;
            scale.x = 0;
            rect.localScale = scale;

            rect.DOScaleX(1, actionDuration).SetEase(Ease.OutQuad);
        }

        public void Close()
        {
            rect.DOKill();
            Sequence close = DOTween.Sequence();
            close.SetLink(gameObject);
            close.Append(rect.DOScaleY(0, actionDuration).SetEase(Ease.InQuad));
            close.OnComplete(() => Destroy(gameObject));
        }

        void OnClicked()
        {
            if (root != null) root.SetType(textUI.text);
        }
    }
}