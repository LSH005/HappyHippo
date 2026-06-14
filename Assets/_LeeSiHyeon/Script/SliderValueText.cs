using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace LeeSihyeon
{
    public class SliderValueText : MonoBehaviour
    {
        [Header("Slider")]
        public Slider slider;

        [Header("output")]
        public TextMeshProUGUI textUI;

        private void Awake()
        {
            slider.AddListener(UpdateUI);
        }

        void UpdateUI(float value)
        {
            textUI.text = value.ToString();
        }
    }
}