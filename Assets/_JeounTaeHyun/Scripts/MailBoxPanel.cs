using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; 

namespace jeountaehyun
{
    public class MailboxPanel : MonoBehaviour
    {
        public Button closeButton; 
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
           
            HidePopupImmediate();

           
            if (closeButton != null)
            {
                closeButton.onClick.AddListener(ClosePopup);
            }
        }

      
        public void ShowPopup()
        {
            gameObject.SetActive(true); 
            canvasGroup.blocksRaycasts = true; 
           
            canvasGroup.DOKill();
            canvasGroup.DOFade(1f, 0.3f);
        }

        
        public void ClosePopup()
        {
            canvasGroup.blocksRaycasts = false; 

           
            canvasGroup.DOKill();
            canvasGroup.DOFade(0f, 0.3f).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }

        
        private void HidePopupImmediate()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;
            gameObject.SetActive(false);
        }
    }
}