using UnityEngine;

namespace JinJooYoung
{
    public class SlidePopUp : MonoBehaviour
    {
        [SerializeField] Vector2 startPos;
        [SerializeField] RectTransform rect;

        private void Awake()
        {
            MyTween.InitPosition(rect, startPos);
        }

        public void Open()
        {

        }

        public void Close()
        {

        }
    }
}