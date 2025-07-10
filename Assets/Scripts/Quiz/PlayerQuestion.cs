using System;
using UnityEditor.Rendering;

[System.Serializable]
public class PlayerQuestion
{
    public PlayerQuestion(bool hit, int id_quiz, int id_question, int id_player)
    {
        this.hit = hit;
        this.id_quiz = id_quiz;
        this.id_question = id_question;
        this.id_player = id_player;
    }
    public bool hit;
    public int id_quiz;
    public int id_question;
    public int id_player;
}