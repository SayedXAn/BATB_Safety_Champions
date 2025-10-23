using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DropHandler_L2T8 : MonoBehaviour, IDropHandler
{
    public Level2Controller l2Con;
    public int slotID;
    private bool iHaveConsumedPointer = false;
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        if(eventData.pointerDrag != null && !iHaveConsumedPointer)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<DragNDrop_L2T8>().placedCorrectly = true;
            iHaveConsumedPointer = true;
            l2Con.DragDropOutput(eventData.pointerDrag.GetComponent<DragNDrop_L2T8>().itemID, slotID);
            GetComponent<Image>().color = new Color32(0, 0, 0, 0);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
