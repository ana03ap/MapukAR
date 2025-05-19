using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S2UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainPPL;
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject itemsMenuCanvas;
    [SerializeField] private GameObject ARPositionCanvas;

    [SerializeField] private GameObject instructionPanel; // NUEVO

    void OnEnable()
    {
        GameManager.instance.OnMainppl += ActivateMainPPL;
        GameManager.instance.OnMainMenu += ActivateMainMenu;
        GameManager.instance.OnItemsMenu += ActivateItemsMenu;
        GameManager.instance.OnARPosition += ActivateARPosition;
    }

    //--------/nuevo 
    private void ResetAllCanvases()
    {
        mainPPL.SetActive(false);
        mainMenuCanvas.SetActive(false);
        itemsMenuCanvas.SetActive(false);
        ARPositionCanvas.SetActive(false);


        instructionPanel.SetActive(false);

    }

    private void ActivateMainPPL()
    {

        ResetAllCanvases();
        mainPPL.SetActive(true);


        mainPPL.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        mainPPL.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        mainPPL.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.3f);

        ARPositionCanvas.transform.DOMoveY(180, 0.3f);


        ResetCanvas(mainMenuCanvas);
        ResetCanvas(itemsMenuCanvas);
        ResetCanvas(ARPositionCanvas);
    }

    private void ActivateMainMenu()
    {

        ResetAllCanvases();
        mainMenuCanvas.SetActive(true);


        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.3f);//NUEVO PARA INTRUCCIONES


        ARPositionCanvas.transform.DOMoveY(180, 0.3f);



        // Asegurar que mainPPL está desactivado y en escala cero
        ResetCanvas(mainPPL);
        ResetCanvas(itemsMenuCanvas);
        ResetCanvas(ARPositionCanvas);
    }

    private void ActivateItemsMenu()
    {

        ResetAllCanvases();
        itemsMenuCanvas.SetActive(true);


        itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(300, 0.3f);


        // Asegurar otros elementos desactivados
        ResetCanvas(mainPPL);
        ResetCanvas(mainMenuCanvas);
        ResetCanvas(ARPositionCanvas);
    }

    private void ActivateARPosition()
    {

        ResetAllCanvases();
        ARPositionCanvas.SetActive(true);

        ARPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);


        // Asegurar otros elementos desactivados
        ResetCanvas(mainPPL);
        ResetCanvas(mainMenuCanvas);
        ResetCanvas(itemsMenuCanvas);
    }




    private void ResetCanvas(GameObject canvas)
    {
        if (canvas == null) return;

        // Resetear escala de todos los hijos
        foreach (Transform child in canvas.transform)
        {
            child.localScale = Vector3.zero;
        }

        // Resetear posición si es necesario (para ARPosition)
        if (canvas == ARPositionCanvas)
        {
            canvas.transform.GetChild(1).localPosition = new Vector3(
                canvas.transform.GetChild(1).localPosition.x,
                canvas.transform.GetChild(1).localPosition.y,
                canvas.transform.GetChild(1).localPosition.z
            );
        }
        else if (canvas == itemsMenuCanvas)
        {
            canvas.transform.GetChild(1).localPosition = new Vector3(
                canvas.transform.GetChild(1).localPosition.x,
                180,
                canvas.transform.GetChild(1).localPosition.z
            );
        }
    }



    // NUEVOS MÉTODOS para las instrucciones
    public void ShowInstructions()
{
    instructionPanel.SetActive(true);
    instructionPanel.transform.localScale = Vector3.one; // 
}

public void HideInstructions()
{
    instructionPanel.SetActive(false);
    instructionPanel.transform.localScale = Vector3.zero; // 
}

}




