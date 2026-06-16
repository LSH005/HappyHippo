using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LeeHyunWoo
{
    public class UpdateDataButton : MonoBehaviour
    {
        private static readonly List<UpdateDataButton> allButtons = new List<UpdateDataButton>();

        [SerializeField] private int addGold = 0;
        [SerializeField] private int addExp = 0;

        [SerializeField] private TextMeshProUGUI goldRewardText;
        [SerializeField] private TextMeshProUGUI expRewardText;

        [SerializeField] private bool isAddOn;
        [SerializeField] private bool isDoubleOn;

        private void OnEnable()
        {
            if (!allButtons.Contains(this))
                allButtons.Add(this);

            RefreshRewardText();
        }

        private void OnDisable()
        {
            allButtons.Remove(this);
        }

        private void Start()
        {
            RefreshRewardText();
        }

        public void OnClickToggleAdd()
        {
            isAddOn = !isAddOn;

            if (CurrencyManager.Instance == null)
                return;

            if (isAddOn)
            {
                CurrencyManager.Instance.AddModifier(addGold, addExp);
            }
            else
            {
                CurrencyManager.Instance.RemoveModifier(addGold, addExp);
            }
        }

        public void OnClickToggleDouble()
        {
            isDoubleOn = !isDoubleOn;

            if (CurrencyManager.Instance == null)
                return;

            CurrencyManager.Instance.SetDouble(isDoubleOn);
        }

        public void RefreshRewardText()
        {
            int gold = GetDifficultyGold();
            int exp = GetDifficultyExp();

            if (goldRewardText != null)
                goldRewardText.text = CurrencyManager.FormatNumberValue(gold);

            if (expRewardText != null)
                expRewardText.text = CurrencyManager.FormatNumberValue(exp);
        }

        private int GetDifficultyGold()
        {
            float multiplier = 1f;

            if (CurrencyManager.Instance != null)
                multiplier = CurrencyManager.Instance.DifficultyAddMultiplier;

            return Mathf.RoundToInt(addGold * multiplier);
        }

        private int GetDifficultyExp()
        {
            float multiplier = 1f;

            if (CurrencyManager.Instance != null)
                multiplier = CurrencyManager.Instance.DifficultyAddMultiplier;

            return Mathf.RoundToInt(addExp * multiplier);
        }

        public static void RefreshAllRewardTexts()
        {
            for (int i = 0; i < allButtons.Count; i++)
            {
                if (allButtons[i] != null)
                    allButtons[i].RefreshRewardText();
            }
        }
    }
}
