using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ScaleOnClick : MonoBehaviour, IPointerClickHandler
{
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Escala hacia arriba y vuelve
        transform
            .DOScale(originalScale * 1.1f, 0.3f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                transform.DOScale(originalScale, 0.1f).SetEase(Ease.InOutSine);
            });
    }
}
