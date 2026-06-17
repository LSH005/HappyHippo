using UnityEngine;
using UnityEngine.UI;
using TMPro; 
using System.Collections.Generic;

public class SearchFilter : MonoBehaviour
{
    [Header("UI 연결")]
    public TMP_InputField searchInput; 
    public Transform contentParent;   

    private void Start()
    {
        // 검색바에 글자를 칠 때마다 필터링 함수 실행
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