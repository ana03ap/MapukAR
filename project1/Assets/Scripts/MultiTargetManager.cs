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
    [SerializeField] private Canvas[] infoCanvases; // ✅ Canvases con el mismo nombre que los modelos

    private Dictionary<string, GameObject> aRModels = new Dictionary<string, GameObject>();
    private Dictionary<string, bool> modelState = new Dictionary<string, bool>();
    private Dictionary<string, Canvas> canvasMap = new Dictionary<string, Canvas>(); // ✅ Mapeo por nombre

    private float rotationSpeed = 0.2f;

    void Start()
    {
        // Instancia modelos
        foreach (var modelPrefab in aRModelsToPlace)
        {
            GameObject instance = Instantiate(modelPrefab, Vector3.zero, Quaternion.identity);
            instance.name = modelPrefab.name;
            instance.SetActive(false);

            aRModels[instance.name] = instance;
            modelState[instance.name] = false;
        }

        // Cargar canvases en mapa
        foreach (var canvas in infoCanvases)
        {
            canvas.gameObject.SetActive(false); // ✅ Ocultarlos al inicio
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
        if (targetCanvas != null && targetCanvas.gameObject.activeSelf)
        {
            HandleTouchRotation();
            HandleTapOnModel(); // ✅ Detectar clic sobre modelo
        }
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventData)
    {
        if (targetCanvas == null || !targetCanvas.gameObject.activeSelf)
            return;

        foreach (var trackedImage in eventData.added)
        {
            ShowARModel(trackedImage);
        }

        foreach (var trackedImage in eventData.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                ShowARModel(trackedImage);
            }
            else if (trackedImage.trackingState == TrackingState.Limited)
            {
                HideARModel(trackedImage);
            }
        }
    }

    private void ShowARModel(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        if (!modelState.ContainsKey(imageName))
            return;

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

        if (!modelState.ContainsKey(imageName) || !modelState[imageName])
            return;

        GameObject model = aRModels[imageName];
        model.SetActive(false);
        modelState[imageName] = false;
    }

    private void HandleTouchRotation()
    {
        if (Input.touchCount != 1)
            return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Moved)
        {
            float rotationAmount = touch.deltaPosition.x * rotationSpeed;

            foreach (var modelEntry in aRModels)
            {
                GameObject model = modelEntry.Value;
                if (model.activeSelf)
                {
                    model.transform.Rotate(0f, -rotationAmount, 0f, Space.World);
                }
            }
        }
    }

    private void HandleTapOnModel()
    {
        if (Input.touchCount != 1)
            return;

        Touch touch = Input.GetTouch(0);
        if (touch.phase != TouchPhase.Began)
            return;

        Ray ray = Camera.main.ScreenPointToRay(touch.position);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject tappedObject = hit.collider.gameObject;
            string tappedName = tappedObject.name;

            if (aRModels.ContainsKey(tappedName))
            {
                ShowCanvasForModel(tappedName);
            }
        }
    }

    private void ShowCanvasForModel(string modelName)
    {
        foreach (var entry in canvasMap)
        {
            entry.Value.gameObject.SetActive(false);
        }

        if (canvasMap.ContainsKey(modelName))
        {
            canvasMap[modelName].gameObject.SetActive(true);
        }
    }
}
