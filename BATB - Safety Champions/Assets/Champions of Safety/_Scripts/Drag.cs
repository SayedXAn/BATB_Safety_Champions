using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public CanvasGroup canvasGroup;
    // public void DragHandler(BaseEventData data)
    // {
        
    //     PointerEventData pointerData = (PointerEventData)data;

    //     Vector2 position;

    //     RectTransformUtility.ScreenPointToLocalPointInRectangle(
    //         (RectTransform)canvas.transform, pointerData.position,
    //         canvas.worldCamera, out position);

    //     transform.position = canvas.transform.TransformPoint(position);
    // }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;
        canvasGroup.interactable = false;
        Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
    }
}
