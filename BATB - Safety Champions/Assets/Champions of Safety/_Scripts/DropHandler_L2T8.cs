using UnityEngine;
using UnityEngine.EventSystems;

public class DropHandler_L2T8 : MonoBehaviour, IDropHandler
{
    public Level2Controller l2Con;
    public int slotID;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<DragNDrop_L2T8>().placedCorrectly = true;
            l2Con.DragDropOutput(eventData.pointerDrag.GetComponent<DragNDrop_L2T8>().itemID, slotID);
        }
    }
}
