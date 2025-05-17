using UnityEngine;

public class SectionFlowManager : MonoBehaviour
{
    public GameObject[] sections; // Array que contiene todas las secciones en orden
    private int currentSectionIndex = 0;

    void Start()
    {
        // Al iniciar la app, desactiva todas las secciones excepto la primera (SectionPpl)
        for (int i = 0; i < sections.Length; i++)
        {
            sections[i].SetActive(i == 0); // solo activa la posici�n 0 (SectionPpl)
        }
    }

    // Funci�n que se llama al presionar el bot�n "Next" o "Iniciar tour"
    public void GoToNextSection()
    {
        if (currentSectionIndex < sections.Length - 1)
        {
            // Desactiva la secci�n actual
            sections[currentSectionIndex].SetActive(false);

            // Avanza el �ndice a la siguiente secci�n
            currentSectionIndex++;

            // Activa la siguiente secci�n
            sections[currentSectionIndex].SetActive(true);
        }
    }


    public void GoToPreviousSection()
    {
        if (currentSectionIndex > 0)
        {
            // Apaga secci�n actual
            sections[currentSectionIndex].SetActive(false);
            currentSectionIndex--;

            // Activa secci�n anterior
            GameObject previousSection = sections[currentSectionIndex];
            previousSection.SetActive(true);

            // Apaga todos los canvas hijos
            Canvas[] canvases = previousSection.GetComponentsInChildren<Canvas>(true);
            foreach (Canvas canvas in canvases)
            {
                canvas.gameObject.SetActive(false);
            }

            // Activa solo el primer canvas
            if (canvases.Length > 0)
            {
                canvases[0].gameObject.SetActive(true);
            }
        }
    }

}


//ESTO VA UNO POR UNO, ENTONCES SI ME VUELO UN BOTON YA LA EMBARRO PORQUE VA ESPECIFICAMENTE UNO POR UNO