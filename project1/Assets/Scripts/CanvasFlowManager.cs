using UnityEngine;

public class CanvasFlowManager : MonoBehaviour
{
    public GameObject[] canvases; // Canvases de esta sección
    private int currentIndex = 0;

    void Start()
    {
        UpdateCanvasVisibility();
    }

    public void GoToNextCanvas()
    {
        if (currentIndex < canvases.Length - 1)
        {
            currentIndex++;
            UpdateCanvasVisibility();
        }
    }

    public void GoToPreviousCanvas()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateCanvasVisibility();
        }
    }

    private void UpdateCanvasVisibility()
    {
        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetActive(i == currentIndex);
        }
    }

    public void GoToCanvas(int index)
    {
        if (index >= 0 && index < canvases.Length)
        {
            currentIndex = index;
            UpdateCanvasVisibility();
        }
    }

    public int GetCurrentCanvasIndex() => currentIndex;
}
