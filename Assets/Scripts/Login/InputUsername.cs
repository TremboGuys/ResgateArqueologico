using UnityEngine;
using TMPro;

public class InputUsername : MonoBehaviour
{
    [SerializeField] private TMP_InputField input;
    void Start()
    {
        // input.onValueChanged.AddListener(OnValueChanged);
        input.onEndEdit.AddListener(OnTextSubmitted);
    }

    private void OnTextSubmitted(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ManagerLogin.Instance.inputUsername = text;
            ManagerLogin.Instance.Login();
            return;
        }
        ManagerLogin.Instance.inputUsername = text;
    }
}
