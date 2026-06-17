using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;




namespace jeountaehyun
{
    public class StockStatusToggle : MonoBehaviour
    {
        
        public Image capsuleImage;
        public RectTransform circleTransform;
        public TextMeshProUGUI statusText;

        
        public Color onColor = new Color(1f, 0.43f, 0.29f);
        public Color offColor = new Color(0.4f, 0.4f, 0.4f);

        
        public float onPositionX = 28f;
        
        public float offPositionX = -28f;

        
        public float animationDuration = 0.2f;
        public float colorTransitionDuration = 0.15f;

        private bool isStockAvailable = true;
        private Vector2 onPosition;
        private Vector2 offPosition;

        private Coroutine toggleAnimationCoroutine;

        private void Start()
        {
           
            onPosition = new Vector2(onPositionX, circleTransform.anchoredPosition.y);
            offPosition = new Vector2(offPositionX, circleTransform.anchoredPosition.y);

           
            ApplyState();
        }

        public void OnToggleClicked()
        {
            if (toggleAnimationCoroutine != null)
            {
                StopCoroutine(toggleAnimationCoroutine);
            }

            isStockAvailable = !isStockAvailable;
            toggleAnimationCoroutine = StartCoroutine(AnimateToggleState());
        }

        private IEnumerator AnimateToggleState()
        {
            float elapsedTime = 0f;
            Vector2 startingPosition = circleTransform.anchoredPosition;
            Color startingColor = capsuleImage.color;

            Vector2 targetPosition = isStockAvailable ? onPosition : offPosition;
            Color targetColor = isStockAvailable ? onColor : offColor;

            while (elapsedTime < animationDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.Clamp01(elapsedTime / animationDuration);

                circleTransform.anchoredPosition = Vector2.Lerp(startingPosition, targetPosition, t);

                if (elapsedTime <= colorTransitionDuration)
                {
                    float colorT = Mathf.Clamp01(elapsedTime / colorTransitionDuration);
                    capsuleImage.color = Color.Lerp(startingColor, targetColor, colorT);
                }

                yield return null;
            }

            circleTransform.anchoredPosition = targetPosition;
            capsuleImage.color = targetColor;

            UpdateStatusText();
            toggleAnimationCoroutine = null;
        }

        private void ApplyState()
        {
            UpdateStatusText();

            if (isStockAvailable)
            {
                capsuleImage.color = onColor;
                circleTransform.anchoredPosition = onPosition;
            }
            else
            {
                capsuleImage.color = offColor;
                circleTransform.anchoredPosition = offPosition;
            }
        }

        private void UpdateStatusText()
        {
            if (statusText != null)
                statusText.text = isStockAvailable ? "재고 있음" : "재고 없음";
        }
    }

}
