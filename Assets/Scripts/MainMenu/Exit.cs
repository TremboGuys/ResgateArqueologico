using UnityEngine;
using UnityEngine.EventSystems;

public class Exit : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }
}