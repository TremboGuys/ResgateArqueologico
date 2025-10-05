using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerLogin : MonoBehaviour
{
    public static ManagerLogin Instance { get; private set; }
    public string inputUsername;
    public string photoPath = "alternativeA";
    [SerializeField] private GameObject errorUserMessage;
    private User user;

    void Start()
    {
        errorUserMessage.SetActive(false);
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            photoPath = "alternativeA";
            PersistentManager.Register("ManagerLogin", gameObject);
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
        Debug.Log(inputUsername);
        if (inputUsername != null)
        {
            UserRegister register = new(inputUsername, photoPath);
            string json = JsonUtility.ToJson(register);
            StartCoroutine(LoginService.Login("https://resgate-arqueologico-backend.onrender.comapi/players/", json));
        }
    }

    public void ShowErrorUserMessage()
    {
        errorUserMessage.SetActive(true);
    }

    public int GetIdUser()
    {
        return user.id;
    }

    public string GetUsername()
    {
        return user.username;
    }
}