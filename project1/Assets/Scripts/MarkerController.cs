using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Collections.Generic;

public class MultiSectionMarkerHandler : MonoBehaviour
{
    public ARTrackedImageManager imageManager;

    public GameObject section1;
    public GameObject section4;
    public GameObject section8; // NUEVO

    public GameObject prefabA;
    public GameObject prefabB;
    public GameObject prefabC; // NUEVO

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

        // Revisa el nombre del marcador y la sección activa correspondiente
        if (markerName == "MarkerA" && section1.activeSelf)
        {
            ShowOrUpdatePrefab(markerName, prefabA, trackedImage);
        }
        else if (markerName == "MarkerB" && section4.activeSelf)
        {
            ShowOrUpdatePrefab(markerName, prefabB, trackedImage);
        }
        else if (markerName == "MarkerC" && section8.activeSelf) // NUEVO
        {
            ShowOrUpdatePrefab(markerName, prefabC, trackedImage);
        }
        else
        {
            // Si no es la sección activa, se oculta
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
