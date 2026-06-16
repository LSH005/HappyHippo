using UnityEngine;
using TMPro;

namespace LeeHyunWoo
{
    public class CurrencyManager : MonoBehaviour
    {
        public static CurrencyManager Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI goldText;
        [SerializeField] private TextMeshProUGUI expText;

        [Header("원본 데이터")]
        [SerializeField] private int baseGold;
        [SerializeField] private int baseExp;

        [Header("난이도 배율")]
        [SerializeField] private float difficultyAddMultiplier = 1f;

        private int addGoldTotal;
        private int addExpTotal;
        private bool isDoubleOn;

        public int Gold { get; private set; }
        public int Exp { get; private set; }

        public float DifficultyAddMultiplier => difficultyAddMultiplier;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            Recalculate();
        }

        public void AddModifier(int goldAmount, int expAmount)
        {
            addGoldTotal += goldAmount;
            addExpTotal += expAmount;

            Recalculate();
        }

        public void RemoveModifier(int goldAmount, int expAmount)
        {
            addGoldTotal -= goldAmount;
            addExpTotal -= expAmount;

            Recalculate();
        }

        public void SetDouble(bool active)
        {
            isDoubleOn = active;

            Recalculate();
        }

        public void SetDifficultyAddMultiplier(float multiplier)
        {
            difficultyAddMultiplier = Mathf.Max(0f, multiplier);

            Recalculate();
            UpdateDataButton.RefreshAllRewardTexts();
        }

        private void Recalculate()
        {
            Gold = Mathf.RoundToInt((baseGold + addGoldTotal) * difficultyAddMultiplier);
            Exp = Mathf.RoundToInt((baseExp + addExpTotal) * difficultyAddMultiplier);

            if (isDoubleOn)
            {
                Gold *= 2;
                Exp *= 2;
            }

            if (Gold < 0)
                Gold = 0;

            if (Exp < 0)
                Exp = 0;

            UpdateUI();
        }

        public void SetBaseReward(int goldAmount, int expAmount, bool resetModifier = true)
        {
            baseGold = goldAmount;
            baseExp = expAmount;

            if (resetModifier)
            {
                addGoldTotal = 0;
                addExpTotal = 0;
            }

            Recalculate();
            UpdateDataButton.RefreshAllRewardTexts();
        }

        public void ResetModifier()
        {
            addGoldTotal = 0;
            addExpTotal = 0;

            Recalculate();
            UpdateDataButton.RefreshAllRewardTexts();
        }

        private void UpdateUI()
        {
            if (goldText != null)
                goldText.text = FormatNumberValue(Gold);

            if (expText != null)
                expText.text = FormatNumberValue(Exp);
        }

        public static string FormatNumberValue(int value)
        {
            if (value >= 10000)
            {
                float manValue = value / 10000f;

                if (value % 10000 == 0)
                    return ((int)manValue).ToString("N0") + "만";

                return manValue.ToString("0.#") + "만";
            }

            return value.ToString("N0");
        }
    }
}