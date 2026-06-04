using UnityEngine;
using UnityEngine.UI;

namespace LeeSihyeon
{
    public class GraphicQualitySelectButton : MonoBehaviour
    {
        public GraphicQuality thisButtonQuality;
        public GameObject selectedButtonBG;

        Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            if (button == null)
            {
                Debug.LogError(gameObject.name + "은(는) Button 컴포넌트가 필요함.");
            }

            button.onClick.AddListener(OnButtonClicked);
        }


        private void OnDisable()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(OnButtonClicked);
            }
        }

        private void OnButtonClicked()
        {
            GraphicQualitySelectManager.Instance.SetGraphicQuality(thisButtonQuality);
        }

        public void SetSelectedWithGraphicQuality(GraphicQuality quality)
        {
            bool isSelected = quality == thisButtonQuality;
            if (selectedButtonBG != null) selectedButtonBG.SetActive(isSelected);
        }
    }
}