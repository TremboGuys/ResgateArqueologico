using UnityEngine;
using UnityEditor.Networking;
using UnityEngine.Networking;
using System.ComponentModel;

public class ManagerQuiz : MonoBehaviour
{
    public static ManagerQuiz Instance { get; private set; }
    Quiz quiz;
    void Start()
    {
        StartCoroutine(QuizService.GetQuiz("http://localhost:8000/api/quizzes/1/"));
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public static void OnQuizReceived(Quiz json)
    {
        Instance.quiz = json;

        Debug.Log(Instance.quiz.theme);
    }
    //     void Update()
    // {

    // }
}
