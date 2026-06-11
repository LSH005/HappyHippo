using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class StockStatusToggle : MonoBehaviour
{
    [Header("UI 연결")]
    public Image capsuleImage;
    public RectTransform circleTransform;
    public TextMeshProUGUI statusText;

    [Header("색상")]
    public Color onColor = new Color(1f, 0.43f, 0.29f);
    public Color offColor = new Color(0.4f, 0.4f, 0.4f);

    [Header("★ 토글 위치 수동 설정 (눈으로 보고 맞추기)")]
    [Tooltip("재고 있음(ON)일 때 원의 Anchored Position X 값")]
    public float onPositionX = 28f;
    [Tooltip("재고 없음(OFF)일 때 원의 Anchored Position X 값")]
    public float offPositionX = -28f;

    [Header("애니메이션 설정")]
    public float animationDuration = 0.2f;
    public float colorTransitionDuration = 0.15f;

    private bool isStockAvailable = true;
    private Vector2 onPosition;
    private Vector2 offPosition;

    private Coroutine toggleAnimationCoroutine;

    private void Start()
    {
        // 인스펙터에서 입력한 X 값과 현재 Y 값을 조합해 좌표를 완성합니다.
        onPosition = new Vector2(onPositionX, circleTransform.anchoredPosition.y);
        offPosition = new Vector2(offPositionX, circleTransform.anchoredPosition.y);

        // 초기 상태 적용
        ApplyState();
    }

    public void OnToggleClicked()
    {
        if (toggleAnimationCoroutine != null)
        {
            StopCoroutine(toggleAnimationCoroutine);
        }

        isStockAvailable = !isStockAvailable;
        toggleAnimationCoroutine = StartCoroutine(AnimateToggleState());
    }

    private IEnumerator AnimateToggleState()
    {
        float elapsedTime = 0f;
        Vector2 startingPosition = circleTransform.anchoredPosition;
        Color startingColor = capsuleImage.color;

        Vector2 targetPosition = isStockAvailable ? onPosition : offPosition;
        Color targetColor = isStockAvailable ? onColor : offColor;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / animationDuration);

            circleTransform.anchoredPosition = Vector2.Lerp(startingPosition, targetPosition, t);

            if (elapsedTime <= colorTransitionDuration)
            {
                float colorT = Mathf.Clamp01(elapsedTime / colorTransitionDuration);
                capsuleImage.color = Color.Lerp(startingColor, targetColor, colorT);
            }

            yield return null;
        }

        circleTransform.anchoredPosition = targetPosition;
        capsuleImage.color = targetColor;

        UpdateStatusText();
        toggleAnimationCoroutine = null;
    }

    private void ApplyState()
    {
        UpdateStatusText();

        if (isStockAvailable)
        {
            capsuleImage.color = onColor;
            circleTransform.anchoredPosition = onPosition;
        }
        else
        {
            capsuleImage.color = offColor;
            circleTransform.anchoredPosition = offPosition;
        }
    }

    private void UpdateStatusText()
    {
        if (statusText != null)
            statusText.text = isStockAvailable ? "재고 있음" : "재고 없음";
    }
}