using UnityEngine;
using UnityEngine.EventSystems;

public class LeftArrow : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ManagerIcon.Instance.ChangeIndexImage(-1);
    }
}