using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class MultiSectionMarkerHandler : MonoBehaviour
{
    public ARTrackedImageManager imageManager;

    public GameObject section1;
    public GameObject section6;

    public GameObject prefabA;
    public GameObject prefabB;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    void OnEnable()
    {
        imageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        imageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        foreach (var trackedImage in args.added)
        {
            HandleMarker(trackedImage);
        }

        foreach (var trackedImage in args.updated)
        {
            HandleMarker(trackedImage);
        }

        foreach (var trackedImage in args.removed)
        {
            if (spawnedPrefabs.ContainsKey(trackedImage.referenceImage.name))
            {
                spawnedPrefabs[trackedImage.referenceImage.name].SetActive(false);
            }
        }
    }

    void HandleMarker(ARTrackedImage trackedImage)
    {
        string markerName = trackedImage.referenceImage.name;

        // Determina si debes responder al marcador según la sección activa
        if (markerName == "MarkerA" && section1.activeSelf)
        {
            ShowOrUpdatePrefab(markerName, prefabA, trackedImage);
        }
        else if (markerName == "MarkerB" && section6.activeSelf)
        {
            ShowOrUpdatePrefab(markerName, prefabB, trackedImage);
        }
        else
        {
            // Si no estás en la sección adecuada, oculta el prefab si está activo
            if (spawnedPrefabs.ContainsKey(markerName))
            {
                spawnedPrefabs[markerName].SetActive(false);
            }
        }
    }

    void ShowOrUpdatePrefab(string markerName, GameObject prefab, ARTrackedImage trackedImage)
    {
        if (!spawnedPrefabs.ContainsKey(markerName))
        {
            GameObject spawned = Instantiate(prefab, trackedImage.transform.position, trackedImage.transform.rotation);
            spawnedPrefabs.Add(markerName, spawned);
        }

        var obj = spawnedPrefabs[markerName];
        obj.transform.position = trackedImage.transform.position;
        obj.transform.rotation = trackedImage.transform.rotation;
        obj.SetActive(true);
    }
}
