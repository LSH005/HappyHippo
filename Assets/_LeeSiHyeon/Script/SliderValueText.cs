using TMPro;
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

        /// <summary> 슬라이더의 <paramref name="value"/>를 텍스트로 갱신 </summary>
        /// <param name="value">갱신할 슬라이더 값</param>
        void UpdateUI(float value) => textUI.text = value.ToString();
    }
}