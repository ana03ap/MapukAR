using UnityEngine;

public class VisibilityToggle : MonoBehaviour
{
    [Header("Objeto a controlar")]
    public GameObject objeto;

    // Llama esta función para mostrar el objeto
    public void Mostrar()
    {
        if (objeto != null)
        {
            objeto.SetActive(true);
        }
    }

    // Llama esta función para ocultar el objeto
    public void Ocultar()
    {
        if (objeto != null)
        {
            objeto.SetActive(false);
        }
    }
}
