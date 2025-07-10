using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEditor.PackageManager.Requests;

public class QuizService : MonoBehaviour
{
    // public static QuizService Instance { get; private set; }

    // void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    public static IEnumerator GetQuiz(string uri)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(uri);

        yield return webRequest.SendWebRequest();

        string json = webRequest.downloadHandler.text;

        Quiz quiz = JsonUtility.FromJson<Quiz>(json);

        quiz.Shuffle();

        ManagerQuiz.OnQuizReceived(quiz);
    }

    public static IEnumerator PostUserResponses(string uri, string json)
    {
        using UnityWebRequest webRequest = new UnityWebRequest(uri, "POST");

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();

        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        string response = webRequest.downloadHandler.text;

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Success POST: " + response);
        }
        else
        {
            Debug.Log("Error in POST: " + response);
        }
    }
}