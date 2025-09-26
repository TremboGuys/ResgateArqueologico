using UnityEngine;
using UnityEngine.UI;

public class ButtonLogin : MonoBehaviour
{
    [SerializeField] private Button button;

    void Start()
    {
        button.onClick.AddListener(() =>
        {
            Debug.Log("OnClick");
            ManagerLogin.Instance.Login();
        });
    }
}
