using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class S4CanvasController : MonoBehaviour
{
    public GameObject canvas1;
    public GameObject canvas2;
    public GameObject bookPrefab;
    private GameObject spawnedBook;

    void Start()
    {
        // Mostrar solo el Canvas1
        canvas1.SetActive(true);
        canvas2.SetActive(false);

        // Instanciar el libro frente a la cámara
        SpawnBook();
    }

    void SpawnBook()
    {
        if (bookPrefab != null && Camera.main != null)
        {
            Vector3 position = Camera.main.transform.position + Camera.main.transform.forward * 2f;
            Quaternion rotation = Quaternion.Euler(4.607f, 534.464f, -0.123f);
            spawnedBook = Instantiate(bookPrefab, position, rotation);
        }
    }


    // Este método se llamará desde el botón OpenMe
    public void OnOpenMePressed()
    {
        canvas1.SetActive(false);
        canvas2.SetActive(true);

        if (spawnedBook != null)
        {
            Destroy(spawnedBook);
        }
    }
}
