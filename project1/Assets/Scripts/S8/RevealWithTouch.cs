using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RevealWithTouch : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public RawImage rawImage;            
    public float radius = 50f;     

    private Texture2D workingTexture;
    private Texture2D originalTexture;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = rawImage.rectTransform;

        // guardar la textura original
        Texture2D sourceTex = (Texture2D)rawImage.texture;
        originalTexture = new Texture2D(sourceTex.width, sourceTex.height, TextureFormat.ARGB32, false);
        originalTexture.SetPixels(sourceTex.GetPixels());
        originalTexture.Apply();

        ResetWorkingTexture();
    }

    void OnEnable()
    {
        if (originalTexture != null)
        {
            ResetWorkingTexture();
        }
    }

    private void ResetWorkingTexture()
    {
        workingTexture = new Texture2D(originalTexture.width, originalTexture.height, TextureFormat.ARGB32, false);
        workingTexture.SetPixels(originalTexture.GetPixels());
        workingTexture.Apply();

        rawImage.texture = workingTexture;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        HandleTouch(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        HandleTouch(eventData.position);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            HandleTouch(Input.mousePosition);
        }
    }

    private void HandleTouch(Vector2 screenPosition)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPosition, null, out Vector2 localPoint))
        {
            Vector2 normalized = Rect.PointToNormalized(rectTransform.rect, localPoint);
            int x = Mathf.RoundToInt(normalized.x * workingTexture.width);
            int y = Mathf.RoundToInt(normalized.y * workingTexture.height);

            for (int i = -Mathf.FloorToInt(radius); i < Mathf.CeilToInt(radius); i++)
            {
                for (int j = -Mathf.FloorToInt(radius); j < Mathf.CeilToInt(radius); j++)
                {
                    int px = x + i;
                    int py = y + j;

                    if (px >= 0 && px < workingTexture.width && py >= 0 && py < workingTexture.height)
                    {
                        float dist = Mathf.Sqrt(i * i + j * j);
                        if (dist < radius)
                        {
                            Color color = workingTexture.GetPixel(px, py);
                            color.a = 0f;
                            workingTexture.SetPixel(px, py, color);
                        }
                    }
                }
            }

            workingTexture.Apply();
        }
    }
}
