using UnityEngine;
using UnityEngine.SceneManagement; // Para cambiar de escena
using UnityEngine.UI; // Para controlar los botones

public class FragmentCollector : MonoBehaviour
{
    private int collected = 0;
    public int totalFragments = 3;

    public void CollectFragment(GameObject fragment)
    {
        fragment.SetActive(false);
        collected++;

        if (collected >= totalFragments)
        {
            // Cambiar a otra escena (puedes poner el nombre de tu escena siguiente)
            SceneManager.LoadScene("Earth");
        }
    }
}
