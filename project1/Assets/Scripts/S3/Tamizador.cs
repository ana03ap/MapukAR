using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonHandler : MonoBehaviour
{
    public GameObject particulas;
    public Button tamizadorButton;
    public Slider progressBar;

    public Canvas canvasActual;
    public Canvas canvasNuevo;
    public Image Instrucciones;

    private int clickCount = 0;
    private int maxClicks = 5;
    private RectTransform buttonRect;

    void Start()
    {
        ResetGame(); // Asegura un inicio limpio
        tamizadorButton.onClick.AddListener(OnTamizadorClicked);
        buttonRect = tamizadorButton.GetComponent<RectTransform>();
    }

    void OnTamizadorClicked()
    {
        StartCoroutine(ShakeButton()); // ✅ Animación de agitación rápida
        StartCoroutine(ShowParticlesTemporarily());
    }

    IEnumerator ShakeButton()
    {
        Vector3 originalPos = buttonRect.anchoredPosition;
        float shakeAmount = 10f; // desplazamiento en píxeles
        float shakeSpeed = 50f;  // velocidad de oscilación
        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float offset = Mathf.Sin(elapsed * shakeSpeed) * shakeAmount;
            buttonRect.anchoredPosition = originalPos + new Vector3(offset, 0f, 0f);
            yield return null;
        }

        buttonRect.anchoredPosition = originalPos; // Restaurar posición original
    }

    IEnumerator ShowParticlesTemporarily()
    {
        particulas.SetActive(true);

        yield return new WaitForSeconds(1f);

        particulas.SetActive(false);

        clickCount++;
        progressBar.value = (float)clickCount / maxClicks;

        if (clickCount >= maxClicks)
        {
            StartCoroutine(SwitchCanvasAfterDelay(1f));
        }
    }

    IEnumerator SwitchCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        ResetGame();

        canvasActual.gameObject.SetActive(false);
        canvasNuevo.gameObject.SetActive(true);
        Instrucciones.gameObject.SetActive(true);
    }

    void ResetGame()
    {
        clickCount = 0;
        progressBar.value = 0f;
        particulas.SetActive(false);

        canvasNuevo.gameObject.SetActive(false);
        canvasActual.gameObject.SetActive(true);

        if (buttonRect != null)
            buttonRect.anchoredPosition = new Vector3(0f, 0f, 0f); // Evita acumulación si se resetea
    }
}
