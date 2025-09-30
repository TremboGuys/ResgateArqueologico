public class PlayerQuiz
{
    public int player;
    public int quiz;
    public int hits;
    public int score;

    public PlayerQuiz(int hits, int player, int quiz, int score)
    {
        this.player = player;
        this.quiz = quiz;
        this.hits = hits;
        this.score = score;
    }
}