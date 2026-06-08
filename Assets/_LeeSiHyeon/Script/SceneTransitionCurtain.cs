using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionCurtain : MonoBehaviour
{
    public Image image;

    Coroutine fadeCoroutine;

    /// <summary>
    /// 페이드 커튼의 알파값 변경. duration이 0보다 크면 선형적으로 변경됨.
    /// </summary>
    public void SetCurtainAlpha(float alpha, float duration = 0.0f)
    {
        if (image == null) return;
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

        alpha = Mathf.Clamp01(alpha);

        if (duration <= 0.0f) SetImageAlpha(alpha);
        else fadeCoroutine = StartCoroutine(SetCurtainAlphaCoroutine(alpha, duration));
    }

    IEnumerator SetCurtainAlphaCoroutine(float targetAlpha, float duration)
    {
        float startAlpha = image.color.a;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsed / duration);
            SetImageAlpha(currentAlpha);

            yield return null;
        }

        SetImageAlpha(targetAlpha);
        fadeCoroutine = null;
    }

    void SetImageAlpha(float alpha)
    {
        if (image == null) return;
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
