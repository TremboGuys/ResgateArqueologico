using System.Linq;
using UnityEngine;

[System.Serializable]

public class ErrorJson
{
    public string[] username;

    public ErrorJson(string username)
    {
        this.username = this.username.Append(username).ToArray();
    }
}