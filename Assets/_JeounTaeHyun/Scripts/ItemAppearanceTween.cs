using UnityEngine;
using DG.Tweening;

public class ItemAppearanceTween : MonoBehaviour
{
    void OnEnable()
    {
       
        Vector3 targetPos = transform.localPosition;
        transform.localPosition = targetPos + new Vector3(0, -50f, 0);

        
        transform.DOLocalMove(targetPos, 0.3f).SetEase(Ease.OutCubic);

      
    }
}