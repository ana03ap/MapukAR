using UnityEngine;

public class SectionPplManager : MonoBehaviour
{
    public GameObject inicioSection; // Asignas el GameObject "Inicio"
    public GameObject mapaSection;   // Asignas el GameObject "Mapa"

    // Esta función se conecta al botón "btnback"
    public void GoToInicio()
    {
        mapaSection.SetActive(false);   // Ocultar Mapa
        inicioSection.SetActive(true);  // Mostrar Inicio
    }

    // Esta función se conecta al botón "BtnInfo"
    public void GoToMapa()
    {
        inicioSection.SetActive(false); // Ocultar Inicio
        mapaSection.SetActive(true);    // Mostrar Mapa
    }
}
