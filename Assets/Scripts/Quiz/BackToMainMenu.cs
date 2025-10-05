using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class BackToMainMenu : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        PersistentManager.Remove("ManagerQuiz");
        PersistentManager.Remove("ManagerRanking");
        PersistentManager.Remove("ManagerLevel");
        SceneManager.LoadScene("MainMenu");
    }
}