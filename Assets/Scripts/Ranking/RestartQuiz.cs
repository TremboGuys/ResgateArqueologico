using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RestartQuiz : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        PersistentManager.Instance.SetQuizId(ManagerQuiz.Instance.GetIdQuiz());
        PersistentManager.Remove("ManagerQuiz");
        PersistentManager.Remove("ManagerRanking");
        SceneManager.LoadScene("Quiz");
    }
}