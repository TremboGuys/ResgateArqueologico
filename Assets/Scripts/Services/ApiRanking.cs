using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RankingService : MonoBehaviour
{
    public static IEnumerator GetRanking(string uri)
    {
        using UnityWebRequest webRequest = UnityWebRequest.Get(uri);

        yield return webRequest.SendWebRequest();

        string json = webRequest.downloadHandler.text;

        Debug.Log(json);

        PlayerRankingList rankingList = JsonUtility.FromJson<PlayerRankingList>(json);

        ManagerRanking.Instance.OnRankingReceived(rankingList.ranking);
    }
}