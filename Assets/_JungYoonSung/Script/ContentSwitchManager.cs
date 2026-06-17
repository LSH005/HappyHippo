using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace QuestScene_JungYoonSung_2023137028
{
    public class ContentSwitchManager : MonoBehaviour
    {
        [System.Serializable]
        public struct ContentData
        {
            public Sprite npcPanelSprite;
            public Sprite npcTokenSprite;
            public string questType;
            public string questTitle;
            [TextArea(3, 5)] public string questDescription;
        }

        [Header("Target UI References")]
        [SerializeField] private Image npcPanelImage;
        [SerializeField] private Image npcTokenImage;
        [SerializeField] private TextMeshProUGUI questTypeText;
        [SerializeField] private TextMeshProUGUI questTitleText;
        [SerializeField] private TextMeshProUGUI questDescriptionText;

        [Header("Content Resources")]
        [SerializeField] private ContentData questData;
        [SerializeField] private ContentData partTimeData;
        [SerializeField] private ContentData eventData;

        private void Start()
        {
            SwitchToQuest();
        }

        public void SwitchToQuest()
        {
            ApplyContent(questData);
        }

        public void SwitchToPartTime()
        {
            ApplyContent(partTimeData);
        }

        public void SwitchToEvent()
        {
            ApplyContent(eventData);
        }

        private void ApplyContent(ContentData data)
        {
            if (npcPanelImage != null) npcPanelImage.sprite = data.npcPanelSprite;
            if (npcTokenImage != null) npcTokenImage.sprite = data.npcTokenSprite;
            if (questTypeText != null) questTypeText.text = data.questType;
            if (questTitleText != null) questTitleText.text = data.questTitle;
            if (questDescriptionText != null) questDescriptionText.text = data.questDescription;
        }
    }
}