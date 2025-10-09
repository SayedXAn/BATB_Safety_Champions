using UnityEngine;
using UnityEngine.EventSystems;

public class GarbageDragger : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public CanvasGroup canvasGroup;
    public string targetBinName;

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