using System.Collections;
using UnityEngine;
using TMPro;

namespace LeeHyunWoo
{
    public class DelayTextButton : MonoBehaviour
    {
        [Header("startOn = false일 때 출력할 Text")]
        [SerializeField] private TextMeshProUGUI offTargetText;

        [Header("startOn = true일 때 출력할 Text")]
        [SerializeField] private TextMeshProUGUI onTargetText;

        [Header("반복 출력할 텍스트들")]
        [SerializeField] private string[] messages =
        {
            "텍스트 출력",
        };

        [SerializeField] private float changeInterval = 2f;

        [SerializeField] private bool startOn = false;

        [SerializeField] private bool clearAfterCycle = true;

        private bool isOn;
        private bool isPlayingCycle;
        private Coroutine textCoroutine;
        private int currentIndex;

        private void Awake()
        {
            isOn = startOn;
        }

        private void OnEnable()
        {
            StartOneCycle();
        }

        private void OnDisable()
        {
            StopCycle();
            ClearAllText();
        }

        public void OnClickToggleTextDelay()
        {
            SetOn(!isOn);
        }

        public void SetOn(bool active)
        {
            bool previousState = isOn;
            isOn = active;

            if (isPlayingCycle)
            {
                ShowCurrentText();
                return;
            }

            if (previousState && !isOn)
            {
                StartOneCycle();
                return;
            }

            ClearAllText();
        }

        private void StartOneCycle()
        {
            if (isPlayingCycle)
                return;

            if (messages == null || messages.Length == 0)
            {
                ClearAllText();
                return;
            }

            currentIndex = 0;
            textCoroutine = StartCoroutine(PlayOneCycleRoutine());
        }


        private IEnumerator PlayOneCycleRoutine()
        {
            isPlayingCycle = true;

            while (currentIndex < messages.Length)
            {
                ShowCurrentText();

                yield return new WaitForSeconds(changeInterval);

                currentIndex++;
            }

            currentIndex++;

            isPlayingCycle = false;
            textCoroutine = null;

            if (clearAfterCycle)
                ClearAllText();
        }

        private void StopCycle()
        {
            if (textCoroutine != null)
            {
                StopCoroutine(textCoroutine);
                textCoroutine = null;
            }

            isPlayingCycle = false;
        }

        private void ShowCurrentText()
        {
            ClearAllText();

            if (messages == null || messages.Length == 0)
                return;

            if (currentIndex < 0 || currentIndex >= messages.Length)
                return;

            TextMeshProUGUI currentTargetText = isOn ? onTargetText : offTargetText;

            if (currentTargetText == null)
                return;

            currentTargetText.text = messages[currentIndex];
        }

        private void ClearAllText()
        {
            if (offTargetText != null)
                offTargetText.text = "";

            if (onTargetText != null)
                onTargetText.text = "";
        }
    }
}