using UnityEngine;

namespace JinJooYoung
{
    public class UIButtonNotification : MonoBehaviour
    {
        [Header("Notification")]
        public GameObject dotObject;

        [SerializeField]
        bool isOpened = false;

        private void Awake()
        {
            Refresh();
        }

        public void Open()
        {
            Debug.Log("¹öÆ° Å¬¸¯µÊ");
            isOpened = true;

            Refresh();
        }

        public void ResetNotification()
        {
            isOpened = false;

            Refresh();
        }

        public void Refresh()
        {
            if (dotObject == null)
                return;

            dotObject.SetActive(!isOpened);
        }

        public bool IsOpened
        {
            get => isOpened;
        }
    }
}