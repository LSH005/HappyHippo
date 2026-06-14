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

        /// <summary> <paramref name="exception"/>을 제외한 모든 <see cref="Dropdown"/> 닫기 </summary>
        /// <param name="exception">닫지 않을 예외 대상.</param>
        public void CloseDropdownItemWithException(Dropdown exception)
        {
            foreach (Dropdown dropdown in allDropdown)
            {
                if (dropdown == null || dropdown == exception) continue;
                dropdown.StartClosing(0.4f);
            }
        }

        /// <summary> <see cref="allDropdown"/>에 등록된 모든 <see cref="Dropdown"/> 닫기 </summary>
        public void CloseAllDropdowns()
        {
            foreach (Dropdown dropdown in allDropdown)
            {
                dropdown.StartClosing(0.4f);
            }
        }

        /// <summary> 관리 목록(<see cref="allDropdown"/>)에 <paramref name="dropdown"/> 추가 </summary>
        /// <param name="dropdown">추가할 대상.</param>
        public void AddDropdown(Dropdown dropdown) => allDropdown.Add(dropdown);
        /// <summary> 관리 목록(<see cref="allDropdown"/>)에서 <paramref name="dropdown"/> 제거 </summary>
        /// <param name="dropdown">제거할 대상.</param>
        public void RemoveDropdown(Dropdown dropdown) => allDropdown.Remove(dropdown);
    }
}