using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARFloorManager : MonoBehaviour
{
    [Header("AR Components")]
    public ARPlaneManager planeManager;
    public ARRaycastManager raycastManager;

    [Header("UI Elements")]
    public Text instructionText;
    public GameObject tileSelectionPanel;  // panel con los 3 botones de baldosa
    public Button placeButton;

    [Header("Floor Prefab & Tiles")]
    public GameObject floorPrefab;
    public Material[] tileMaterials = new Material[3];

    // estado interno
    ARPlane detectedPlane;
    GameObject spawnedFloor;
    int selectedMaterialIndex = -1;
    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        // UI al inicio
        instructionText.text = "Detectando plano…";
        tileSelectionPanel.SetActive(false);
        placeButton.interactable = false;
        placeButton.onClick.AddListener(PlaceFloor);
    }

    void OnEnable()
    {
        planeManager.planesChanged += OnPlanesChanged;
    }

    void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    // 1) Cuando ARFoundation detecta nuevos planos
    void OnPlanesChanged(ARPlanesChangedEventArgs args)
    {
        // buscamos el primer horizontal
        foreach (var plane in args.added)
        {
            if (plane.alignment == PlaneAlignment.HorizontalUp)
            {
                detectedPlane = plane;
                instructionText.text = "Selecciona la baldosa que quieres";
                tileSelectionPanel.SetActive(true);
                return;
            }
        }
    }

    // 2) Método llamado desde los 3 botones Baldosa A/B/C
    public void SelectTile(int index)
    {
        if (index < 0 || index >= tileMaterials.Length) return;
        selectedMaterialIndex = index;
        instructionText.text = $"Baldosa {(char)('A' + index)} seleccionada.\nOprime “Colocar piso”";
        placeButton.interactable = true;
    }

    // 3) Colocar el piso cuando se pulse ese botón
    public void PlaceFloor()
    {
        if (detectedPlane == null || selectedMaterialIndex < 0) return;

        // opcional: hacer raycast para alin­ear exactamente
        Vector3 center = detectedPlane.transform.position;
        Quaternion rot = Quaternion.identity;

        // eliminar piso previo
        if (spawnedFloor != null)
            Destroy(spawnedFloor);

        // instanciar el prefab ya rotado en X=-90° en el editor
        spawnedFloor = Instantiate(floorPrefab, center, rot);
        spawnedFloor.transform.localScale = new Vector3(3f, 1f, 5f);

        // asignar material y repetir textura
        var mr = spawnedFloor.GetComponent<MeshRenderer>();
        mr.material = tileMaterials[selectedMaterialIndex];
        mr.material.mainTextureScale = new Vector2(3f, 5f);

        instructionText.text = "Piso colocado.";
        placeButton.interactable = false;
    }
}
