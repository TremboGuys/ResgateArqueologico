using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerLogin : MonoBehaviour
{
    public static ManagerLogin Instance { get; private set; }
    public string inputUsername;
    public string photoPath = "alternativeA";
    User user;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log(photoPath);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnUserReceived(User user)
    {
        this.user = user;
        SceneManager.LoadScene("MainMenu");
    }

    public void Login()
    {
        if (inputUsername != null)
        {
            UserRegister register = new(inputUsername, photoPath);
            string json = JsonUtility.ToJson(register);
            StartCoroutine(LoginService.Login("http://localhost:8000/api/players/", json));
        }
    }
}