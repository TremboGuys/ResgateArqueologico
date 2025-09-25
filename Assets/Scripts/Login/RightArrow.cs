using UnityEngine;
using UnityEngine.EventSystems;

public class RightArrow : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(ManagerIcon.Instance);
        ManagerIcon.Instance.ChangeIndexImage(1);    
    }
}
