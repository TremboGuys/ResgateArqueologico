using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ToLevelScene : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("LevelsScene");
        PersistentManager.Remove("ManagerQuiz");
        PersistentManager.Remove("ManagerRanking");
    }
}