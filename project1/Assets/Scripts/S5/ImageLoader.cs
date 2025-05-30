using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
    [Header("UI Image donde se mostrar� el collar")]
    [SerializeField] private Image mainImage;

    [Header("Sprites de cada collar (en el mismo orden que los botones)")]
    [SerializeField] private Sprite[] collarSprites;

    [Header("Botones que eligen cada collar")]
    [SerializeField] private Button[] buttons;

    void Start()
    {

        if (buttons.Length != collarSprites.Length)
        for (int i = 0; i < buttons.Length; i++)
        {
            int idx = i; 
            buttons[i].onClick.AddListener(() => ShowSprite(idx));
        }
    }

    private void ShowSprite(int index)
    {
        if (index < 0 || index >= collarSprites.Length) return;
        mainImage.sprite = collarSprites[index];
    }
}
