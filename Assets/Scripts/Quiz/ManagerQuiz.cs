using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.Mathematics;

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
    [SerializeField] private Score score;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private ImagesQuiz images;
    private Dictionary<int, bool> user_responses = new();
    private int numCurrentQuestion = 0;
    private int numHits = 0;
    private Coroutine timerCoroutine;
    private int remainingTime;

    void Start()
    {
        InitializeQuiz();
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PersistentManager.Register("ManagerQuiz", gameObject);
            // SceneManager.sceneLoaded += OnSceneLoaded;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializeQuiz()
    {
        StartCoroutine(QuizService.GetQuiz("https://resgate-arqueologico-backend.onrender.com/api/quizzes/" + PersistentManager.Instance.GetIdQuiz() + "/"));
        wrongPanel.SetActive(false);
        correctPanel.SetActive(false);
        scorePanel.SetActive(false);
    }

    // public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     if (scene.name == "Quiz" && wrongPanel == null)
    //     {
    //         RestartQuiz();
    //     }
    // }

    public void OnPointerClickRestartQuiz()
    {
        StartCoroutine(QuizService.GetQuiz("https://resgate-arqueologico-backend.onrender.com/api/quizzes/" + quiz.id + "/"));
        SceneManager.LoadScene("Quiz");
    }

    // public void RestartQuiz()
    // {
    //     Debug.Log("Entrou no RestartQuiz");
    //     numCurrentQuestion = 0;
    //     numHits = 0;
    //     user_responses.Clear();
    //     quizPanel = GameObject.Find("Canvas/QuizPanel");
    //     Debug.Log(quizPanel);
    //     correctPanel = GameObject.Find("Canvas/CorrectPanel");
    //     Debug.Log(correctPanel);
    //     wrongPanel = GameObject.Find("Canvas/WrongPanel");
    //     Debug.Log(wrongPanel);
    //     scorePanel = GameObject.Find("Canvas/ScorePanel");
    //     Debug.Log(scorePanel);
    //     theme = GameObject.Find("Canvas/QuizPanel/LeftPanel/TitleQuiz/Theme").GetComponent<TMP_Text>();
    //     Debug.Log(theme);
    //     statement_question = GameObject.Find("Canvas/QuizPanel/RightPanel/QuestionArea/Statement/StatementText").GetComponent<TMP_Text>();
    //     alternative_a = GameObject.Find("Canvas/QuizPanel/RightPanel/QuestionArea/Alternative_A/Alternative_A_Text").GetComponent<TMP_Text>();
    //     alternative_b = GameObject.Find("Canvas/QuizPanel/RightPanel/QuestionArea/Alternative_B/Alternative_B_Text").GetComponent<TMP_Text>();
    //     alternative_c = GameObject.Find("Canvas/QuizPanel/RightPanel/QuestionArea/Alternative_C/Alternative_C_Text").GetComponent<TMP_Text>();
    //     alternative_d = GameObject.Find("Canvas/QuizPanel/RightPanel/QuestionArea/Alternative_D/Alternative_D_Text").GetComponent<TMP_Text>();
    //     score = GameObject.Find("Canvas/ScorePanel/MainPanel").GetComponent<Score>();
    //     timerText = GameObject.Find("Canvas/QuizPanel/LeftPanel/ArtifactImage/TimerText").GetComponent<TMP_Text>();
    //     images = GameObject.Find("ImagesQuiz").GetComponent<ImagesQuiz>();
    // }

    public void OnQuizReceived(Quiz json)
    {
        Debug.Log(json);
        quiz = json;
        theme.text = Instance.quiz.theme.ToUpper();
        if (theme.text.Length > 15)
        {
            theme.fontSize = 32;
        }
        ChangeQuestion();
        images.ChangeImages(quiz.image);
        timerCoroutine = StartCoroutine(Timer(300));
    }

    public void ChangeQuestion()
    {
        Question q = quiz.questions[numCurrentQuestion];
        string text = (numCurrentQuestion + 1) + "- " + q.statement;
        statement_question.text = text;
        int statementLength = text.Length;

        if (statementLength <= 80)
        {
            statement_question.fontSize = 40;
        }
        else if (statementLength <= 150)
        {
            statement_question.fontSize = 32;
        }
        else
        {
            statement_question.fontSize = 26;
        }
        alternative_a.text = q.alternative_a;
        alternative_b.text = q.alternative_b;
        alternative_c.text = q.alternative_c;
        alternative_d.text = q.alternative_d;
        AlternativesFontSizeCalculator();
    }

    public void AlternativesFontSizeCalculator()
    {
        List<TMP_Text> alternatives = new List<TMP_Text> { alternative_a, alternative_b, alternative_c, alternative_d };

        int biggerLength = 0;

        foreach (TMP_Text alternative in alternatives)
        {
            int length = alternative.text.Length;
            biggerLength = math.max(biggerLength, length);
        }

        foreach (TMP_Text alternative in alternatives)
        {
            if (biggerLength <= 42)
            {
                alternative.fontSize = 42;
            }
            else if (biggerLength <= 76)
            {
                alternative.fontSize = 35;
            }
            else
            {
                alternative.fontSize = 30;
            }
        }
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
        ShowScore();
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
        timerText.text = time;
    }

    public async void HiddenQuizPanel(bool correctAnswer)
    {
        DisableQuizPanel();
        Debug.Log(correctAnswer);
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
            ShowScore();
            CreateFormUserResponses();
        }
    }

    public void ShowScore()
    {
        score.ShowScore(quiz.theme, numHits);
        scorePanel.SetActive(true);
    }

    public void CreateFormUserResponses()
    {
        PlayerQuiz playerQuiz = new(numHits, ManagerLogin.Instance.GetIdUser(), quiz.id, CalculateScore());
        string jsonPlayerQuiz = JsonUtility.ToJson(playerQuiz);
        Debug.Log(jsonPlayerQuiz);
        StartCoroutine(QuizService.PostUserResponses("https://resgate-arqueologico-backend.onrender.com/api/playerQuizzes/", jsonPlayerQuiz));
        foreach (var userResponse in user_responses)
        {
            PlayerQuestion playerQuestion = new(userResponse.Value, quiz.id, userResponse.Key, ManagerLogin.Instance.GetIdUser());
            string jsonPlayerQuestion = JsonUtility.ToJson(playerQuestion);
            StartCoroutine(QuizService.PostUserResponses("https://resgate-arqueologico-backend.onrender.com/api/playerQuestions/", jsonPlayerQuestion));
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
    }

    int CalculateScore()
    {
        int discountPerTime = 0;
        if (remainingTime / 60 < 2)
        {
            discountPerTime += (5 - remainingTime / 60) * 5;
        }
        Debug.Log(numHits * quiz.xp_per_question - discountPerTime);
        return numHits * quiz.xp_per_question - discountPerTime;
    }

    public int GetIdQuiz()
    {
        return quiz.id;
    }
}
