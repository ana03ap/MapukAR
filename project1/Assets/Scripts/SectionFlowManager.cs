using UnityEngine;


public class SectionFlowManager : MonoBehaviour
{
    [Tooltip("Lista de GameObjects que representan cada sección completa")]
    public GameObject[] sections;

    private int currentSectionIndex = 0;

    void Start()
    {
        // asegurarse de solo la primera sección al inicio
        for (int i = 0; i < sections.Length; i++)
        {
            sections[i].SetActive(i == 0);
        }
        ResetCanvasFlowInSection(sections[0]);
    }

    public void GoToNextSection()
    {
        if (currentSectionIndex < sections.Length - 1)
        {
            sections[currentSectionIndex].SetActive(false);
            currentSectionIndex++;
            sections[currentSectionIndex].SetActive(true);
            ResetCanvasFlowInSection(sections[currentSectionIndex]);
        }
    }

    public void GoToPreviousSection()
    {
        if (currentSectionIndex > 0)
        {
            sections[currentSectionIndex].SetActive(false);
            currentSectionIndex--;
            sections[currentSectionIndex].SetActive(true);
            ResetCanvasFlowInSection(sections[currentSectionIndex]);
        }
    }



    private void ResetCanvasFlowInSection(GameObject section)
    {
        Debug.Log($" Reiniciando seccion: {section.name}");
        CanvasFlowManager flow = section.GetComponentInChildren<CanvasFlowManager>(true);
        if (flow != null)
        {
            flow.ResetToFirstCanvas(); 
        }
        Canvas[] canvases = section.GetComponentsInChildren<Canvas>(true);

        foreach (Canvas c in canvases)
        {
            c.gameObject.SetActive(false);
        }

        if (canvases.Length > 0)
        {
            canvases[0].gameObject.SetActive(true);
        }
        else
        {
        }

        // Si estamos en la sección 2, fuerza la activación del flujo MainPPL
        if (section.name.Contains("Section2")) 
        {
            S2UIManager uiManager = section.GetComponentInChildren<S2UIManager>(true);
            if (uiManager != null)
            {
                GameManager.instance.MainPPl(); // llamar OnMainppl?.Invoke()
            }
        }
    }


}

