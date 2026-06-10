using System.Collections;
using UnityEngine;
using TMPro;

namespace LeeHyunWoo
{
    public class DelayTextButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI targetText;

        [SerializeField] private string message = "텍스트 출력";
        [SerializeField] private float delayTime = 1f;

        [SerializeField] private bool startOn = false;

        private bool isOn;
        private Coroutine textCoroutine;

        private void Awake()
        {
            isOn = startOn;

            if (targetText != null)
                targetText.text = isOn ? "" : message;
        }

        public void OnClickToggleTextDelay()
        {
            isOn = !isOn;

            if (textCoroutine != null)
            {
                StopCoroutine(textCoroutine);
                textCoroutine = null;
            }

            if (isOn)
            {
                HideText();
            }
            else
            {
                textCoroutine = StartCoroutine(ShowTextDelay());
            }
        }

        private IEnumerator ShowTextDelay()
        {
            yield return new WaitForSeconds(delayTime);

            if (targetText != null)
                targetText.text = message;

            textCoroutine = null;
        }

        private void HideText()
        {
            if (targetText != null)
                targetText.text = "";
        }
    }
}