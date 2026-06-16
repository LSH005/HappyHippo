using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Scene_Quest_Á¤À±¼º_2023137028
{
    public class DimController : MonoBehaviour
    {
        private Image dimImage;

        void Awake()
        {
            dimImage = GetComponent<Image>();
        }

        public void FadeInDim()
        {
            if (dimImage != null)
            {
                dimImage.color = new Color(0, 0, 0, 0);
                dimImage.DOFade(150f / 255f, 0.3f).SetEase(Ease.OutSine);
            }
        }
    }
}