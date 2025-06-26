using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public string dropId;
    public void OnDrop(PointerEventData eventData)
    {
        DraggableCard card = eventData.pointerDrag.GetComponent<DraggableCard>();
        if (card != null)
        {
            card.transform.SetParent(transform);
        }
    }
}