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
            sections[currentSectionIndex].SetActive(false);
            currentSectionIndex--;
            sections[currentSectionIndex].SetActive(true);
        }
    }
}


//ESTO VA UNO POR UNO, ENTONCES SI ME VUELO UN BOTON YA LA EMBARRO PORQUE VA ESPECIFICAMENTE UNO POR UNO