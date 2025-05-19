using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public GameObject actual;       // Canvas1 (activar� el modelo 3D)
    public GameObject modelo3D;       // Canvas1 (activar� el modelo 3D)
    public GameObject popupFinal;     // Panel de informaci�n (opcional)


    public DragPiece[] piezas; // as�gnalas desde el inspector (las 4 piezas del rompecabezas)

    private HashSet<string> piezasColocadas = new HashSet<string>();

    private void Awake()
    {
        if (Instance == null) Instance = this;

        // Aseg�rate de iniciar ocultando el resultado
        if (modelo3D != null) modelo3D.SetActive(false);
        if (popupFinal != null) popupFinal.SetActive(false);
    }

    public void PiezaColocada(string id)
    {
        if (!piezasColocadas.Contains(id))
            piezasColocadas.Add(id);

        if (piezasColocadas.Count == 4)
        {
            // Resetear piezas antes de cambiar de canvas
            foreach (DragPiece pieza in piezas)
            {
                pieza.ResetPiece();
            }

            piezasColocadas.Clear(); // por si vuelves a hacer el juego


            actual.SetActive(false);
            if (modelo3D != null) modelo3D.SetActive(true);
            if (popupFinal != null) popupFinal.SetActive(true);
        }
    }
}

