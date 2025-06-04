using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ColoringGame : MonoBehaviour
{
    public RawImage paintingArea;
    public Texture2D sourceTexture;
    public Color color = Color.red;

    public Canvas currentCanvas;
    public Canvas nextCanvas;
    public Image instructionsImage;
    public float completionThreshold = 0.85f;

    public Image finalImage;         // Imagen final 
    public Image hintImage;          // Imagen de ayuda 

    private Texture2D paintTexture;
    private bool completed = false;

    private float lastPaintTime;
    private float currentProgress = 0f;
    private float hintIdleThreshold = 15f;
    private bool hintVisible = false;
    [Header("Horneado")]
    public GrowImageOnClick horneadoScript; // Referencia al script de horneado


    void Start()
    {
        ResetGame();
    }

    void Update()
    {
        if (completed) return;

        
        bool showHint = currentProgress == 0f || (Time.time - lastPaintTime > hintIdleThreshold);
        if (showHint && !hintVisible)
        {
            ShowHint();
        }
        else if (!showHint && hintVisible)
        {
            HideHint();
        }

        
        if (hintVisible)
        {
            float offset = Mathf.Sin(Time.time * 2f) * 10f; 
            hintImage.rectTransform.anchoredPosition = new Vector2(offset, hintImage.rectTransform.anchoredPosition.y);
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                paintingArea.rectTransform,
                Input.mousePosition,
                null,
                out localPos
            );

            float x = (localPos.x + paintingArea.rectTransform.rect.width / 2f) / paintingArea.rectTransform.rect.width;
            float y = (localPos.y + paintingArea.rectTransform.rect.height / 2f) / paintingArea.rectTransform.rect.height;

            int px = Mathf.FloorToInt(x * paintTexture.width);
            int py = Mathf.FloorToInt(y * paintTexture.height);

            Paint(px, py);
        }
    }

    void Paint(int x, int y)
    {
        int radius = 70;

        for (int dx = -radius; dx <= radius; dx++)
        {
            for (int dy = -radius; dy <= radius; dy++)
            {
                int nx = x + dx;
                int ny = y + dy;

                if (nx < 0 || ny < 0 || nx >= paintTexture.width || ny >= paintTexture.height)
                    continue;

                if (sourceTexture.GetPixel(nx, ny).a > 0.1f)
                {
                    paintTexture.SetPixel(nx, ny, color);
                }
            }
        }

        paintTexture.Apply();
        CheckFillCompletion();
    }

    void CheckFillCompletion()
    {
        Color[] sourcePixels = sourceTexture.GetPixels();
        Color[] paintedPixels = paintTexture.GetPixels();

        int filled = 0;
        int total = 0;

        for (int i = 0; i < sourcePixels.Length; i++)
        {
            if (sourcePixels[i].a > 0.1f)
            {
                total++;
                if (paintedPixels[i] == color)
                {
                    filled++;
                }
            }
        }

        float percent = (float)filled / total;

        // Si el progreso aumentó, actualiza el tiempo DEBERIA DE CAMBBIARSE A UN CAMBIO SMOOTH
        if (percent > currentProgress)
        {
            lastPaintTime = Time.time;
            currentProgress = percent;
        }

        if (!completed && percent >= completionThreshold)
        {
            completed = true;
            StartCoroutine(ShowFinalImageAndSwitchCanvas());
        }
    }

    IEnumerator ShowFinalImageAndSwitchCanvas()
    {
        if (finalImage != null)
        {
            paintingArea.gameObject.SetActive(false);
            finalImage.gameObject.SetActive(true);

            CanvasGroup group = finalImage.GetComponent<CanvasGroup>();
            if (group == null) group = finalImage.gameObject.AddComponent<CanvasGroup>();

            finalImage.rectTransform.localScale = Vector3.zero;
            group.alpha = 0;

            float duration = 1.0f;
            float t = 0;

            while (t < duration)
            {
                t += Time.deltaTime;
                float progress = t / duration;

                finalImage.rectTransform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, progress);
                group.alpha = progress;
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }

        ResetGame();

        if (currentCanvas != null) currentCanvas.gameObject.SetActive(false);
        if (nextCanvas != null) nextCanvas.gameObject.SetActive(true);
    }

    void ShowHint()
    {
        if (hintImage != null)
        {
            hintImage.gameObject.SetActive(true);
            hintVisible = true;
        }
    }

    void HideHint()
    {
        if (hintImage != null)
        {
            hintImage.gameObject.SetActive(false);
            hintVisible = false;
            hintImage.rectTransform.anchoredPosition = Vector2.zero; // Reset posición
        }
    }

    void ResetGame()
    {
        completed = false;
        currentProgress = 0f;
        lastPaintTime = Time.time;

        paintTexture = new Texture2D(sourceTexture.width, sourceTexture.height);
        paintTexture.SetPixels(sourceTexture.GetPixels());
        paintTexture.Apply();
        paintingArea.texture = paintTexture;

        if (paintingArea != null) paintingArea.gameObject.SetActive(true);
        if (finalImage != null)
        {
            finalImage.gameObject.SetActive(false);
            finalImage.rectTransform.localScale = Vector3.one;

            var group = finalImage.GetComponent<CanvasGroup>();
            if (group != null) group.alpha = 1;
        }

        if (hintImage != null)
        {
            hintImage.gameObject.SetActive(false);
            hintVisible = false;
        }

        if (nextCanvas != null) nextCanvas.gameObject.SetActive(false);
        if (currentCanvas != null) currentCanvas.gameObject.SetActive(true);
        if (instructionsImage != null) instructionsImage.gameObject.SetActive(true);
        if (horneadoScript != null) horneadoScript.ResetGame(); 

    }
}
