


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S5Canvas1 : MonoBehaviour
{
    [SerializeField] private GameObject megalaniaPrefab;
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
        ShowMegalania();
    }

    void OnDisable()
    {
        ClearPlacedObjects(); // borra el modelo si se apaga el canvas
    }

    public void ShowMegalania()
    {
        var cam = Camera.main.transform;
        Vector3 spawnPos = cam.position + cam.forward * 0.7f;

        currentModel = Instantiate(
            megalaniaPrefab,
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
        //HandleZoom();
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
        float horizontal = Input.GetAxis("Horizontal"); // A/D o ← →
        currentModel.transform.Rotate(0f, horizontal * 100f * Time.deltaTime, 0f, Space.World);
    }

    private void HandleZoom()
    {
        Vector3 direction = currentModel.transform.forward;

        // Zoom móvil
        if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            Vector2 prevT0 = t0.position - t0.deltaPosition;
            Vector2 prevT1 = t1.position - t1.deltaPosition;

            float prevDist = (prevT0 - prevT1).magnitude;
            float currentDist = (t0.position - t1.position).magnitude;

            float delta = (currentDist - prevDist) * zoomSpeed * Time.deltaTime;
            ApplyZoom(delta);
        }

        // Zoom PC
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.001f)
        {
            ApplyZoom(scroll * zoomSpeed);
        }
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
