using UnityEngine;
using UnityEngine.UI;

public enum GraphicQuality
{
    Low,
    Medium,
    High,
    Custom
}

namespace LeeSihyeon
{
    public class GraphicQualitySelectManager : MonoBehaviour
    {
        public static GraphicQualitySelectManager Instance { get; private set; }
        public GraphicQualitySelectButton[] allButtons;
        [Header("Preview Image")]
        public Image previewImage;
        public Sprite[] previewSprites; // Low, Medium, High, Custom 순서로 스프라이트 할당

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            SetGraphicQuality(GraphicQuality.High);
        }

        public void SetGraphicQuality(GraphicQuality quality)
        {
            foreach (var button in allButtons)
            {
                button.SetSelectedWithGraphicQuality(quality);
            }

            if (previewImage == null || previewSprites == null || previewSprites.Length == 0)
            {
                Debug.LogError("프리뷰 이미지 또는 스프라이트가 설정되지 않음.");
                return;
            }

            int index = 0;

            switch (quality)
            {
                case GraphicQuality.Medium:
                    index = 1;
                    break;
                case GraphicQuality.High:
                    index = 2;
                    break;
                case GraphicQuality.Custom:
                    index = 3;
                    break;
            }

            if (index < previewSprites.Length) previewImage.sprite = previewSprites[index];
        }
    }
}