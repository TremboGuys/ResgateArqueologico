using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class BackMenu : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData) {
        SceneManager.LoadScene("MainMenu");
    }
}
