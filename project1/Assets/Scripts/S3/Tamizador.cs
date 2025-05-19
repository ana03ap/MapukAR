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

    private int clickCount = 0;
    private int maxClicks = 5;

    void Start()
    {
        ResetGame(); // Asegura un inicio limpio
        tamizadorButton.onClick.AddListener(OnTamizadorClicked);
    }

    void OnTamizadorClicked()
    {
        StartCoroutine(ShowParticlesTemporarily());
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
            // Llamamos a la rutina que reinicia y luego cambia canvas
            StartCoroutine(SwitchCanvasAfterDelay(1f));
        }
    }

    IEnumerator SwitchCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        ResetGame(); // ✅ Reiniciar antes de desactivar canvas

        canvasActual.gameObject.SetActive(false);
        canvasNuevo.gameObject.SetActive(true);
    }

    void ResetGame()
    {
        clickCount = 0;
        progressBar.value = 0f;
        particulas.SetActive(false);

        canvasNuevo.gameObject.SetActive(false);
        canvasActual.gameObject.SetActive(true);
    }
}
