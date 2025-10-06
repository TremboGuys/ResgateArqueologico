using System;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    public static PersistentManager Instance { get; private set; }
    private int quizId;
    private static Dictionary<String, GameObject> persistentObjects = new();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Register("PersistentManager", gameObject);
        }
        else
        {
            Remove("PersistentManager");
        }
    }

    public static void Register(string nameGo, GameObject go)
    {
        persistentObjects.Add(nameGo, go);
        DontDestroyOnLoad(go);
    }

    public static void Remove(string nameGo)
    {
        if (persistentObjects.ContainsKey(nameGo))
        {
            Destroy(persistentObjects[nameGo]);
            persistentObjects.Remove(nameGo);
        }
    }

    public void SetQuizId(int quizId)
    {
        this.quizId = quizId;
    }

    public int GetIdQuiz()
    {
        return quizId;
    }
}