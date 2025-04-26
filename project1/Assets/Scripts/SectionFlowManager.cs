using UnityEngine;

public class SectionFlowManager : MonoBehaviour
{
    [Header("Canvases de Sección (0 a 6)")]
    [SerializeField] private GameObject[] sectionCanvases;

    [Header("Contenedor de Objetos AR")]
    [SerializeField] private Transform placedObjectsContainer;

    private int currentSectionIndex = 0;

    void Start()
    {
        // Al iniciar, activa sólo la sección 0 y desactiva el resto
        for (int i = 0; i < sectionCanvases.Length; i++)
            sectionCanvases[i].SetActive(i == 0);
    }

    /// <summary>
    /// Llamar desde el botón "Siguiente" de cada sección.
    /// </summary>
    public void GoToNextSection()
    {
        // 1) Limpiar AR de la sección actual
        foreach (Transform child in placedObjectsContainer)
            Destroy(child.gameObject);

        // 2) Ocultar Canvas actual
        sectionCanvases[currentSectionIndex].SetActive(false);

        // 3) Avanzar índice
        currentSectionIndex++;

        // 4) Si queda una sección válida, mostrar; si no, opcional “Fin”
        if (currentSectionIndex < sectionCanvases.Length)
        {
            sectionCanvases[currentSectionIndex].SetActive(true);
        }
        else
        {
            Debug.Log("¡Tour terminado!");
            // Aquí podrías volver al inicio o cerrar la app
        }
    }
}
