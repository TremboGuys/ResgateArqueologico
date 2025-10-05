using System.Collections.Generic;

[System.Serializable]
public class PlayerRanking
{
    public string username;
    public string photo;
    public string score;
    public int hits;
    public string position;

    PlayerRanking(string username, string photo, int score, int hits, string position)
    {
        this.username = username;
        this.photo = photo + "_0";
        this.score = score.ToString();
        this.hits = hits;
        this.position = position;
    }
}

[System.Serializable]
public class PlayerRankingList
{
    public List<PlayerRanking> ranking;
}