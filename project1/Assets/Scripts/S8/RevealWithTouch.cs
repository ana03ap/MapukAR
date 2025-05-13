using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RevealWithTouch : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public RawImage rawImage;             // La imagen superior
    public float radius = 50f;            // Tamaño del "borrado"

    private Texture2D workingTexture;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = rawImage.rectTransform;

        // Copiar la textura original para editarla
        Texture2D sourceTex = (Texture2D)rawImage.texture;
        workingTexture = new Texture2D(sourceTex.width, sourceTex.height, TextureFormat.ARGB32, false);
        workingTexture.SetPixels(sourceTex.GetPixels());
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
        // Permite probar en PC con clic del mouse
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
