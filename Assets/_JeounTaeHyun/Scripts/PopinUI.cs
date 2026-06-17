using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace jeountaehyun
{
    public class UIPanel : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        public Button confirmButton;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {

            ShowPopup();

            if (confirmButton != null)
            {
                confirmButton.onClick.AddListener(ClosePopup);
            }
        }

        public void ShowPopup()
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
        }

        public void ClosePopup()
        {

            canvasGroup.blocksRaycasts = false;


            canvasGroup.DOFade(0f, 0.5f).OnComplete(() =>
            {

                gameObject.SetActive(false);
            });
        }
    }

}
