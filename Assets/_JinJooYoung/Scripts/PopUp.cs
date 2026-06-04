using UnityEngine;

namespace JinJooYoung
{
    public class PopUp : MonoBehaviour
    {
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] RectTransform popupBox;

        private void Awake()
        {
            MyTween.InitPopup(canvasGroup, popupBox);
        }

        public void Open()
        {
            MyTween.OpenPopup(canvasGroup, popupBox);
        }

        public void Close()
        {
            MyTween.ClosePopup(canvasGroup, popupBox);
        }
    }
}