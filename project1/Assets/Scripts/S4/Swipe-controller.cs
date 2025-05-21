using UnityEngine;
using UnityEngine.UI;

public class SwipeInstructionUI : MonoBehaviour
{    [SerializeField] private GameObject instructionImage;

    [SerializeField] private float swipeThreshold = 50f;

    private Vector2 startTouchPosition;
    private bool swipeDetected = false;

    void OnEnable()
    {
        // Reinicia todo al entrar de nuevo a la sección
        swipeDetected = false;

        if (instructionImage != null)
        {
            instructionImage.SetActive(true);
        }
    }

    void Update()
    {
        if (swipeDetected) return;

#if UNITY_EDITOR || UNITY_STANDALONE
        // Para pruebas con mouse en el editor
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Vector2 endTouchPosition = Input.mousePosition;
            DetectSwipe(endTouchPosition - startTouchPosition);
        }
#else
        // En dispositivos móviles
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 endTouchPosition = touch.position;
                DetectSwipe(endTouchPosition - startTouchPosition);
            }
        }
#endif
    }

    void DetectSwipe(Vector2 delta)
    {
        if (delta.magnitude >= swipeThreshold)
        {
            swipeDetected = true;

            if (instructionImage != null)
            {
                instructionImage.SetActive(false);
            }
        }
    }
}
