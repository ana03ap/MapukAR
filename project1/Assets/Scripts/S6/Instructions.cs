using UnityEngine;

public class VisibilityToggle : MonoBehaviour
{
    [Header("Objeto a controlar")]
    public GameObject objeto;
    public void Mostrar()
    {
        if (objeto != null)
        {
            objeto.SetActive(true);
        }
    }
    public void Ocultar()
    {
        if (objeto != null)
        {
            objeto.SetActive(false);
        }
    }
}
