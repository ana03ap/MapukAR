using UnityEngine;
using UnityEngine.EventSystems;

public class DragPiece : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private Vector3 originalPosition;
    private bool yaTienePosicionInicial = false;

    public string pieceID; // cualquiera, solo que sea identificable al resto de piezas

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ResetPiece()
    {
        rectTransform.SetParent(GameController.Instance.actual.transform);
        rectTransform.position = originalPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!yaTienePosicionInicial)
        {
            originalPosition = rectTransform.position;
            yaTienePosicionInicial = true;
        }

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (eventData.pointerEnter != null && eventData.pointerEnter.name == "DropZone")
        {
            rectTransform.SetParent(eventData.pointerEnter.transform);
            GameController.Instance.PiezaColocada(pieceID);
        }
        else
        {
            rectTransform.position = originalPosition;
        }
    }
}
