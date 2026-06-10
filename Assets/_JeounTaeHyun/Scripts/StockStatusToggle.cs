using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StockStatusToggle : MonoBehaviour
{
    [Header("UI 에셋 연결")]
    public Image capsuleImage;          
    public RectTransform circleTransform; 
    public TextMeshProUGUI statusText;  

    [Header("스위치 설정")]
    public Color onColor = new Color(1f, 0.43f, 0.29f);
    public Color offColor = new Color(0.5f, 0.5f, 0.5f); 

    
    public float offX = -25f; 
    public float onX = 25f;   

    private bool isStockAvailable = true; 

   
    public void OnToggleClicked()
    {
       
        isStockAvailable = !isStockAvailable;

        if (isStockAvailable)
        {
            
            capsuleImage.color = onColor;
            circleTransform.anchoredPosition = new Vector2(onX, circleTransform.anchoredPosition.y);
            if (statusText != null) statusText.text = "재고 있음";
        }
        else
        {
           
            capsuleImage.color = offColor;
            circleTransform.anchoredPosition = new Vector2(offX, circleTransform.anchoredPosition.y);
            if (statusText != null) statusText.text = "재고 없음";
        }
    }
}