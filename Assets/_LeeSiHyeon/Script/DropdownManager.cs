using System.Collections.Generic;
using UnityEngine;

namespace LeeSihyeon
{
    public class DropdownManager : MonoBehaviour
    {
        public static DropdownManager Instance { get; private set; }
        public List<Dropdown> allDropdown = new List<Dropdown>();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void CloseDropdownItemWithException(Dropdown exception)
        {
            foreach (Dropdown dropdown in allDropdown)
            {
                if (dropdown == null || dropdown == exception) continue;
                dropdown.StartClosing(0.4f);
            }
        }

        public void CloseAllDropdowns()
        {
            foreach (Dropdown dropdown in allDropdown)
            {
                dropdown.StartClosing(0.4f);
            }
        }

        public void AddDropdown(Dropdown dropdown) => allDropdown.Add(dropdown);
        public void RemoveDropdown(Dropdown dropdown) => allDropdown.Remove(dropdown);
    }
}