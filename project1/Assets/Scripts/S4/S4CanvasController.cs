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

    private Vector2 lastTouchPosition;
    private bool isRotating = false;

    void OnEnable()
    {
        canvas1.SetActive(true);
        canvas2.SetActive(false);

        if (spawnedBook == null)
        {
            SpawnBook();
        }
    }

    void SpawnBook()
{
    if (bookPrefab != null && Camera.main != null)
    {
        Vector3 position = Camera.main.transform.position + Camera.main.transform.forward * 2f;

        Quaternion rotation = Quaternion.LookRotation(Camera.main.transform.forward);

        spawnedBook = Instantiate(bookPrefab, position, rotation);

        spawnedBook.transform.localScale = Vector3.one * 0.7f;
    }
}


    void Update()
    {
        if (spawnedBook == null) return;

        // Mouse rotation (PC)
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
            lastTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0) && isRotating)
        {
            Vector2 delta = (Vector2)Input.mousePosition - lastTouchPosition;
            spawnedBook.transform.Rotate(Vector3.up, -delta.x * 0.2f, Space.World);
            spawnedBook.transform.Rotate(Vector3.right, delta.y * 0.2f, Space.World);
            lastTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }

        // Touch rotation (Mobile)
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 delta = touch.deltaPosition;
                spawnedBook.transform.Rotate(Vector3.up, -delta.x * 0.2f, Space.World);
                spawnedBook.transform.Rotate(Vector3.right, delta.y * 0.2f, Space.World);
            }
        }
    }

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

