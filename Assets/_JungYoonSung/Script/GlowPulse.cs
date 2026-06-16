using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Scene_Quest_정윤성_2023137028
{
    public class GlowPulse : MonoBehaviour
{
    private Image glowImage;

    void Start()
    {
        glowImage = GetComponent<Image>();

        if (glowImage != null)
        {
            // 투명도를 부드럽게 오가도록 반복 
            glowImage.DOFade(0.6f, 1.2f)
                .From(0.2f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        //  크기도 미세하게 커졌다 작아졌다 하도록 연출
        transform.DOScale(new Vector3(1.15f, 1.15f, 1f), 1.2f)
            .From(Vector3.one)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    private void OnDestroy()
    {
        transform.DOKill();
        if (glowImage != null)
        {
            glowImage.DOKill();
        }
    }
}
}