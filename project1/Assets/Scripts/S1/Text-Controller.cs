using UnityEngine;
using UnityEngine.UI;

public class ResetSeccionTexto : MonoBehaviour
{
    [Header("Textos que deben ocultarse")]
    [SerializeField] private GameObject[] textosAMostrar;

    [Header("Botones que deben reactivarse")]
    [SerializeField] private Button[] botones;

    private void OnEnable()
    {
        foreach (GameObject texto in textosAMostrar)
        {
            if (texto != null)
                texto.SetActive(false);
        }

        foreach (Button boton in botones)
        {
            if (boton != null)
                boton.interactable = true;
        }
    }
}
