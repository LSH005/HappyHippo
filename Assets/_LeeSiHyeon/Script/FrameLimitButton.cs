using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LeeSihyeon
{
    public class FrameLimitButton : MonoBehaviour
    {
        public FrameLimitLevel thisButtonFrameLimit;
        public Image selectedButtonBG;

        [Header("Text")]
        public TextMeshProUGUI buttonText;
        public Color enableColor;
        public Color disableColor;

        Button button;
        Color originalBGColor;
        Color disableBGColor;

        private void Awake()
        {
            button = GetComponent<Button>();
            if (button == null) Debug.LogError(gameObject.name + "은(는) Button 컴포넌트가 필요함.");
            else button.onClick.AddListener(OnButtonClicked);

            if (selectedButtonBG == null) Debug.LogError(gameObject.name + "은(는) selectedButtonBG 가 할당되지 않음.");
            else
            {
                disableBGColor = originalBGColor = selectedButtonBG.color;
                disableBGColor.a = 0;
            }

            if (buttonText == null) Debug.LogError(gameObject.name + "은(는) buttonText 가 할당되지 않음.");
        }

        private void OnDisable()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(OnButtonClicked);
            }
        }

        private void OnButtonClicked() => FrameLimitButtonManager.Instance.SetFrameLimit(thisButtonFrameLimit);

        public void SetSelectedWithFrameLimit(FrameLimitLevel limit)
        {
            bool isSelected = limit == thisButtonFrameLimit;
            if (buttonText == null || selectedButtonBG == null)
            {
                Debug.LogError(gameObject.name + "은(는) 필요한 컴포넌트가 할당되지 않음.");
                return;
            }

            if (isSelected)
            {
                selectedButtonBG.color = originalBGColor;
                buttonText.color = enableColor;
            }
            else
            {
                selectedButtonBG.color = disableBGColor;
                buttonText.color = disableColor;
            }
        }
    }
}