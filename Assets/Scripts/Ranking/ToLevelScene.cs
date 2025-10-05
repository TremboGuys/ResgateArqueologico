using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ToLevelScene : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("LevelScene");
    }
}