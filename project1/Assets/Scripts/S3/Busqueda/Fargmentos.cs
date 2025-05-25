using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class FragmentCollector : MonoBehaviour
{
    [Header("UI Canvases")]
    public Canvas canvasActual;
    public Canvas canvasNuevo;
    public Image canvasInstrucciones;

    [Header("UI Elements")]
    public TMP_Text contadorText;
    public List<Button> fragmentButtons;

    [Header("Settings")]
    public int totalFragments = 3;
    public float idleTimeToPulse = 10f;
    public float pulseScale = 1.15f;
    public float pulseSpeed = 3f;

    private int collected = 0;
    private float lastUpdateTime;
    private bool isPulsing = false;

    void Start()
    {
        ResetGame();
    }

    void Update()
    {
        bool shouldPulse = ShouldAnimateButtons();

        if (shouldPulse && !isPulsing)
        {
            isPulsing = true;
        }
        else if (!shouldPulse && isPulsing)
        {
            isPulsing = false;
            ResetButtonScales();
        }

        if (isPulsing)
        {
            AnimateVisibleButtons();
        }
    }

    public void CollectFragment(Button button)
    {
        button.interactable = false;
        StartCoroutine(ShrinkAndDisable(button));
    }

    IEnumerator ShrinkAndDisable(Button button)
    {
        Transform btnTransform = button.transform;

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
            StartCoroutine(SwitchCanvasAfterDelay(1f));
        }
    }

    void UpdateCounter()
    {
        contadorText.text = ": " + collected;
        lastUpdateTime = Time.time;
    }

    IEnumerator SwitchCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        ResetGame();

        canvasActual.gameObject.SetActive(false);
        canvasNuevo.gameObject.SetActive(true);
    }

    void ResetGame()
    {
        collected = 0;
        lastUpdateTime = Time.time;
        UpdateCounter();

        foreach (Button button in fragmentButtons)
        {
            button.gameObject.SetActive(true);
            button.transform.localScale = Vector3.one;
            button.interactable = true;
        }

        canvasNuevo.gameObject.SetActive(false);
        canvasActual.gameObject.SetActive(true);
        canvasInstrucciones.gameObject.SetActive(true);
    }

    bool ShouldAnimateButtons()
    {
        return collected == 0 || (collected > 0 && Time.time - lastUpdateTime > idleTimeToPulse);
    }

    void AnimateVisibleButtons()
    {
        float scale = 1 + Mathf.Sin(Time.time * pulseSpeed) * (pulseScale - 1f);

        foreach (Button button in fragmentButtons)
        {
            if (button.gameObject.activeInHierarchy)
            {
                button.transform.localScale = new Vector3(scale, scale, 1f);
            }
        }
    }

    void ResetButtonScales()
    {
        foreach (Button button in fragmentButtons)
        {
            if (button.gameObject.activeInHierarchy)
            {
                button.transform.localScale = Vector3.one;
            }
        }
    }
}
