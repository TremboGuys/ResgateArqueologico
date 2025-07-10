using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

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
}