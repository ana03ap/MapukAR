using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S5Canvas1 : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform placedObjectsContainer;
    [SerializeField] private GameObject currentCanvas;
    [SerializeField] private GameObject nextCanvas;

    [SerializeField] private float rotationSpeed = 0.2f;
    [SerializeField] private float zoomSpeed = 0.1f;
    [SerializeField] private float minZoom = 0.3f;
    [SerializeField] private float maxZoom = 1.0f;

    private GameObject currentModel;

    void OnEnable()
    {
        ClearPlacedObjects();
        Show();
    }

    void OnDisable()
    {
        ClearPlacedObjects(); 
    }

    public void Show()
    {
        var cam = Camera.main.transform;
        Vector3 spawnPos = cam.position + cam.forward * 0.7f;

        currentModel = Instantiate(
            prefab,
            spawnPos,
            Quaternion.LookRotation(cam.forward),
            placedObjectsContainer
        );
    }

    public void GoToNextCanvas()
    {
        ClearPlacedObjects();
        currentCanvas.SetActive(false);
        nextCanvas.SetActive(true);
    }

    private void ClearPlacedObjects()
    {
        foreach (Transform child in placedObjectsContainer)
            Destroy(child.gameObject);
    }

    void Update()
    {
        if (currentModel == null) return;

        HandleTouchRotation();
        HandleMouseRotation();
        HandleKeyboardRotation();

    }

    private void HandleTouchRotation()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float rotationAmount = touch.deltaPosition.x * rotationSpeed;
                currentModel.transform.Rotate(0f, -rotationAmount, 0f, Space.World);
            }
        }
    }

    private void HandleMouseRotation()
    {
        if (Input.GetMouseButton(0))
        {
            float rotationAmount = Input.GetAxis("Mouse X") * 100f * Time.deltaTime;
            currentModel.transform.Rotate(0f, -rotationAmount, 0f, Space.World);
        }
    }

    private void HandleKeyboardRotation()
    {
        float horizontal = Input.GetAxis("Horizontal"); // se mueve también con las flechas del teclado
        currentModel.transform.Rotate(0f, horizontal * 100f * Time.deltaTime, 0f, Space.World);
    }



    private void ApplyZoom(float amount)
    {
        Vector3 direction = currentModel.transform.forward;
        Vector3 newPos = currentModel.transform.position + direction * amount;

        float distance = Vector3.Distance(Camera.main.transform.position, newPos);
        if (distance >= minZoom && distance <= maxZoom)
        {
            currentModel.transform.position = newPos;
        }
    }
}
