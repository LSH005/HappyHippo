using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using System.Collections.Generic;


namespace jeountaehyun
{
    public class SearchFilter : MonoBehaviour
    {
        
        public TMP_InputField searchInput;
        public Transform contentParent;

        private void Start()
        {
            
            searchInput.onValueChanged.AddListener(FilterItems);
        }

        private void FilterItems(string searchText)
        {
            searchText = searchText.ToLower();

            foreach (Transform item in contentParent)
            {

                TextMeshProUGUI itemName = item.GetComponentInChildren<TextMeshProUGUI>();

                if (itemName != null)
                {
                    bool isMatch = itemName.text.ToLower().Contains(searchText);
                    item.gameObject.SetActive(isMatch); // 포함되면 보여주고, 아니면 숨김
                }
            }
        }
    }
}
