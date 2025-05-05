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

    void Start()
    {
        GameManager.instance.OnMainppl += ActivateMainPPL;
        GameManager.instance.OnMainMenu += ActivateMainMenu;
        GameManager.instance.OnItemsMenu += ActivateItemsMenu;
        GameManager.instance.OnARPosition += ActivateARPosition;
    }


    private void ActivateMainPPL()
    {
        mainPPL.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        mainPPL.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        mainPPL.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.3f);

        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);


        itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        ARPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        ARPositionCanvas.transform.DOMoveY(180, 0.3f);
    }

    private void ActivateMainMenu()
    {
        mainPPL.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainPPL.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainPPL.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);


        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
       

        itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        ARPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        ARPositionCanvas.transform.DOMoveY(180, 0.3f);
    }

    private void ActivateItemsMenu()
    {

        mainPPL.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainPPL.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainPPL.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);


        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        

        itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(300, 0.3f);

    }

    private void ActivateARPosition()
    {

        mainPPL.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainPPL.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainPPL.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        mainMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        mainMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
       

        itemsMenuCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        itemsMenuCanvas.transform.GetChild(1).transform.DOMoveY(180, 0.3f);

        ARPositionCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        ARPositionCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
    }

}