using UnityEngine;
using UnityEngine.UI;

namespace QuestScene_JungYoonSung_2023137028
{
    [RequireComponent(typeof(Button))]
    public class UI_Close_Trigger : MonoBehaviour
    {
        private Button button;
        private UIPanel parentPanel;

        void Awake()
        {
            button = GetComponent<Button>();
            parentPanel = GetComponentInParent<UIPanel>();
        }

        void Start()
        {
            if (button != null && parentPanel != null)
            {
                button.onClick.AddListener(OnCloseButtonClicked);
            }
        }

        private void OnCloseButtonClicked()
        {
            if (parentPanel != null)
            {
                parentPanel.Hide();
            }
        }

        void OnDestroy()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(OnCloseButtonClicked);
            }
        }
    }
}