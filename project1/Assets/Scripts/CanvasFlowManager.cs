using UnityEngine;

//public class CanvasFlowManager : MonoBehaviour
//{
//    public GameObject[] canvases; // Canvases de esta sección
//    private int currentIndex = 0;

//    void Start()
//    {
//        UpdateCanvasVisibility();
//    }

//    public void GoToNextCanvas()
//    {
//        if (currentIndex < canvases.Length - 1)
//        {
//            currentIndex++;
//            UpdateCanvasVisibility();
//        }
//    }

//    public void GoToPreviousCanvas()
//    {
//        if (currentIndex > 0)
//        {
//            currentIndex--;
//            UpdateCanvasVisibility();
//        }
//    }

//    private void UpdateCanvasVisibility()
//    {
//        for (int i = 0; i < canvases.Length; i++)
//        {
//            canvases[i].SetActive(i == currentIndex);
//        }
//    }

//    public void GoToCanvas(int index)
//    {
//        if (index >= 0 && index < canvases.Length)
//        {
//            currentIndex = index;
//            UpdateCanvasVisibility();
//        }
//    }

//    public int GetCurrentCanvasIndex() => currentIndex;


//    //#NUEVO 18 05 10am
//    public void ResetToFirstCanvas()
//    {
//        currentIndex = 0;
//        for (int i = 0; i < canvases.Length; i++)
//        {
//            canvases[i].SetActive(i == 0); // solo el primero queda activo
//        }
//    }

//}



public class CanvasFlowManager : MonoBehaviour
{
   
    public GameObject[] canvases;

    private int currentIndex = 0;

    void Start()
    {
        ResetToFirstCanvas();
    }

    public void GoToNextCanvas()
    {
        Debug.Log($" Intentando ir de {currentIndex} a {currentIndex + 1}");

        if (currentIndex < canvases.Length - 1)
        {
            canvases[currentIndex].SetActive(false);
            currentIndex++;
            canvases[currentIndex].SetActive(true);
            Debug.Log($" Se cambió al canvas: {canvases[currentIndex].name}");
        }
        else
        {
            Debug.Log(" Ya estás en el último canvas.");
        }
    }

    public void GoToPreviousCanvas()
    {
        if (currentIndex > 0)
        {
            canvases[currentIndex].SetActive(false);
            currentIndex--;
            canvases[currentIndex].SetActive(true);
        }
    }

    public void GoToCanvas(int index)
    {
        if (index >= 0 && index < canvases.Length)
        {
            canvases[currentIndex].SetActive(false);
            currentIndex = index;
            canvases[currentIndex].SetActive(true);
        }
    }

    public int GetCurrentCanvasIndex() => currentIndex;

    /// <summary>
    /// Apaga todos los canvases excepto el primero.
    /// Se llama automáticamente al entrar a la sección.
    /// </summary>
    public void ResetToFirstCanvas()
    {
        currentIndex = 0; //  Esto es lo que faltaba

        for (int i = 0; i < canvases.Length; i++)
        {
            canvases[i].SetActive(i == 0); // Solo enciende el primero
        }
    }

}
