using UnityEngine;
using UnityEngine.UI;

public class ProgressHintManager : MonoBehaviour
{
    [Header("UI Elements")]
    public Slider progressBar;
    public Image handImage;
    public Button tapButton;

    [Header("Config")]
    public float idleTimeToShowHint = 15f;
    public float pulseSpeed = 1f;
    public float pulseScale = 1.2f;

    private float lastTapTime;
    private bool isHintVisible = false;

    void Start()
    {
        if (tapButton != null)
        {
            tapButton.onClick.AddListener(OnTap);
        }

        lastTapTime = Time.time;
        handImage.gameObject.SetActive(false);
    }

    void Update()
    {
        // Mostrar mano si barra está en 0
        if (progressBar.value == 0)
        {
            ShowHint();
        }
        else if (Time.time - lastTapTime > idleTimeToShowHint)
        {
            ShowHint();
        }
        else
        {
            HideHint();
        }

        // Animación "pulse" si está activa
        if (handImage.gameObject.activeSelf)
        {
            float scale = 1 + Mathf.Sin(Time.time * pulseSpeed) * (pulseScale - 1);
            handImage.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }

    void OnTap()
    {
        lastTapTime = Time.time;
        HideHint();
    }

    void ShowHint()
    {
        if (!isHintVisible)
        {
            handImage.gameObject.SetActive(true);
            isHintVisible = true;
        }
    }

    void HideHint()
    {
        if (isHintVisible)
        {
            handImage.gameObject.SetActive(false);
            handImage.transform.localScale = Vector3.one;
            isHintVisible = false;
        }
    }
}
