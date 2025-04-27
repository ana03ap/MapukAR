using UnityEngine;

public class SectionPplManager : MonoBehaviour
{
    public GameObject inicioSection; // Asignas el GameObject "Inicio"
    public GameObject mapaSection;   // Asignas el GameObject "Mapa"

    // Esta funci�n se conecta al bot�n "btnback"
    public void GoToInicio()
    {
        mapaSection.SetActive(false);   // Ocultar Mapa
        inicioSection.SetActive(true);  // Mostrar Inicio
    }

    // Esta funci�n se conecta al bot�n "BtnInfo"
    public void GoToMapa()
    {
        inicioSection.SetActive(false); // Ocultar Inicio
        mapaSection.SetActive(true);    // Mostrar Mapa
    }
}
