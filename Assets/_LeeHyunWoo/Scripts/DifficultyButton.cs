using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace LeeHyunWoo
{
    public enum DifficultyType
    {
        Normal,
        Hard,
        VeryHard
    }

    public class DifficultyButton : MonoBehaviour
    {
        [System.Serializable]
        private class ButtonVisual
        {
            public DifficultyType difficultyType;

            public Outline outline;
            public Image image;
            public TextMeshProUGUI text;
        }

        [SerializeField] private ButtonVisual[] buttons;

        [SerializeField] private int startIndex = 0;

        [Header("난이도 배율")]
        [SerializeField] private float normalMultiplier = 1f;
        [SerializeField] private float hardMultiplier = 3.5f;
        [SerializeField] private float veryHardMultiplier = 8f;

        [Header("난이도별 레벨 텍스트")]
        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private string normalLevelText = "35";
        [SerializeField] private string hardLevelText = "45";
        [SerializeField] private string veryHardLevelText = "65";

        [Header("선택 색")]
        [SerializeField] private Color selectedImageColor = new Color(1f, 0.35f, 0.1f);
        [SerializeField] private Color selectedTextColor = Color.white;

        [Header("기본 색")]
        [SerializeField] private Color normalImageColor = new Color(0.35f, 0.15f, 0.3f);
        [SerializeField] private Color normalTextColor = new Color(1f, 0.35f, 0.1f);

        [Header("Outline")]
        [SerializeField] private Color outlineColor = Color.yellow;
        [SerializeField] private Vector2 outlineDistance = new Vector2(2f, -2f);

        private int currentIndex = -1;

        private void Start()
        {
            SelectButton(startIndex);
        }

        public void SelectButton(int index)
        {
            currentIndex = index;

            for (int i = 0; i < buttons.Length; i++)
            {
                ApplyState(buttons[i], i == index);
            }

            if (index < 0 || index >= buttons.Length)
                return;

            DifficultyType selectedDifficulty = buttons[index].difficultyType;
            float multiplier = GetDifficultyMultiplier(selectedDifficulty);

            if (CurrencyManager.Instance != null)
            {
                CurrencyManager.Instance.SetDifficultyAddMultiplier(multiplier);
            }

            ApplyLevelText(selectedDifficulty);
        }

        private float GetDifficultyMultiplier(DifficultyType difficultyType)
        {
            switch (difficultyType)
            {
                case DifficultyType.Normal:
                    return normalMultiplier;

                case DifficultyType.Hard:
                    return hardMultiplier;

                case DifficultyType.VeryHard:
                    return veryHardMultiplier;

                default:
                    return 1f;
            }
        }

        private void ApplyLevelText(DifficultyType difficultyType)
        {
            if (levelText == null)
                return;

            switch (difficultyType)
            {
                case DifficultyType.Normal:
                    levelText.text = normalLevelText;
                    break;

                case DifficultyType.Hard:
                    levelText.text = hardLevelText;
                    break;

                case DifficultyType.VeryHard:
                    levelText.text = veryHardLevelText;
                    break;
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