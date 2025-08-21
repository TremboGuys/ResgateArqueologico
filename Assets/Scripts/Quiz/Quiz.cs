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
    public List<Question> questions = new List<Question>();

    public void Shuffle()
    {
        List<Question> newArrayQuestions = new List<Question>();
        System.Random rd = new System.Random();

        while (newArrayQuestions.Count < 10)
        {
            int index = rd.Next(0, questions.Count);
            Question question = questions[index];
            newArrayQuestions.Add(question);
            questions.RemoveAt(index);
        }

        questions = newArrayQuestions;
    }
}