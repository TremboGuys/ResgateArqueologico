using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

public class ManagerQuiz : MonoBehaviour
{
    public static ManagerQuiz Instance { get; private set; }
    private Quiz quiz;
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
    [SerializeField] private ImagesQuiz images;
    private Dictionary<int, bool> user_responses = new Dictionary<int, bool>();
    private int numCurrentQuestion = 0;
    private int numHits = 0;
    private Coroutine timerCoroutine;
    private int remainingTime;

    void Start()
    {
        // StartCoroutine(QuizService.GetQuiz("https://resgate-arqueologico-backend.onrender.com/api/quizzes/1/"));
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

    public void OnQuizReceived(Quiz json)
    {
        quiz = json;
        theme.text = Instance.quiz.theme.ToUpper();
        ChangeQuestion();
        images.ChangeImages(quiz.image);
        timerCoroutine = StartCoroutine(Timer(600));
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

    public IEnumerator Timer(int time)
    {
        remainingTime = time;

        while (remainingTime > 0)
        {
            ChangeTimerText(remainingTime);
            yield return new WaitForSeconds(1f);
            remainingTime--;
        }
        DisableQuizPanel();
        ShowResults();
        CreateFormUserResponses();
    }

    public void ChangeTimerText(int remainingTime)
    {
        string time = "";
        int minutes = remainingTime / 60;
        int seconds = remainingTime % 60;
        if (minutes < 10)
        {
            time += "0";
        }
        time += minutes.ToString() + ":";
        if (seconds < 10)
        {
            time += "0";
        }
        time += seconds.ToString();
    }

    public async void HiddenQuizPanel(bool correctAnswer)
    {
        DisableQuizPanel();
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

        if (numCurrentQuestion < 9)
        {
            numCurrentQuestion++;
            ChangeQuestion();
            quizPanel.SetActive(true);
        }
        else
        {
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
            }
            ShowResults();
            CreateFormUserResponses();
        }
    }

    public void ShowResults()
    {
        Debug.Log("ShowResults chamado!");
        scoreText.text = "Acertou " + numHits + "/10" + " (" + numHits * 10 + "%)";
        scorePanel.SetActive(true);
    }

    public void CreateFormUserResponses()
    {
        PlayerQuiz playerQuiz = new(numHits, ManagerLogin.Instance.GetIdUser(), quiz.id, CalculateScore());
        string jsonPlayerQuiz = JsonUtility.ToJson(playerQuiz);
        Debug.Log(jsonPlayerQuiz);
        StartCoroutine(QuizService.PostUserResponses("http://localhost:8000/api/playerQuizzes/", jsonPlayerQuiz));
        foreach (var userResponse in user_responses)
        {
            PlayerQuestion playerQuestion = new(userResponse.Value, quiz.id, userResponse.Key, ManagerLogin.Instance.GetIdUser());
            string jsonPlayerQuestion = JsonUtility.ToJson(playerQuestion);
            StartCoroutine(QuizService.PostUserResponses("http://localhost:8000/api/playerQuestions/", jsonPlayerQuestion));
        }
    }

    public void UserResponse(string response)
    {
        bool correctAnswer = (response == quiz.questions[numCurrentQuestion].response) ? true : false;

        if (correctAnswer) numHits++;

        user_responses.Add(quiz.questions[numCurrentQuestion].id, correctAnswer);
        HiddenQuizPanel(correctAnswer);
    }

    public void DisableQuizPanel()
    {
        quizPanel.SetActive(false);
        Debug.Log("DisableQuizPanel chamado!");
    }

    int CalculateScore()
    {
        int discountPerTime = 0;
        if (remainingTime / 60 < 5)
        {
            discountPerTime += (5 - remainingTime / 60) * 5;
        }
        return numHits * quiz.xp_per_question - discountPerTime;
    }
}
