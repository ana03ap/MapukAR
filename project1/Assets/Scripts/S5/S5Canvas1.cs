using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S5Canvas1 : MonoBehaviour
{
    [SerializeField] private GameObject megalaniaPrefab;
    [SerializeField] private Transform placedObjectsContainer;
    [SerializeField] private GameObject currentCanvas;
    [SerializeField] private GameObject nextCanvas;

    void OnEnable()
    {
        // Borra lo antiguo y vuelve a generar
        ClearPlacedObjects();
        ShowMegalania();
    }

    public void ShowMegalania()
    {
        var cam = Camera.main.transform;
        Vector3 spawnPos = cam.position + cam.forward * 0.7f;

        Instantiate(
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
}
