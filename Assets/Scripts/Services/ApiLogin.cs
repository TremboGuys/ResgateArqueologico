using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class LoginService : MonoBehaviour
{
    public static IEnumerator Login(string uri, string json)
    {
        using UnityWebRequest webRequest = new(uri, "POST");

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();

        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        string response = webRequest.downloadHandler.text;

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            User user = JsonUtility.FromJson<User>(response);
            ManagerLogin.Instance.OnUserReceived(user);
        }

        else
        {
            ErrorJson error = JsonUtility.FromJson<ErrorJson>(response);

            Debug.Log(error);
            if (error.username.Length > 0)
            {
                ManagerLogin.Instance.ShowErrorUserMessage();
            }
            Debug.LogError("Error in POST: " + error.username[0]);
        }
    }
}