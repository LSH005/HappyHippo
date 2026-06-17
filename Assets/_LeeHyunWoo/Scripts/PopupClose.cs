using UnityEngine;

namespace LeeHyunWoo
{
    public class PopupClose : MonoBehaviour
    {
        [Header("비활성화할 팝업 UI")]
        [SerializeField] private GameObject popupUI;

        [Header("ESC 키로 닫기")]
        [SerializeField] private bool closeWithEscape = true;

        private void Awake()
        {
            if (popupUI == null)
                popupUI = gameObject;
        }

        private void Update()
        {
            if (!closeWithEscape)
                return;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClosePopup();
            }
        }

        public void ClosePopup()
        {
            if (popupUI != null)
                popupUI.SetActive(false);
        }

        public void OpenPopup()
        {
            if (popupUI != null)
                popupUI.SetActive(true);
        }

        public void TogglePopup()
        {
            if (popupUI != null)
                popupUI.SetActive(!popupUI.activeSelf);
        }
    }
}