using UnityEngine;

public class TouchRotator : MonoBehaviour
{
    [Header("Modelo a rotar")]
    [SerializeField] private GameObject modelToRotate;

    [Header("Canvas que debe estar activo para permitir rotación")]
    [SerializeField] private Canvas targetCanvas;

    [Header("Velocidad de rotación")]
    [SerializeField] private float rotationSpeed = 0.2f;

    void Update()
    {
        if (modelToRotate == null || targetCanvas == null)
            return;

        if (!targetCanvas.gameObject.activeSelf)
            return;

        // Touch input (para dispositivos móviles)
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float rotationAmount = touch.deltaPosition.x * rotationSpeed;
                modelToRotate.transform.Rotate(0f, -rotationAmount, 0f, Space.World);
            }
        }

        // Mouse input (para pruebas en editor)
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            float rotationAmount = Input.GetAxis("Mouse X") * rotationSpeed * 10f;
            modelToRotate.transform.Rotate(0f, -rotationAmount, 0f, Space.World);
        }
#endif
    }
}
