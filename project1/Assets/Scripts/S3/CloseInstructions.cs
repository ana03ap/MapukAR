using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseInstructions : MonoBehaviour
{
    // GameObject que se activará después de cerrar la imagen
    public GameObject objectToShow;

    // Esta función se conecta al botón y toma una imagen como parámetro
    public void Close(Image imageToHide)
    {
        if (imageToHide != null)
        {
            imageToHide.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No se asignó ninguna imagen.");
        }

        if (objectToShow != null)
        {
            objectToShow.SetActive(true);
        }
        else
        {
            Debug.LogWarning("No se asignó ningún GameObject para mostrar.");
        }
    }
}
