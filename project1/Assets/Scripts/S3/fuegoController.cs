using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GrowImageOnClick : MonoBehaviour
{
    [Header("Juego")]
    public Image targetImage;
    public Slider progressBar;
    public int totalTaps = 15;
    public float animationDuration = 0.3f;

    [Header("Elementos Finales")]
    public Button finalButton;
    public Image vasija;

    private int currentTaps = 0;
    private Coroutine scaleCoroutine;
    private bool isPulsingFinalButton = false;
    private Vector3 initialVasijaPosition = new Vector3(-57f, -387f, 0f);

    void Start()
    {
        ResetGame();
    }

    void Update()
    {
        if (isPulsingFinalButton && finalButton != null)
        {
            float scale = 1f + Mathf.Sin(Time.time * 2f) * 0.05f;
            finalButton.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }

    public void OnButtonClick()
    {
        if (currentTaps >= totalTaps) return;

        currentTaps++;

        float scaleFactor = 1f + (0.5f * currentTaps / totalTaps);
        Vector3 targetScale = Vector3.one * scaleFactor;

        if (scaleCoroutine != null)
            StopCoroutine(scaleCoroutine);

        scaleCoroutine = StartCoroutine(AnimateScale(targetScale));

        if (progressBar != null)
            progressBar.value = currentTaps;

        if (currentTaps >= totalTaps)
            StartCoroutine(AnimateVasijaAndShowButton());
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

    private IEnumerator AnimateVasijaAndShowButton()
    {
        if (vasija == null)
        {
            if (finalButton != null)
                finalButton.gameObject.SetActive(true);
            yield break;
        }

        RectTransform vasijaRect = vasija.rectTransform;
        vasijaRect.SetAsLastSibling();
        vasijaRect.anchoredPosition = initialVasijaPosition;

        Vector3 startPos = initialVasijaPosition;
        Vector3 startScale = Vector3.one;
        Vector3 targetPos = Vector3.zero;
        Vector3 targetScale = Vector3.one * 3f;

        float duration = 1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.SmoothStep(0, 1, elapsed / duration);
            vasijaRect.anchoredPosition = Vector3.Lerp(startPos, targetPos, t);
            vasijaRect.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        float rotationTime = 0f;
        float swingDuration = 1.5f;
        float swingAngle = 10f;

        while (rotationTime < swingDuration)
        {
            rotationTime += Time.deltaTime;
            float angle = Mathf.Sin(rotationTime * 4f) * swingAngle;
            vasijaRect.rotation = Quaternion.Euler(0f, 0f, angle);
            yield return null;
        }

        vasijaRect.rotation = Quaternion.identity;

        if (finalButton != null)
        {
            finalButton.gameObject.SetActive(true);
            isPulsingFinalButton = true;
        }
    }

    public void ResetGame()
    {
        currentTaps = 0;

        if (targetImage != null)
        {
            targetImage.rectTransform.localScale = Vector3.one;
            targetImage.gameObject.SetActive(true);
        }

        if (progressBar != null)
        {
            progressBar.minValue = 0;
            progressBar.maxValue = totalTaps;
            progressBar.value = 0;
        }

        if (finalButton != null)
        {
            finalButton.gameObject.SetActive(false);
            finalButton.transform.localScale = Vector3.one;
            isPulsingFinalButton = false;
        }

        if (vasija != null)
        {
            RectTransform vasijaRect = vasija.rectTransform;
            vasijaRect.localScale = Vector3.one;
            vasijaRect.anchoredPosition = initialVasijaPosition;
            vasijaRect.rotation = Quaternion.identity;
        }
    }
}
