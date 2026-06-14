using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LeeSihyeon
{
    public class Slider : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        [Header("UI")]
        [SerializeField] private RectTransform bgRect;
        [SerializeField] private RectTransform handleRect;
        [SerializeField] private Image fillImage;

        [Header("Slider Settings")]
        [SerializeField] private float minValue = 0f;
        [SerializeField] private float maxValue = 1f;
        [SerializeField] private bool isInteger = false;

        [Range(0f, 1f)]
        [SerializeField] private float rawPercent = 0.5f;

        private System.Action<float> onValueChanged;

        public float Value
        {
            get
            {
                float calculatedValue = Mathf.Lerp(minValue, maxValue, rawPercent);
                //return isInteger ? Mathf.Round(calculatedValue) : calculatedValue;
                return isInteger ? Mathf.CeilToInt(calculatedValue) : calculatedValue;
            }
            set
            {
                float clampedValue = Mathf.Clamp(value, minValue, maxValue);
                rawPercent = Mathf.InverseLerp(minValue, maxValue, clampedValue);

                UpdateSliderUI();
                onValueChanged?.Invoke(Value);
            }
        }

        private void OnValidate()
        {
            if (minValue >= maxValue) maxValue = minValue + 0.001f;
            UpdateSliderUI();
        }

        private void Start()
        {
            UpdateSliderUI();
            onValueChanged?.Invoke(Value);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            UpdateSliderFromPointer(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            UpdateSliderFromPointer(eventData);
        }
        private void UpdateSliderFromPointer(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgRect, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
            {
                float width = bgRect.rect.width;
                float adjustedX = localPoint.x + (width * bgRect.pivot.x);

                rawPercent = Mathf.Clamp01(adjustedX / width);

                UpdateSliderUI();
                onValueChanged?.Invoke(Value);
            }
        }

        private void UpdateSliderUI()
        {
            if (bgRect == null) return;

            float width = bgRect.rect.width;
            float pivotOffset = width * bgRect.pivot.x;

            float displayPercent = rawPercent;
            //if (isInteger && maxValue != minValue)
            //{
            //    displayPercent = (Value - minValue) / (maxValue - minValue);
            //}

            if (handleRect != null)
            {
                float localX = Mathf.Lerp(-pivotOffset, width - pivotOffset, displayPercent);
                handleRect.anchoredPosition = new Vector2(localX, handleRect.anchoredPosition.y);
            }

            if (fillImage != null) fillImage.fillAmount = displayPercent;
        }

        public void AddListener(System.Action<float> listener) => onValueChanged += listener;
        public void RemoveListener(System.Action<float> listener) => onValueChanged -= listener;
    }
}