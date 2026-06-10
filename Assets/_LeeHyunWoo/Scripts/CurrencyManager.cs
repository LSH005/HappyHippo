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

        private int addGoldTotal;
        private int addExpTotal;
        private bool isDoubleOn;

        public int Gold { get; private set; }
        public int Exp { get; private set; }

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

        private void Recalculate()
        {
            Gold = baseGold + addGoldTotal;
            Exp = baseExp + addExpTotal;

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

        private void UpdateUI()
        {
            if (goldText != null)
                goldText.text = FormatNumber(Gold);

            if (expText != null)
                expText.text = FormatNumber(Exp);
        }

        private string FormatNumber(int value)
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