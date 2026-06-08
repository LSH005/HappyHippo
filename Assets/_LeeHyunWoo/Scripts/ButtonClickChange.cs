using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LeeHyunWoo
{
    public class ButtonClickChange : MonoBehaviour
    {
        [Header("버튼")]
        [SerializeField] private Image buttonImage;
        [SerializeField] private TextMeshProUGUI buttonText;

        [Header("패널")]
        [SerializeField] private Image panelImage;

        [Header("기본 값")]
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color normalTextColor = Color.black;
        [SerializeField] private Color normalPanelColor = Color.white;
        [SerializeField] private string normalText = "선택";

        [Header("선택 값")]
        [SerializeField] private Color clickedColor = Color.gray;
        [SerializeField] private Color clickedTextColor = Color.white;
        [SerializeField] private Color clickedPanelColor = Color.gray;
        [SerializeField] private string clickedText = "선택함";

        [Header("보이게 할 이미지")]
        [SerializeField] private GameObject targetImage;

        private bool isSelected;

        private void Start()
        {
            ApplyState(false);
        }

        public void OnClickButton()
        {
            isSelected = !isSelected;
            ApplyState(isSelected);
        }

        private void ApplyState(bool selected)
        {
            if (buttonImage != null)
                buttonImage.color = selected ? clickedColor : normalColor;

            if (buttonText != null)
            {
                buttonText.text = selected ? clickedText : normalText;
                buttonText.color = selected ? clickedTextColor : normalTextColor;
            }

            if (panelImage != null)
                panelImage.color = selected ? clickedPanelColor : normalPanelColor;

            if (targetImage != null)
                targetImage.SetActive(selected);
        }
    }
}