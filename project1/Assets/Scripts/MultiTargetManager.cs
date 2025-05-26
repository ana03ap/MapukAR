using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("AR Foundation")]
    [SerializeField] private ARTrackedImageManager aRTrackedImageManager;

    [Header("3D Models")]
    [SerializeField] private GameObject[] aRModelsToPlace;

    [Header("UI")]
    [SerializeField] private Canvas targetCanvas;
    [SerializeField] private Canvas[] infoCanvases;

    private Dictionary<string, GameObject> aRModels = new Dictionary<string, GameObject>();
    private Dictionary<string, bool> modelState = new Dictionary<string, bool>();
    private Dictionary<string, Canvas> canvasMap = new Dictionary<string, Canvas>();

    private float rotationSpeed = 0.2f;
    private Vector2 initialTouchPos;
    private float tapThreshold = 10f;

    private GameObject activeModel = null;

    void Start()
    {
        foreach (var modelPrefab in aRModelsToPlace)
        {
            GameObject instance = Instantiate(modelPrefab, Vector3.zero, Quaternion.identity);
            instance.name = modelPrefab.name;
            instance.SetActive(false);

            aRModels[instance.name] = instance;
            modelState[instance.name] = false;

            if (instance.GetComponent<Collider>() == null)
                instance.AddComponent<BoxCollider>();
        }

        foreach (var canvas in infoCanvases)
        {
            canvas.gameObject.SetActive(false);
            canvasMap[canvas.name] = canvas;
        }
    }

    private void OnEnable()
    {
        aRTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        aRTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void Update()
    {
        if (targetCanvas != null && targetCanvas.gameObject.activeSelf && !IsAnyCanvasVisible())
        {
            HandleTouchInput();
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount != 1) return;

        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                initialTouchPos = touch.position;
                activeModel = GetModelUnderTouch(touch.position);
                break;

            case TouchPhase.Moved:
                if (activeModel != null)
                {
                    float rotationX = touch.deltaPosition.y * rotationSpeed;
                    float rotationY = -touch.deltaPosition.x * rotationSpeed;

                    activeModel.transform.Rotate(rotationX, rotationY, 0f, Space.World);
                }
                break;

            case TouchPhase.Ended:
                float distance = Vector2.Distance(touch.position, initialTouchPos);
                if (distance < tapThreshold && activeModel != null)
                {
                    ShowCanvasForModel(activeModel.name);
                }

                activeModel = null; // liberar modelo activo
                break;
        }
    }

    private GameObject GetModelUnderTouch(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject touched = hit.collider.gameObject;
            if (aRModels.ContainsKey(touched.name) && touched.activeSelf)
            {
                return touched;
            }
        }
        return null;
    }

    private void ShowCanvasForModel(string modelName)
    {
        foreach (var canvas in canvasMap.Values)
        {
            canvas.gameObject.SetActive(false);
        }

        if (canvasMap.ContainsKey(modelName))
        {
            canvasMap[modelName].gameObject.SetActive(true);
        }
    }

    private bool IsAnyCanvasVisible()
    {
        foreach (var canvas in canvasMap.Values)
        {
            if (canvas.gameObject.activeSelf)
                return true;
        }
        return false;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventData)
    {
        if (targetCanvas == null || !targetCanvas.gameObject.activeSelf) return;

        foreach (var trackedImage in eventData.added)
        {
            ShowARModel(trackedImage);
        }

        foreach (var trackedImage in eventData.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
                ShowARModel(trackedImage);
            else if (trackedImage.trackingState == TrackingState.Limited)
                HideARModel(trackedImage);
        }
    }

    private void ShowARModel(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (!modelState.ContainsKey(imageName)) return;

        GameObject model = aRModels[imageName];
        model.transform.position = trackedImage.transform.position;

        if (!modelState[imageName])
        {
            model.SetActive(true);
            modelState[imageName] = true;
        }
    }

    private void HideARModel(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (!modelState.ContainsKey(imageName) || !modelState[imageName]) return;

        GameObject model = aRModels[imageName];
        model.SetActive(false);
        modelState[imageName] = false;
    }
}
