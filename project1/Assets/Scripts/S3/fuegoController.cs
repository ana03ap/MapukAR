using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GrowImageOnClick : MonoBehaviour
{
    public Image targetImage;           // Imagen a escalar (sprite)
    public Slider progressBar;          // Barra de progreso
    public int totalTaps = 15;          // Número de taps para llegar al tamaño máximo
    public float animationDuration = 0.3f; // Duración del crecimiento por clic

    public GameObject canvasToHide;     // GameObject del canvas a ocultar
    public GameObject canvasToShow;     // GameObject del canvas a mostrar

    private int currentTaps = 0;
    private Vector3 originalScale;
    private Coroutine scaleCoroutine;

    void Start()
    {
        if (targetImage != null)
            originalScale = targetImage.rectTransform.localScale;

        if (progressBar != null)
        {
            progressBar.minValue = 0;
            progressBar.maxValue = totalTaps;
            progressBar.value = 0;
        }

        // Mostrar solo el canvas inicial
        if (canvasToHide != null)
            canvasToHide.SetActive(true);

        if (canvasToShow != null)
            canvasToShow.SetActive(false);
    }

    public void OnButtonClick()
    {
        if (currentTaps >= totalTaps) return;

        currentTaps++;

        // Calcular nuevo tamaño destino
        float scaleFactor = 1f + (0.5f * currentTaps / totalTaps); // crece hasta 1.5x
        Vector3 targetScale = originalScale * scaleFactor;

        // Cancelar animación anterior si está corriendo
        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        // Iniciar nueva animación
        scaleCoroutine = StartCoroutine(AnimateScale(targetScale));

        // Actualizar barra de progreso
        if (progressBar != null)
            progressBar.value = currentTaps;

        // Si se ha alcanzado el total de taps, cambiar de canvas
        if (currentTaps >= totalTaps)
        {
            if (canvasToHide != null)
                canvasToHide.SetActive(false);

            if (canvasToShow != null)
                canvasToShow.SetActive(true);
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
}
