using UnityEngine;
using UnityEditor.Networking;
using UnityEngine.Networking;
using TMPro;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

public class ManagerQuiz : MonoBehaviour
{
    public static ManagerQuiz Instance { get; private set; }
    Quiz quiz;
    [SerializeField] private GameObject quizPanel;
    [SerializeField] private GameObject correctPanel;
    [SerializeField] private GameObject wrongPanel;
    [SerializeField] private TMP_Text theme;
    [SerializeField] private TMP_Text statement_question;
    [SerializeField] private TMP_Text alternative_a;
    [SerializeField] private TMP_Text alternative_b;
    [SerializeField] private TMP_Text alternative_c;
    [SerializeField] private TMP_Text alternative_d;
    private Dictionary<int, string> user_responses = new Dictionary<int, string>();
    private int numCurrentQuestion = 0;

    void Start()
    {
        StartCoroutine(QuizService.GetQuiz("http://localhost:8000/api/quizzes/1/"));
        wrongPanel.SetActive(false);
        correctPanel.SetActive(false);
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
        Instance.ChangeQuestion();
    }

    public void ChangeQuestion()
    {
        Question q = quiz.questions[numCurrentQuestion];
        statement_question.text = (numCurrentQuestion + 1) + "- " + q.statement;
        alternative_a.text = "A) " + q.alternative_a;
        alternative_b.text = "B) " + q.alternative_b;
        alternative_c.text = "C) " + q.alternative_c;
        alternative_d.text = "D) " + q.alternative_d;
    }

    public async void HiddenQuizPanel(bool correctAnswer)
    {
        quizPanel.SetActive(false);
        if (numCurrentQuestion < 3)
        {
            numCurrentQuestion++;
            ChangeQuestion();
        }
        else
        {
            
        }

        if (correctAnswer)
        {
            correctPanel.SetActive(true);
            await Task.Delay(2000);
            correctPanel.SetActive(false);
        }
        else
        {
            wrongPanel.SetActive(true);
            await Task.Delay(2000);
            wrongPanel.SetActive(false);
        }
        quizPanel.SetActive(true);
    }

    public void UserResponse(string response)
    {
        bool correctAnswer = (response == quiz.questions[numCurrentQuestion].response) ? true : false;

        Debug.Log(quiz.questions[numCurrentQuestion].response);
        Debug.Log(correctAnswer);
        user_responses.Add(quiz.questions[numCurrentQuestion].id, response);
        HiddenQuizPanel(correctAnswer);
    }
}
