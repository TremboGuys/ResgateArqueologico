using UnityEngine;
using TMPro;
using System.ComponentModel;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class ManagerLevel : MonoBehaviour
{
    public static ManagerLevel Instance { get; private set; }

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

    public void ChooseLevel()
    {
        StartCoroutine(QuizService.GetQuiz("https://resgate-arqueologico-backend.onrender.com/api/quizzes/1"));
        SceneManager.LoadScene("Quiz");
    }
}