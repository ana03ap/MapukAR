using UnityEngine;
using UnityEngine.EventSystems;


public class DragPiece: MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;

    public string pieceID; // ejemplo: "cabeza", "alas", etc.

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originalPosition = rectTransform.position;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        // Verificar si fue soltado sobre DropZone
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
