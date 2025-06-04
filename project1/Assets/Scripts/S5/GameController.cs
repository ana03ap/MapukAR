using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public GameObject actual;       
    public GameObject modelo3D;       
  


    public DragPiece[] piezas; 

    private HashSet<string> piezasColocadas = new HashSet<string>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        if (modelo3D != null) modelo3D.SetActive(false);
    }

    public void PiezaColocada(string id)
    {
        if (!piezasColocadas.Contains(id))
            piezasColocadas.Add(id);

        if (piezasColocadas.Count == 6)
        {
            foreach (DragPiece pieza in piezas)
            {
                pieza.ResetPiece();
            }

            piezasColocadas.Clear(); 
            actual.SetActive(false);
            MostrarModelo3DAnimado();
        }
    }

    public void MostrarModelo3DAnimado()
    {
        if (modelo3D == null) return;

        modelo3D.SetActive(true);
        modelo3D.SetActive(true);

        foreach (Transform child in modelo3D.transform)
        {
            child.localScale = Vector3.zero; 
            child.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack); 
        }
    }

}

