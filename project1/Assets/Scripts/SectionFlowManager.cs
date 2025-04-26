using UnityEngine;

public class SectionFlowManager : MonoBehaviour
{
    [Header("Canvases de Secci�n (0 a 6)")]
    [SerializeField] private GameObject[] sectionCanvases;

    [Header("Contenedor de Objetos AR")]
    [SerializeField] private Transform placedObjectsContainer;

    private int currentSectionIndex = 0;

    void Start()
    {
        // Al iniciar, activa s�lo la secci�n 0 y desactiva el resto
        for (int i = 0; i < sectionCanvases.Length; i++)
            sectionCanvases[i].SetActive(i == 0);
    }

    /// <summary>
    /// Llamar desde el bot�n "Siguiente" de cada secci�n.
    /// </summary>
    public void GoToNextSection()
    {
        // 1) Limpiar AR de la secci�n actual
        foreach (Transform child in placedObjectsContainer)
            Destroy(child.gameObject);

        // 2) Ocultar Canvas actual
        sectionCanvases[currentSectionIndex].SetActive(false);

        // 3) Avanzar �ndice
        currentSectionIndex++;

        // 4) Si queda una secci�n v�lida, mostrar; si no, opcional �Fin�
        if (currentSectionIndex < sectionCanvases.Length)
        {
            sectionCanvases[currentSectionIndex].SetActive(true);
        }
        else
        {
            Debug.Log("�Tour terminado!");
            // Aqu� podr�as volver al inicio o cerrar la app
        }
    }
}
