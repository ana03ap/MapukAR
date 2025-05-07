using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    [Header("UI Image donde se mostrará el collar")]
    [SerializeField] private Image mainImage;

    [Header("Sprites de cada collar (en el mismo orden que los botones)")]
    [SerializeField] private Sprite[] collarSprites;

    [Header("Botones que eligen cada collar")]
    [SerializeField] private Button[] buttons;

    void Start()
    {
        // Validación rápida
        if (buttons.Length != collarSprites.Length)
            Debug.LogWarning("¡Sprites y botones no coinciden en cantidad!");

        // Suscribir cada botón a mostrar el sprite correspondiente
        for (int i = 0; i < buttons.Length; i++)
        {
            int idx = i;  // captura local
            buttons[i].onClick.AddListener(() => ShowSprite(idx));
        }
    }

    private void ShowSprite(int index)
    {
        if (index < 0 || index >= collarSprites.Length) return;
        mainImage.sprite = collarSprites[index];
    }
}
