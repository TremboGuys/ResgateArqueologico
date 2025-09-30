using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class OpenCreditsTMP : MonoBehaviour
{
    public string creditsSceneName = "CreditsScene";
    private TMP_Text tmpText;

    void Start()
    {
        tmpText = GetComponent<TMP_Text>();
        var button = tmpText.gameObject.AddComponent<UnityEngine.UI.Button>();
        button.onClick.AddListener(OpenCredits);
    }

    void OpenCredits()
    {
        SceneManager.LoadScene(creditsSceneName);
    }
}
