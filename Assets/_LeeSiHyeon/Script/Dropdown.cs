using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;

namespace LeeSihyeon
{
    public class Dropdown : MonoBehaviour
    {
        public TextMeshProUGUI text;
        [Header("Icon")]
        public RectTransform icon;
        [Header("Items")]
        public DropdownItem itemPrefab;
        public Transform panel;
        public string[] itemTexts;
        [Header("Duration")]
        public float toggleDuration = 0.1f;
        public float buttonActionDuration = 0.125f;

        Button button;
        DropdownItem[] items;
        Coroutine dropdownActionCoroutine;
        bool isOpen;
        public bool IsOpen { get { return isOpen; } private set { isOpen = value; } }
        int index;

        private void Awake()
        {
            button = GetComponent<Button>();
            if (button == null)
            {
                Debug.LogError(gameObject.name + "은(는) Button 컴포넌트가 필요함.");
                return;
            }
            if (text == null)
            {
                Debug.LogError(gameObject.name + "은(는) TextMeshProUGUI 컴포넌트가 할당되지 않음.");
                return;
            }
            if (panel == null)
            {
                Debug.LogError(gameObject.name + "은(는) 패널이 할당되지 않음.");
                return;
            }

            button.onClick.AddListener(OnClick);
        }

        private void Start()
        {
            text.text = itemTexts.Length > 0 ? itemTexts[0] : "No Items";
            if (DropdownManager.Instance) DropdownManager.Instance.AddDropdown(this);

            items = new DropdownItem[itemTexts.Length];
        }

        void OnClick()
        {
            OpenToggle();
        }

        public void OpenToggle()
        {
            if (IsOpen) StartClosing();
            else StartOpening();
        }

        public void StartOpening(float durationMultiplier = 1.0f)
        {
            StopAction(true);
            transform.SetAsLastSibling();
            dropdownActionCoroutine = StartCoroutine(DropdownAction(true, durationMultiplier));
            int randomStupidNumber = Random.Range(0, 250); // Wow!! Awesome logic!
            int n = (randomStupidNumber * 2) + 1;
            icon.DORotate(Vector3.forward * 180 * n, toggleDuration * durationMultiplier).SetEase(Ease.OutQuad);
        }

        public void StartClosing(float durationMultiplier = 1.0f)
        {
            StopAction(false);
            dropdownActionCoroutine = StartCoroutine(DropdownAction(false, durationMultiplier));
            icon.DORotate(Vector3.zero, toggleDuration * durationMultiplier).SetEase(Ease.OutQuad);
        }

        IEnumerator DropdownAction(bool open, float durationMultiplier = 1)
        {
            float actionInterval = toggleDuration / itemTexts.Length;
            actionInterval *= durationMultiplier;

            if (open)
            {
                if (index >= itemTexts.Length) index = itemTexts.Length - 1;
                if (index < 0) index = 0;
                for (; index < itemTexts.Length; index++)
                {
                    DropdownItem newItem = items[index] = Instantiate(itemPrefab, panel);
                    newItem.root = this;
                    newItem.startActionDuration = buttonActionDuration;
                    newItem.SetText(itemTexts[index]);
                    if (index + 1 < itemTexts.Length) yield return new WaitForSeconds(actionInterval);
                }

                if (index >= itemTexts.Length) index = itemTexts.Length - 1;
            }
            else
            {
                if (index >= itemTexts.Length) index = itemTexts.Length - 1;
                if (index < 0) index = 0;
                for (; index >= 0; index--)
                {
                    if (items[index] != null) items[index].Close(buttonActionDuration);
                    if (index - 1 >= 0) yield return new WaitForSeconds(actionInterval);
                }

                if (index < 0) index = 0;
            }
        }

        void StopAction(bool goingOpen)
        {
            IsOpen = goingOpen;
            if (dropdownActionCoroutine != null) StopCoroutine(dropdownActionCoroutine);
        }

        public void SetText(string type)
        {
            text.text = type;
            StartClosing();
        }

        private void OnDestroy() { if (DropdownManager.Instance) DropdownManager.Instance.RemoveDropdown(this); }
    }
}