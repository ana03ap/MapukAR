using UnityEngine;


public class SectionFlowManager : MonoBehaviour
{
    [Tooltip("Lista de GameObjects que representan cada sección completa")]
    public GameObject[] sections;

    private int currentSectionIndex = 0;

    void Start()
    {
        // Activar solo la primera sección al inicio
        for (int i = 0; i < sections.Length; i++)
        {
            sections[i].SetActive(i == 0);
        }

        // Resetear canvases internos de la sección activa
        ResetCanvasFlowInSection(sections[0]);
    }

    public void GoToNextSection()
    {
        if (currentSectionIndex < sections.Length - 1)
        {
            // Apagar la sección actual
            sections[currentSectionIndex].SetActive(false);

            // Avanzar al siguiente índice
            currentSectionIndex++;

            // Activar la nueva sección
            sections[currentSectionIndex].SetActive(true);

            // Resetear sus canvases internos
            ResetCanvasFlowInSection(sections[currentSectionIndex]);
        }
    }

    public void GoToPreviousSection()
    {
        if (currentSectionIndex > 0)
        {
            // Apagar la sección actual
            sections[currentSectionIndex].SetActive(false);

            // Retroceder al índice anterior
            currentSectionIndex--;

            // Activar la nueva sección
            sections[currentSectionIndex].SetActive(true);

            // Resetear sus canvases internos
            ResetCanvasFlowInSection(sections[currentSectionIndex]);
        }
    }

    /// <summary>
    /// Apaga todos los Canvas hijos de una sección y enciende solo el primero.
    /// </summary>

    private void ResetCanvasFlowInSection(GameObject section)
    {
        Debug.Log($" Reiniciando seccion: {section.name}");

        // 👉 Si hay un CanvasFlowManager en la sección, resetéalo también
        CanvasFlowManager flow = section.GetComponentInChildren<CanvasFlowManager>(true);
        if (flow != null)
        {
            Debug.Log($" Reseteando índice interno de canvas con CanvasFlowManager");
            flow.ResetToFirstCanvas(); // ✅ Esto sí actualiza currentIndex a 0
        }

        // Obtener todos los Canvas (activos o inactivos)
        Canvas[] canvases = section.GetComponentsInChildren<Canvas>(true);
        Debug.Log($" Canvas encontrados: {canvases.Length}");

        foreach (Canvas c in canvases)
        {
            Debug.Log($" Apagando canvas: {c.name}");
            c.gameObject.SetActive(false);
        }

        if (canvases.Length > 0)
        {
            Debug.Log($" Encendiendo canvas principal: {canvases[0].name}");
            canvases[0].gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning($" No se encontró ningún canvas en {section.name}");
        }

        // Si estamos en la sección 2, fuerza la activación del flujo MainPPL
        if (section.name.Contains("Section2")) // o usa el nombre exacto, como "S2"
        {
            S2UIManager uiManager = section.GetComponentInChildren<S2UIManager>(true);
            if (uiManager != null)
            {
                Debug.Log("Forzando activar MainPPL al volver a sección 2");
                GameManager.instance.MainPPl(); // Este método debe existir y llamar OnMainppl?.Invoke()
            }
        }




    }


}

