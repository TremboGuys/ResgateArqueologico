using UnityEngine;
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
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private TMP_Text theme;
    [SerializeField] private TMP_Text statement_question;
    [SerializeField] private TMP_Text alternative_a;
    [SerializeField] private TMP_Text alternative_b;
    [SerializeField] private TMP_Text alternative_c;
    [SerializeField] private TMP_Text alternative_d;
    [SerializeField] private TMP_Text scoreText;
    private Dictionary<int, bool> user_responses = new Dictionary<int, bool>();
    private int numCurrentQuestion = 0;
    private int numHits = 0;

    void Start()
    {
        StartCoroutine(QuizService.GetQuiz("https://resgate-arqueologico-backend.onrender.com/api/quizzes/1/"));
        wrongPanel.SetActive(false);
        correctPanel.SetActive(false);
        scorePanel.SetActive(false);
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
        Instance.theme.text = Instance.quiz.theme.ToUpper();
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

        if (numCurrentQuestion < 3)
        {
            numCurrentQuestion++;
            ChangeQuestion();
            quizPanel.SetActive(true);
        }
        else
        {
            ShowResults();
            CreateFormUserResponses();
        }
    }

    public void ShowResults()
    {
        scoreText.text = "Acertou " + numHits + "/10" + " (" + numHits * 10 + "%)";
        scorePanel.SetActive(true);
    }

    public void CreateFormUserResponses()
    {
        foreach (var userResponse in user_responses)
        {
            PlayerQuestion pq = new PlayerQuestion(userResponse.Value, quiz.id, userResponse.Key, 7);
            string json = JsonUtility.ToJson(pq);
            StartCoroutine(QuizService.PostUserResponses("https://resgate-arqueologico-backend.onrender.com/api/playerQuestions/", json));
        }
    }

    public void UserResponse(string response)
    {
        bool correctAnswer = (response == quiz.questions[numCurrentQuestion].response) ? true : false;

        if (correctAnswer) numHits++;

        user_responses.Add(quiz.questions[numCurrentQuestion].id, correctAnswer);
        HiddenQuizPanel(correctAnswer);
    }
}
