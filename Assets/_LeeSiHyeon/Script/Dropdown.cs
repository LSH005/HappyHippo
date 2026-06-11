using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LeeSihyeon
{
    public class Dropdown : MonoBehaviour
    {
        public TextMeshProUGUI text;
        [Header("Items")]
        public DropdownItem itemPrefab;
        public Transform panel;
        public string[] itemTexts;
        [Header("Duration")]
        public float toggleInterval = 0.1f;
        public float buttonActionDuration = 0.125f;

        Button button;
        List<DropdownItem> items = new List<DropdownItem>();
        enum state
        {
            Open,
            Close,
            Opening,
            Closing
        }
        state currntState = state.Close;

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
        }

        void OnClick()
        {
            if (IsState(state.Opening) || IsState(state.Closing)) return;
            if (IsState(state.Open))
            {
                StartClosing();
            }
            else if (IsState(state.Close))
            {
                StartOpening();
            }
        }

        public void StartOpening()
        {
            StartCoroutine(Open());
            SetState(state.Opening);
        }

        public void StartClosing()
        {
            StartCoroutine(Close());
            SetState(state.Closing);
        }

        IEnumerator Open()
        {
            transform.SetAsLastSibling();

            foreach (string text in itemTexts)
            {
                DropdownItem newItem = Instantiate(itemPrefab, panel);
                newItem.textUI.text = text;
                newItem.actionDuration = buttonActionDuration;
                newItem.root = this;
                items.Add(newItem);
                yield return new WaitForSeconds(toggleInterval);
            }

            SetState(state.Open);
        }

        IEnumerator Close()
        {
            if (items.Count > 0)
            {
                for (int i = items.Count - 1; i >= 0; i--)
                {
                    items[i].Close();
                    yield return new WaitForSeconds(toggleInterval);
                }
            }

            items.Clear();
            SetState(state.Close);
        }

        public void SetType(string type)
        {
            if (IsState(state.Opening) || IsState(state.Closing)) return;
            StartCoroutine(Close());
            SetState(state.Closing);
            text.text = type;
        }

        bool IsState(state Comparator) => currntState == Comparator;
        void SetState(state newState) => currntState = newState;

        private void OnDestroy() { if (DropdownManager.Instance) DropdownManager.Instance.RemoveDropdown(this); }
    }
}