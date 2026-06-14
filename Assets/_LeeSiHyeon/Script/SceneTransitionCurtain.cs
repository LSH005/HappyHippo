using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransitionCurtain : MonoBehaviour
{
    public Image image;

    /// <summary> 페이드 커튼의 알파값 변경. <paramref name="duration"/>이 0보다 크면 선형적으로 변경됨
    /// </summary>
    /// <param name="alpha">목표 알파값</param>
    /// <param name="duration">변경 소요 시간</param>
    public void SetCurtainAlpha(float alpha, float duration = 0.0f)
    {
        if (image == null) return;
        image.DOKill();

        alpha = Mathf.Clamp01(alpha);

        if (duration <= 0.0f) SetImageAlpha(alpha);
        else image.DOFade(alpha, duration);
    }

    /// <summary> 이미지 컴포넌트의 알파값을 <paramref name="alpha"/>로 즉시 설정 </summary>
    /// <param name="alpha">설정할 알파값</param>
    void SetImageAlpha(float alpha)
    {
        if (image == null) return;
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
