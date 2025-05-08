using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasButtonHandler : MonoBehaviour
{
    public CanvasFlowManager manager;

    public void Next() => manager.GoToNextCanvas();
    public void Back() => manager.GoToPreviousCanvas();
}
