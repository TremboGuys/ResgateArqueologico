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
        Debug.Log(json);

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();

        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();

        string response = webRequest.downloadHandler.text;

        User user = JsonUtility.FromJson<User>(response);

        ManagerLogin.Instance.OnUserReceived(user);

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Success POST: " + response);
        }
        else
        {
            Debug.Log("Error in POST: " + response);
        }
    }
}