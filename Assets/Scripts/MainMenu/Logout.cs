using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Logout : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        PersistentManager.Remove("ManagerLogin");
        SceneManager.LoadScene("LoginScene");
    }
}