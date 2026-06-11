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

        public void AddDropdown(Dropdown dropdown) => allDropdown.Add(dropdown);
        public void RemoveDropdown(Dropdown dropdown) => allDropdown.Remove(dropdown);
    }
}