using UnityEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

[System.Serializable]
public class Quiz
{
    public int id;
    public string theme;
    public int xp_per_question;
    public string image;
    public List<Question> questions = new List<Question>(4);

    public void Shuffle()
    {
        List<Question> newArrayQuestions = new List<Question>(4);
        System.Random rd = new System.Random();

        while (questions.Count > 0)
        {
            int index = rd.Next(0, questions.Count);
            Question question = questions[index];
            newArrayQuestions.Add(question);
            questions.RemoveAt(index);
        }

        questions = newArrayQuestions;
    }
}