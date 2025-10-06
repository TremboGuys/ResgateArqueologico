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
            PersistentManager.Register("ManagerLevel", gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void ChooseLevel(string id)
    {
        PersistentManager.Instance.SetQuizId(int.Parse(id));
        SceneManager.LoadScene("Quiz");
    }
}