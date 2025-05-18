using UnityEngine;

//public class SectionFlowManager : MonoBehaviour
//{
//    public GameObject[] sections; // Array que contiene todas las secciones en orden
//    private int currentSectionIndex = 0;

//    void Start()
//    {
//        // Al iniciar la app, desactiva todas las secciones excepto la primera (SectionPpl)
//        for (int i = 0; i < sections.Length; i++)
//        {
//            sections[i].SetActive(i == 0); // solo activa la posición 0 (SectionPpl)
//        }

//    }

//    // Función que se llama al presionar el botón "Next" o "Iniciar tour"
//    public void GoToNextSection()
//    {
//        if (currentSectionIndex < sections.Length - 1)
//        {
//            // Desactiva la sección actual
//            sections[currentSectionIndex].SetActive(false);

//            // Avanza el índice a la siguiente sección
//            currentSectionIndex++;

//            // Activa la siguiente sección
//            sections[currentSectionIndex].SetActive(true);
//        }
//    }


//    public void GoToPreviousSection()
//    {
//        if (currentSectionIndex > 0)
//        {
//            // Apaga sección actual
//            sections[currentSectionIndex].SetActive(false);
//            currentSectionIndex--;

//            // Activa sección anterior
//            GameObject previousSection = sections[currentSectionIndex];
//            previousSection.SetActive(true);

//            // Apaga todos los canvas hijos
//            Canvas[] canvases = previousSection.GetComponentsInChildren<Canvas>(true);
//            foreach (Canvas canvas in canvases)
//            {
//                canvas.gameObject.SetActive(false);
//            }

//            // Activa solo el primer canvas
//            if (canvases.Length > 0)
//            {
//                canvases[0].gameObject.SetActive(true);
//            }
//        }
//    }

//}




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
    }


}

