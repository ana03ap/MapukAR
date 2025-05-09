using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public GameObject particulas;
    public Button tamizadorButton;
    public Slider progressBar;

    public Canvas canvasActual;  // El canvas donde están los botones
    public Canvas canvasNuevo;   // El canvas que quieres mostrar después

    private int clickCount = 0;
    private int maxClicks = 5;

    void Start()
    {
        particulas.SetActive(false);
        tamizadorButton.onClick.AddListener(OnTamizadorClicked);
        progressBar.value = 0f;

        canvasNuevo.gameObject.SetActive(false); // Asegúrate de que el nuevo canvas esté oculto al inicio
    }

    void OnTamizadorClicked()
    {
        StartCoroutine(ShowParticlesTemporarily());
    }

    System.Collections.IEnumerator ShowParticlesTemporarily()
    {
        particulas.SetActive(true);

        yield return new WaitForSeconds(1f);

        particulas.SetActive(false);

        clickCount++;

        progressBar.value = (float)clickCount / maxClicks;

        if (clickCount >= maxClicks)
        {
            canvasActual.gameObject.SetActive(false); // Oculta el Canvas actual
            canvasNuevo.gameObject.SetActive(true);   // Muestra el nuevo Canvas
        }
    }
}
