using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ARInteractionsManager : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    [SerializeField] private float moveSpeed = 0.0005f; // Ajusta para velocidad de movimiento
    [SerializeField] private float scaleFactor = 0.25f;  // Tamaño relativo del modelo

    private GameObject arPointer;
    private GameObject item3DModel;
    private bool isDragging = false;
    private Vector2 lastTouchPosition;
    private bool isPlacing = false;

    public GameObject Item3DModel
    {
        set
        {
            item3DModel = value;

            if (arPointer != null && item3DModel != null)
            {
                arPointer.SetActive(true);

                // Posicionar el puntero a 0.6 metros frente al usuario
                Vector3 forward = arCamera.transform.forward * 0.6f;
                arPointer.transform.position = arCamera.transform.position + forward;

                // Escalar modelo de forma global y centrado sobre el puntero
                item3DModel.transform.localScale = Vector3.one; // Reset
                item3DModel.transform.SetParent(null);
                item3DModel.transform.localScale = Vector3.one * scaleFactor;
                item3DModel.transform.SetParent(arPointer.transform);

                // Centrar y apoyar el modelo sobre el puntero visual (ajustar Y según el modelo)
                item3DModel.transform.localPosition = new Vector3(0f, -0.1f, 0f);
               

                isPlacing = true;
            }
        }
    }

    void Start()
    {
        arPointer = transform.GetChild(0).gameObject;
        arPointer.SetActive(false); // Se activa cuando se selecciona un modelo
    }

    void Update()
    {
        if (!isPlacing || Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            lastTouchPosition = touch.position;

            if (!IsTapOverUI(touch.position))
                isDragging = true;
        }

        else if (touch.phase == TouchPhase.Moved && isDragging)
        {
            Vector2 delta = touch.position - lastTouchPosition;
            lastTouchPosition = touch.position;

            // Movimiento libre en X, Y, Z con respecto a la cámara
            Vector3 right = arCamera.transform.right;
            Vector3 up = arCamera.transform.up;
            Vector3 forward = arCamera.transform.forward;

            Vector3 move = (right * delta.x + up * delta.y + forward * delta.y) * moveSpeed;
            arPointer.transform.position += move;
        }

        else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            isDragging = false;
        }
    }

    private bool IsTapOverUI(Vector2 touchPosition)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = touchPosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        return results.Count > 0;
    }

    public void ConfirmPlacement()
    {
        if (item3DModel != null)
        {
            item3DModel.transform.SetParent(null);
            arPointer.SetActive(false);
            isPlacing = false;
        }
    }

    public void DeleteItem()
    {
        if (item3DModel != null)
        {
            Destroy(item3DModel);
            item3DModel = null;
        }

        arPointer.SetActive(false);
        isPlacing = false;
    }
}
