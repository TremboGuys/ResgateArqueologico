using UnityEngine;
using UnityEditor.Networking;
using UnityEngine.Networking;
using TMPro;
using System.ComponentModel;
using System.Collections.Generic;
using System;

public class ManagerQuiz : MonoBehaviour
{
    public static ManagerQuiz Instance { get; private set; }
    Quiz quiz;
    [SerializeField] private GameObject quizPanel;
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

    public void UserResponse(string response)
    {
        bool correctAnswer = (response == Char.ToString(quiz.questions[numCurrentQuestion].response)) ? true : false;
        user_responses.Add(quiz.questions[numCurrentQuestion].id, response);
        numCurrentQuestion++;
        ChangeQuestion();
    }
}
