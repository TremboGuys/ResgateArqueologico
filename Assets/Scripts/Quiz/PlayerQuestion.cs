using System;

[System.Serializable]
public class PlayerQuestion
{
    public bool hit;
    public int quiz;
    public int question;
    public int player;
    public PlayerQuestion(bool hit, int quiz, int question, int player)
    {
        this.hit = hit;
        this.quiz = quiz;
        this.question = question;
        this.player = player;
    }
}