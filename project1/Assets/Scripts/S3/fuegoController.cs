using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GrowImageOnClick : MonoBehaviour
{
    public Image targetImage;
    public Slider progressBar;
    public int totalTaps = 15;
    public float animationDuration = 0.3f;

    public Button finalButton; // 👉 Botón a mostrar al final

    private int currentTaps = 0;
    private Vector3 originalScale;
    private Coroutine scaleCoroutine;

    void Start()
    {
        if (targetImage != null)
            originalScale = targetImage.rectTransform.localScale;

        ResetGame(); // Inicia en estado limpio
    }

    public void OnButtonClick()
    {
        if (currentTaps >= totalTaps) return;

        currentTaps++;

        float scaleFactor = 1f + (0.5f * currentTaps / totalTaps);
        Vector3 targetScale = originalScale * scaleFactor;

        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        scaleCoroutine = StartCoroutine(AnimateScale(targetScale));

        if (progressBar != null)
            progressBar.value = currentTaps;

        if (currentTaps >= totalTaps && finalButton != null)
        {
            finalButton.gameObject.SetActive(true); // ✅ Mostrar botón final
        }
    }

    private IEnumerator AnimateScale(Vector3 targetScale)
    {
        Vector3 startScale = targetImage.rectTransform.localScale;
        float elapsed = 0f;

        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / animationDuration);
            targetImage.rectTransform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        targetImage.rectTransform.localScale = targetScale;
    }

    public void ResetGame()
    {
        currentTaps = 0;

        if (targetImage != null)
            targetImage.rectTransform.localScale = originalScale;

        if (progressBar != null)
        {
            progressBar.minValue = 0;
            progressBar.maxValue = totalTaps;
            progressBar.value = 0;
        }

        if (finalButton != null)
            finalButton.gameObject.SetActive(false); // Ocultar al inicio
    }
}
