using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LeeHyunWoo
{
    public class DifficultyButton : MonoBehaviour
    {
        [System.Serializable]
        private class ButtonVisual
        {
            public Outline outline;
            public Image image;
            public TextMeshProUGUI text;
        }

        [SerializeField] private ButtonVisual[] buttons;

        [SerializeField] private int startIndex = -1;

        [Header("선택 색")]
        [SerializeField] private Color selectedImageColor = new Color(1f, 0.35f, 0.1f);
        [SerializeField] private Color selectedTextColor = Color.white;

        [Header("기본 색")]
        [SerializeField] private Color normalImageColor = new Color(0.35f, 0.15f, 0.3f);
        [SerializeField] private Color normalTextColor = new Color(1f, 0.35f, 0.1f);

        [Header("Outline")]
        [SerializeField] private Color outlineColor = Color.yellow;
        [SerializeField] private Vector2 outlineDistance = new Vector2(2f, -2f);

        private void Awake()
        {
            SelectButton(startIndex);
        }

        public void SelectButton(int index)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                ApplyState(buttons[i], i == index);
            }
        }

        private void ApplyState(ButtonVisual button, bool selected)
        {
            if (button == null)
                return;

            if (button.outline != null)
            {
                button.outline.enabled = selected;
                button.outline.effectColor = outlineColor;
                button.outline.effectDistance = outlineDistance;
                button.outline.useGraphicAlpha = true;
            }

            if (button.image != null)
                button.image.color = selected ? selectedImageColor : normalImageColor;

            if (button.text != null)
                button.text.color = selected ? selectedTextColor : normalTextColor;
        }
    }
}