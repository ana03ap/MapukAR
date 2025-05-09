using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections; // <- Necesario para usar Coroutines

public class FragmentCollector : MonoBehaviour
{
    private int collected = 0;
    public int totalFragments = 3;

    public Canvas canvasActual;
    public Canvas canvasNuevo;

    public TMP_Text contadorText;

    void Start()
    {
        UpdateCounter();
    }

    public void CollectFragment(Button button)
    {
        StartCoroutine(ShrinkAndDisable(button));
    }

    IEnumerator ShrinkAndDisable(Button button)
    {
        Transform btnTransform = button.transform;
        Vector3 originalScale = btnTransform.localScale;

        float duration = 0.2f; // Tiempo que tarda la animación
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
            canvasActual.gameObject.SetActive(false);
            canvasNuevo.gameObject.SetActive(true);
        }
    }

    void UpdateCounter()
    {
        contadorText.text = ": " + collected;
    }
}
