using UnityEngine;

namespace LeeHyunWoo
{
    public class UpdateDataButton : MonoBehaviour
    {
        [SerializeField] private int addGold = 0;
        [SerializeField] private int addExp = 0;

        [SerializeField] private bool isAddOn;
        [SerializeField] private bool isDoubleOn;

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
    }
}