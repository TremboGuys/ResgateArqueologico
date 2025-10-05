using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RestartQuiz : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ManagerQuiz.Instance.OnPointerClickRestartQuiz();
        PersistentManager.Remove("ManagerRanking");
    }
}