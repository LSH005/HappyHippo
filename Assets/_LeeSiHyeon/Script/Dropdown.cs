using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        /// <summary> 버튼 클릭 이벤트. 상태에 따라 토글 열기 또는 닫기 수행 </summary>
        void OnClick()
        {
            OpenToggle();
        }

        /// <summary> 현재 <see cref="IsOpen"/> 상태에 따라 <see cref="StartOpening"/> 또는 <see cref="StartClosing"/> 호출 </summary>
        public void OpenToggle()
        {
            if (IsOpen) StartClosing();
            else StartOpening();
        }

        /// <summary> 드롭다운 열기 시작 </summary>
        /// <param name="durationMultiplier">애니메이션 속도 배율.</param>
        public void StartOpening(float durationMultiplier = 1.0f)
        {
            StopAction(true);
            transform.SetAsLastSibling();
            dropdownActionCoroutine = StartCoroutine(DropdownAction(true, durationMultiplier));
            int randomStupidNumber = Random.Range(0, 250); // Wow!! Awesome logic!
            int n = (randomStupidNumber * 2) + 1;
            icon.DORotate(Vector3.forward * 180 * n, toggleDuration * durationMultiplier).SetEase(Ease.OutQuad);
        }

        /// <summary> 드롭다운 닫기 시작 </summary>
        /// <param name="durationMultiplier">애니메이션 속도 배율.</param>
        public void StartClosing(float durationMultiplier = 1.0f)
        {
            StopAction(false);
            dropdownActionCoroutine = StartCoroutine(DropdownAction(false, durationMultiplier));
            icon.DORotate(Vector3.zero, toggleDuration * durationMultiplier).SetEase(Ease.OutQuad);
        }

        /// <summary> 드롭다운 항목 생성 또는 삭제 애니메이션 코루틴 </summary>
        /// <param name="open">열면 <see langword="true"/>, 닫으면 <see langword="false"/>.</param>
        /// <param name="durationMultiplier">애니메이션 속도 배율.</param>
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

        /// <summary> 진행 중인 코루틴 중지 및 <see cref="IsOpen"/> 상태 갱신 </summary>
        /// <param name="goingOpen">열리는 중이면 <see langword="true"/>, 아니면 <see langword="false"/>.</param>
        void StopAction(bool goingOpen)
        {
            IsOpen = goingOpen;
            if (dropdownActionCoroutine != null) StopCoroutine(dropdownActionCoroutine);
        }

        /// <summary> 드롭다운 텍스트 설정 후 <see cref="StartClosing"/> 호출. </summary>
        /// <param name="type">변경할 텍스트 내용.</param>
        public void SetText(string type)
        {
            text.text = type;
            StartClosing();
        }

        private void OnDestroy() { if (DropdownManager.Instance) DropdownManager.Instance.RemoveDropdown(this); }
    }
}