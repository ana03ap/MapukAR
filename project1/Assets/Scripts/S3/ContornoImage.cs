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
    public float completionThreshold = 0.85f;

    public Image finalImage; // La imagen que aparece desde el centro

    private Texture2D paintTexture;
    private bool completed = false;

    void Start()
    {
        paintTexture = new Texture2D(sourceTexture.width, sourceTexture.height);
        paintTexture.SetPixels(sourceTexture.GetPixels());
        paintTexture.Apply();
        paintingArea.texture = paintTexture;

        if (finalImage != null)
        {
            finalImage.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (completed) return;

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
        int radius = 50;

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

            yield return new WaitForSeconds(1f); // espera 1 segundo después de mostrar la imagen
        }

        if (currentCanvas != null) currentCanvas.gameObject.SetActive(false);
        if (nextCanvas != null) nextCanvas.gameObject.SetActive(true);
    }
}
