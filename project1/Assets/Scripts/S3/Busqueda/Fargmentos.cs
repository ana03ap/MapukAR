using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class FragmentCollector : MonoBehaviour
{
    private int collected = 0;
    public int totalFragments = 3;

    public Canvas canvasActual;
    public Canvas canvasNuevo;

    public TMP_Text contadorText;

    public List<Button> fragmentButtons; // Asignar en el Inspector

    void Start()
    {
        ResetGame(); // Asegura que todo inicie correctamente
    }

    public void CollectFragment(Button button)
    {
        StartCoroutine(ShrinkAndDisable(button));
    }

    IEnumerator ShrinkAndDisable(Button button)
    {
        Transform btnTransform = button.transform;
        Vector3 originalScale = btnTransform.localScale;

        float duration = 0.2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float scale = Mathf.Lerp(1f, 0f, elapsed / duration);
            btnTransform.localScale = new Vector3(scale, scale, scale);
            elapsed += Time.deltaTime;
            yield return null;
        }

        btnTransform.localScale = Vector3.zero;
        button.gameObject.SetActive(false);

        collected++;
        UpdateCounter();

        if (collected >= totalFragments)
        {
            // Espera antes de mostrar el nuevo canvas, luego cambia
            StartCoroutine(SwitchCanvasAfterDelay(1f));
        }
    }

    void UpdateCounter()
    {
        contadorText.text = ": " + collected;
    }

    IEnumerator SwitchCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        ResetGame(); // ✅ Reinicia todo antes de desactivar canvas

        canvasActual.gameObject.SetActive(false);
        canvasNuevo.gameObject.SetActive(true);
    }

    void ResetGame()
    {
        collected = 0;
        UpdateCounter();

        foreach (Button button in fragmentButtons)
        {
            button.gameObject.SetActive(true);
            button.transform.localScale = Vector3.one;
        }

        // Asegura estado inicial de canvas
        canvasNuevo.gameObject.SetActive(false);
        canvasActual.gameObject.SetActive(true);
    }
}
