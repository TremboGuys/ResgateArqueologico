using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ToRanking : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (ManagerRanking.Instance == null)
        {
            SceneManager.LoadScene("RankingScene");
        }
    }
}