using System.Collections.Generic;
using UnityEngine;

public class ManagerRanking : MonoBehaviour
{
    public static ManagerRanking Instance { get; private set; }
    [SerializeField] private List<RankingEntries> rankingEntries;

    void Start()
    {
        string url = "https://resgate-arqueologico-backend.onrender.com/api/playerQuizzes?quiz=" + ManagerQuiz.Instance.GetIdQuiz() +"&player=" + ManagerLogin.Instance.GetIdUser();
        StartCoroutine(RankingService.GetRanking(url));
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PersistentManager.Register("ManagerRanking", gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnRankingReceived(List<PlayerRanking> ranking)
    {
        ImageUtils imageUtils = new();
        if (ranking.Count == 11)
        {
            rankingEntries[10].rankingGameObject.SetActive(true);
        }
        for (int i = 0; i < ranking.Count; i++)
        {
            PlayerRanking pr = ranking[i];
            if (i == 10)
            {
                rankingEntries[i].position.text = pr.position;
            }
            rankingEntries[i].username.text = pr.username;
            rankingEntries[i].score.text = pr.score;
            if (pr.username == ManagerLogin.Instance.GetUsername())
            {
                if (i > 2)
                {
                    rankingEntries[i].position.color = Color.red;
                }
                rankingEntries[i].username.color = Color.red;
                rankingEntries[i].score.color = Color.red;
            }
            rankingEntries[i].icon.sprite = imageUtils.ChangeImage(pr.photo);
        }
    }
}